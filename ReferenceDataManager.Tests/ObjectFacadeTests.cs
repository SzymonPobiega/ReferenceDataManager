using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;

// ReSharper disable InconsistentNaming
namespace ReferenceDataManager.Tests
{
    [TestFixture]
    public class ObjectFacadeTests
    {
        private ObjectId objectId;
        private ChangeSetId changeSetId;
        private ObjectTypeDescriptorRepository typeRepository;
        private Mock<IDataFacade> dataFacadeMock;
        private ObjectFacade objectFacade;
        private ObjectTypeId objectTypeId;
        private const string objectTypeIdValue = "70B6B877-06E2-4FE5-8F60-C83437B3B499";

        [Test]
        public void It_returns_same_reference_each_time_when_getting_object_with_certain_id()
        {
            var objectState = new ObjectState(objectId, objectTypeId);
            dataFacadeMock.Setup(x => x.GetById(changeSetId, objectId)).Returns(objectState);

            var snapshot = objectFacade.GetSnapshot(changeSetId);
            var firstReference = snapshot.GetById<TestingObject>(objectId);
            var secondReference = snapshot.GetById<TestingObject>(objectId);

            Assert.IsNotNull(firstReference);
            Assert.AreSame(firstReference, secondReference);
        }

        [Test]
        public void It_maps_attribute_values_to_properties()
        {
            var objectState = new ObjectState(objectId, objectTypeId);
            objectState.ModifyAttribute("TextValue", "SomeValue");
            objectState.ModifyAttribute("IntValue", 42);
            dataFacadeMock.Setup(x => x.GetById(changeSetId, objectId)).Returns(objectState);

            var snapshot = objectFacade.GetSnapshot(changeSetId);
            var o = snapshot.GetById<TestingObject>(objectId);

            Assert.AreEqual("SomeValue", o.TextValue);
            Assert.AreEqual(42, o.IntValue);
        }

        [Test]
        public void It_does_not_map_attributes_that_are_not_listed_in_object_type_descriptor()
        {
            var objectState = new ObjectState(objectId, objectTypeId);
            objectState.ModifyAttribute("NotMappedProperty", 10.5m);
            dataFacadeMock.Setup(x => x.GetById(changeSetId, objectId)).Returns(objectState);

            var snapshot = objectFacade.GetSnapshot(changeSetId);
            var o = snapshot.GetById<TestingObject>(objectId);

            Assert.AreEqual(0m, o.NotMappedProperty);
        }

        [Test]
        public void It_maps_relations_to_collections_of_proxy_objects()
        {
            const string relationName = "Children";

            var firstChildObjectId = ObjectId.NewUniqueId();
            var secondChildObjectId = ObjectId.NewUniqueId();
            var parentObjectState = new ObjectState(objectId, objectTypeId);
            parentObjectState.Attach(firstChildObjectId, relationName);
            parentObjectState.Attach(secondChildObjectId, relationName);
            dataFacadeMock.Setup(x => x.GetById(changeSetId, objectId)).Returns(parentObjectState);
            dataFacadeMock.Setup(x => x.GetById(changeSetId, firstChildObjectId)).Returns(new ObjectState(firstChildObjectId, objectTypeId));
            dataFacadeMock.Setup(x => x.GetById(changeSetId, secondChildObjectId)).Returns(new ObjectState(secondChildObjectId, objectTypeId));

            var snapshot = objectFacade.GetSnapshot(changeSetId);
            var o = snapshot.GetById<TestingObject>(objectId);

            var collection = o.Children;
            Assert.AreEqual(2, collection.Count());
        }

        [Test]
        public void It_maps_non_single_valued_relations_to_refefences_to_proxy_objects()
        {
            const string relationName = "Parent";

            var parentObjectId = ObjectId.NewUniqueId();
            var thisObjectState = new ObjectState(objectId, objectTypeId);
            thisObjectState.Attach(parentObjectId, relationName);
            dataFacadeMock.Setup(x => x.GetById(changeSetId, objectId)).Returns(thisObjectState);
            dataFacadeMock.Setup(x => x.GetById(changeSetId, parentObjectId)).Returns(new ObjectState(parentObjectId, objectTypeId));

            var snapshot = objectFacade.GetSnapshot(changeSetId);
            var o = snapshot.GetById<TestingObject>(objectId);

            var parent = o.Parent;
            Assert.IsNotNull(parent);
        }

        [Test]
        public void Getting_object_reference_by_traversing_a_relation_preserves_identity_property()
        {
            var parentObjectId = ObjectId.NewUniqueId();
            var thisObjectState = new ObjectState(objectId, objectTypeId);
            thisObjectState.Attach(parentObjectId, "Parent");
            var parentObjectState = new ObjectState(parentObjectId, objectTypeId);
            parentObjectState.Attach(objectId, "Children");
            dataFacadeMock.Setup(x => x.GetById(changeSetId, objectId)).Returns(thisObjectState);
            dataFacadeMock.Setup(x => x.GetById(changeSetId, parentObjectId)).Returns(parentObjectState);

            var snapshot = objectFacade.GetSnapshot(changeSetId);
            var objectDirectly = snapshot.GetById<TestingObject>(objectId);

            var chidViaParentViaChild = objectDirectly.Parent.Children.First();

            Assert.AreSame(chidViaParentViaChild, objectDirectly);
        }

        [Test]
        public void It_maps_attributes_without_values_to_default_values_for_their_typs()
        {
            var objectState = new ObjectState(objectId, objectTypeId);
            dataFacadeMock.Setup(x => x.GetById(changeSetId, objectId)).Returns(objectState);

            var snapshot = objectFacade.GetSnapshot(changeSetId);
            var o = snapshot.GetById<TestingObject>(objectId);

            Assert.IsNull(o.TextValue);
            Assert.AreEqual(0, o.IntValue);
        }

        [SetUp]
        public void SetUp()
        {
            objectId = ObjectId.NewUniqueId();
            objectTypeId = new ObjectTypeId(new Guid(objectTypeIdValue));
            changeSetId = ChangeSetId.NewUniqueId();
            typeRepository = new ObjectTypeDescriptorRepository().RegisterUsingReflection<TestingObject>();
            dataFacadeMock = new Mock<IDataFacade>();
            objectFacade = new ObjectFacade(dataFacadeMock.Object, typeRepository); 
        }

        [ObjectType(objectTypeIdValue)]
        public class TestingObject
        {
            public virtual decimal NotMappedProperty { get; set; }
            [ObjectAttribute]
            public virtual string TextValue { get; set; }
            [ObjectAttribute]
            public virtual int IntValue { get; set; }
            [ObjectRelation]
            public virtual IEnumerable<TestingObject> Children { get; set; }
            [ObjectRelation]
            public virtual TestingObject Parent { get; set; }
        }
    }
}
// ReSharper restore InconsistentNaming
