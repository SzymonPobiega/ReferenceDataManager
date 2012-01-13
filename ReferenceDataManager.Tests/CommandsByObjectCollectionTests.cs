using System;
using NUnit.Framework;

// ReSharper disable InconsistentNaming
namespace ReferenceDataManager.Tests
{
    [TestFixture]
    public class CommandsByObjectCollectionTests
    {
        [Test]
        public void Command_targeted_to_certain_object_are_executed()
        {
            var targetObjectId = Guid.NewGuid();
            var command = new TestingCommand(targetObjectId);
            var collection = new CommandsByObjectCollection();
            collection.Add(command);

            collection.ExecuteCommands(targetObjectId, null);

            Assert.IsTrue(command.IsExecuted);
        }

        [Test]
        public void Command_not_targeted_to_certain_object_are_not_executed()
        {
            var targetObjectId = Guid.NewGuid();
            var command = new TestingCommand(Guid.NewGuid());
            var collection = new CommandsByObjectCollection();
            collection.Add(command);

            collection.ExecuteCommands(targetObjectId, null);

            Assert.IsFalse(command.IsExecuted);
        }

        private class TestingCommand : AbstractCommand
        {
            private bool isExecuted;

            public TestingCommand(Guid targetObjectId) : base(targetObjectId)
            {
            }

            public bool IsExecuted
            {
                get { return isExecuted; }
            }

            public override void Execute(ICommandExecutionContext context)
            {
                isExecuted = true;
            }
        }
    }
}
// ReSharper restore InconsistentNaming
