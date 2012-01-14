using System;
using Moq;
using NUnit.Framework;

// ReSharper disable InconsistentNaming
namespace ReferenceDataManager.Tests
{
    [TestFixture]
    public class ObjectFacadeTests
    {
        [Test]
        public void It_returns_same_reference_each_time_when_getting_object_with_certain_id()
        {
            var objectId = ObjectId.NewUniqueId();
            var changeSetId = ChangeSetId.NewUniqueId();
            var dataFacadeMock = new Mock<IDataFacade>();
            var objectFacade = new ObjectFacade(dataFacadeMock.Object);
            
            var snapshot = objectFacade.GetSnapshot(changeSetId);
            var firstReference = snapshot.GetById<TestingObject>(objectId);
            var secondReference = snapshot.GetById<TestingObject>(objectId);

            Assert.IsNotNull(firstReference);
            Assert.AreSame(firstReference, secondReference);
        }

        [Test]
        public void It_maps_attribute_values_to_properties()
        {
            var objectId = ObjectId.NewUniqueId();
            var changeSetId = ChangeSetId.NewUniqueId();
            var dataFacadeMock = new Mock<IDataFacade>();
            var objectState = new ObjectState(objectId);
            objectState.ModifyAttribute("TextValue", "SomeValue");
            objectState.ModifyAttribute("IntValue", 42);
            dataFacadeMock.Setup(x => x.GetById(changeSetId, objectId)).Returns(objectState);
            var objectFacade = new ObjectFacade(dataFacadeMock.Object);
            
            var snapshot = objectFacade.GetSnapshot(changeSetId);
            var o = snapshot.GetById<TestingObject>(objectId);

            Assert.AreEqual("SomeValue", o.TextValue);
            Assert.AreEqual(42, o.IntValue);
        }

        [Test]
        public void It_maps_attributes_without_values_to_default_values_for_their_typs()
        {
            var objectId = ObjectId.NewUniqueId();
            var changeSetId = ChangeSetId.NewUniqueId();
            var dataFacadeMock = new Mock<IDataFacade>();
            var objectState = new ObjectState(objectId);
            dataFacadeMock.Setup(x => x.GetById(changeSetId, objectId)).Returns(objectState);
            var objectFacade = new ObjectFacade(dataFacadeMock.Object);

            var snapshot = objectFacade.GetSnapshot(changeSetId);
            var o = snapshot.GetById<TestingObject>(objectId);

            Assert.IsNull(o.TextValue);
            Assert.AreEqual(0, o.IntValue);
        }

        public class TestingObject
        {
            public virtual string TextValue { get; set; }
            public virtual int IntValue { get; set; }
        }
    }
}
// ReSharper restore InconsistentNaming
