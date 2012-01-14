using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

// ReSharper disable InconsistentNaming
namespace ReferenceDataManager.Tests
{
    [TestFixture]
    public class ObjectTypeDescriptorRepositoryExtensionsTests
    {
        private ObjectTypeDescriptor typeDescriptor;
        private const string objectTypeIdValue = "287EB638-CBAA-41A8-84DE-226665721ACA";

        [Test]
        public void It_can_register_type_using_reflection()
        {
            Assert.IsNotNull(typeDescriptor);
        }

        [Test]
        public void It_can_discover_public_object_attribute_using_reflection()
        {
            var attributeDescriptor = typeDescriptor.Attributes.Single(x => x.PropertyName == "TextValue");
            Assert.AreEqual("TextValueAttribute", attributeDescriptor.AttributeName);
        }

        [Test]
        public void It_can_discover_protected_object_attribute_using_reflection()
        {
            var attributeDescriptor = typeDescriptor.Attributes.Single(x => x.PropertyName == "IntValue");
            Assert.AreEqual("IntValue", attributeDescriptor.AttributeName);
        }

        [Test]
        public void It_can_discover_public_object_relation_using_reflection()
        {
            var relationDescriptor = typeDescriptor.Relations.Single(x => x.PropertyName == "Children");
            Assert.AreEqual("ChildrenRelation", relationDescriptor.RelationName);
        }

        [SetUp]
        public void SetUp()
        {
            var objectTypeId = new ObjectTypeId(new Guid(objectTypeIdValue));
            var repository = new ObjectTypeDescriptorRepository();

            repository.RegisterUsingReflection(typeof(TestingObject));
            typeDescriptor = repository.GetByTypeId(objectTypeId);
        }

        [ObjectType(objectTypeIdValue)]
        public class TestingObject
        {
            [ObjectAttribute("TextValueAttribute")]
            public virtual string TextValue { get; set; }

            [ObjectAttribute]
            protected virtual int IntValue { get; set; }

            [ObjectRelation("ChildrenRelation")]
            public virtual IEnumerable<TestingObject> Children { get; set; }

            [ObjectRelation]
            public virtual IEnumerable<TestingObject> Related { get; set; }
        }
    }
}
// ReSharper restore InconsistentNaming
