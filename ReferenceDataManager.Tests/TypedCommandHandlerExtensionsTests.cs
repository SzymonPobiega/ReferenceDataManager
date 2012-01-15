using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;

// ReSharper disable InconsistentNaming
namespace ReferenceDataManager.Tests
{
    [TestFixture]
    public class TypedCommandHandlerExtensionsTests
    {
        [Test]
        public void It_can_create_new_instance()
        {
            var contextMock = new Mock<ICommandExecutionContext>();
            var handler = new CreateTestingCommandHandler();

            handler.Handle(new TestingCommand(ObjectId.NewUniqueId()), contextMock.Object);

            contextMock.Verify(x => x.Create(ObjectTypeId.Parse("4FBF64D4-96A5-4693-87ED-670E88DDD705")), Times.Once());
        }

        [Test]
        public void It_can_modify_attribute_value()
        {
            var contextMock = new Mock<ICommandExecutionContext>();
            var handler = new ModifyTestingCommandHandler();

            handler.Handle(new TestingCommand(ObjectId.NewUniqueId()), contextMock.Object);

            contextMock.Verify(x => x.ModifyAttribute("TextValue", "SomeValue"), Times.Once());
        }

        [Test]
        public void It_can_attach_to_multi_valued_relation()
        {
            var contextMock = new Mock<ICommandExecutionContext>();
            var handler = new AttachMultiValuedTestingCommandHandler();
            var targetObjectId = ObjectId.NewUniqueId();

            handler.Handle(new TestingCommand(targetObjectId), contextMock.Object);

            contextMock.Verify(x => x.Attach(targetObjectId, "MVRelation"), Times.Once());
        }

        [Test]
        public void It_can_attach_to_single_valued_relation()
        {
            var contextMock = new Mock<ICommandExecutionContext>();
            var handler = new AttachSingleValuedTestingCommandHandler();
            var targetObjectId = ObjectId.NewUniqueId();

            handler.Handle(new TestingCommand(targetObjectId), contextMock.Object);

            contextMock.Verify(x => x.Attach(targetObjectId, "SVRelation"), Times.Once());
        }

        [Test]
        public void It_can_dettach_to_multi_valued_relation()
        {
            var contextMock = new Mock<ICommandExecutionContext>();
            var handler = new DetachMultiValuedTestingCommandHandler();
            var targetObjectId = ObjectId.NewUniqueId();

            handler.Handle(new TestingCommand(targetObjectId), contextMock.Object);

            contextMock.Verify(x => x.Detach(targetObjectId, "MVRelation"), Times.Once());
        }

        [Test]
        public void It_can_detach_to_single_valued_relation()
        {
            var contextMock = new Mock<ICommandExecutionContext>();
            var handler = new DetachSingleValuedTestingCommandHandler();
            var targetObjectId = ObjectId.NewUniqueId();

            handler.Handle(new TestingCommand(targetObjectId), contextMock.Object);

            contextMock.Verify(x => x.Detach(targetObjectId, "SVRelation"), Times.Once());
        }

        [ObjectType("4FBF64D4-96A5-4693-87ED-670E88DDD705")]
        public class TestingObject
        {
            [ObjectAttribute("TextValue")]
            public string TextProperty { get; set; }

            [ObjectRelation("SVRelation")]
            public TestingObject SingleValuedRelation { get; set; }

            [ObjectRelation("MVRelation")]
            public IEnumerable<TestingObject> MultiValuedRelation { get; set; }
        }

        public class TestingCommand : TypedCommand<TestingObject>
        {
            public TestingCommand(ObjectId targetObjectId) : base(targetObjectId)
            {
            }
        }

        public class CreateTestingCommandHandler : ITypedCommandHandler<TestingCommand, TestingObject>
        {
            public void Handle(TestingCommand command, ICommandExecutionContext context)
            {
                context.Create(this);
            }
        }

        public class ModifyTestingCommandHandler : ITypedCommandHandler<TestingCommand, TestingObject>
        {
            public void Handle(TestingCommand command, ICommandExecutionContext context)
            {
                context.ModifyAttribute(this, x => x.TextProperty, "SomeValue");
            }
        }

        public class AttachMultiValuedTestingCommandHandler : ITypedCommandHandler<TestingCommand, TestingObject>
        {
            public void Handle(TestingCommand command, ICommandExecutionContext context)
            {
                context.Attach(this, x => x.MultiValuedRelation, command.TargetObjectId);
            }
        }

        public class AttachSingleValuedTestingCommandHandler : ITypedCommandHandler<TestingCommand, TestingObject>
        {
            public void Handle(TestingCommand command, ICommandExecutionContext context)
            {
                context.Attach(this, x => x.SingleValuedRelation, command.TargetObjectId);
            }
        }

        public class DetachMultiValuedTestingCommandHandler : ITypedCommandHandler<TestingCommand, TestingObject>
        {
            public void Handle(TestingCommand command, ICommandExecutionContext context)
            {
                context.Detach(this, x => x.MultiValuedRelation, command.TargetObjectId);
            }
        }

        public class DetachSingleValuedTestingCommandHandler : ITypedCommandHandler<TestingCommand, TestingObject>
        {
            public void Handle(TestingCommand command, ICommandExecutionContext context)
            {
                context.Detach(this, x => x.SingleValuedRelation, command.TargetObjectId);
            }
        }
    }
}
// ReSharper restore InconsistentNaming
