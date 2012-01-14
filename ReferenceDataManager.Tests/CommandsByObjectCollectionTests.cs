using System;
using Moq;
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
            var executorMock = new Mock<ICommandExecutor>();
            var targetObjectId = ObjectId.NewUniqueId();
            var command = new TestingCommand(targetObjectId);
            var collection = new CommandsByObjectCollection();
            collection.Add(command);

            collection.ExecuteCommands(targetObjectId, executorMock.Object, null);

            executorMock.Verify(x => x.Execute(command, null), Times.Once());
        }

        [Test]
        public void Command_not_targeted_to_certain_object_are_not_executed()
        {
            var executorMock = new Mock<ICommandExecutor>();
            var targetObjectId = ObjectId.NewUniqueId();
            var command = new TestingCommand(ObjectId.NewUniqueId());
            var collection = new CommandsByObjectCollection();
            collection.Add(command);

            collection.ExecuteCommands(targetObjectId, executorMock.Object, null);

            executorMock.Verify(x => x.Execute(command, null), Times.Never());
        }

        private class TestingCommand : AbstractCommand
        {
            public TestingCommand(ObjectId targetObjectId) : base(targetObjectId)
            {
            }
        }
    }
}
// ReSharper restore InconsistentNaming
