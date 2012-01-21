using System;
using System.Diagnostics;
using Moq;
using NUnit.Framework;

// ReSharper disable InconsistentNaming
namespace ReferenceDataManager.Tests
{
    [TestFixture]
    public class CommandExecutorTests
    {
        [Test]
        public void It_uses_the_handler_if_there_is_exact_type_match()
        {
            var executor = new CommandExecutor();
            var handler = new ExactMatchCommandHandler();
            executor.RegisterCommandHandler(handler);

            executor.Execute(new TestingCommand(ObjectId.NewUniqueId()), new Mock<ICommandExecutionContext>().Object);

            Assert.IsTrue(handler.Executed);
        }

        [Test]
        public void It_throws_if_provided_handler_does_not_handle_any_command()
        {
            var executor = new CommandExecutor();

            TestDelegate act = () => executor.RegisterCommandHandler(new object());

            Assert.Throws<InvalidOperationException>(act);
        }

        [Test]
        public void It_throws_if_provided_handler_tries_to_handle_more_than_one_command()
        {
            var executor = new CommandExecutor();

            TestDelegate act = () => executor.RegisterCommandHandler(new DoingToMuchCommandHandler());

            Assert.Throws<InvalidOperationException>(act);
        }

        [Test]
        public void It_does_not_use_the_handler_for_a_base_class()
        {
            var executor = new CommandExecutor();
            var handler = new BaseClassCommandHandler();
            executor.RegisterCommandHandler(handler);

            TestDelegate act = () => executor.Execute(new TestingCommand(ObjectId.NewUniqueId()), new Mock<ICommandExecutionContext>().Object);

            Assert.Throws<InvalidOperationException>(act);
        }
        
        [Test]
        public void It_optimizes_command_execution()
        {
            var executor = new CommandExecutor();
            var handler = new PerformanceTestingCommandHandler();
            executor.RegisterCommandHandler(handler);

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            executor.Execute(new TestingCommand(ObjectId.NewUniqueId()), new Mock<ICommandExecutionContext>().Object);
            stopwatch.Stop();
            var firstRunTime = stopwatch.ElapsedTicks;
            stopwatch.Reset();
            stopwatch.Start();
            executor.Execute(new TestingCommand(ObjectId.NewUniqueId()), new Mock<ICommandExecutionContext>().Object);
            stopwatch.Stop();
            var secondRunTime = stopwatch.ElapsedTicks;

            Assert.IsTrue(secondRunTime * 500 < firstRunTime);
        }

        public class TestingCommand : AbstractCommand
        {
            public TestingCommand(ObjectId targetObjectId) : base(targetObjectId)
            {
            }
        }

        public class PerformanceTestingCommandHandler : ICommandHandler<TestingCommand>
        {
            public void Handle(TestingCommand command, ICommandExecutionContext context)
            {
            }
        }

        public class ExactMatchCommandHandler : ICommandHandler<TestingCommand>
        {
            public bool Executed { get; set; }

            public void Handle(TestingCommand command, ICommandExecutionContext context)
            {
                Executed = true;
            }
        }

        public class BaseClassCommandHandler : ICommandHandler<AbstractCommand>
        {
            public bool Executed { get; set; }

            public void Handle(AbstractCommand command, ICommandExecutionContext context)
            {
                Executed = true;
            }
        }

        public class DoingToMuchCommandHandler : ICommandHandler<TestingCommand>, ICommandHandler<AbstractCommand>
        {
            public void Handle(TestingCommand command, ICommandExecutionContext context)
            {
            }

            public void Handle(AbstractCommand command, ICommandExecutionContext context)
            {
            }
        }
    }
}
// ReSharper restore InconsistentNaming
