using System;
using NUnit.Framework;

// ReSharper disable InconsistentNaming
namespace ReferenceDataManager.Tests
{
    [TestFixture]
    public class ObjectTypeDescriptorRepositoryExtensionsTests
    {
        private const string objectTypeIdValue = "287EB638-CBAA-41A8-84DE-226665721ACA";

        [Test]
        public void It_can_object_register_type_using_reflection()
        {
            var objectTypeId = new ObjectTypeId(new Guid(objectTypeIdValue));
            var repository = new ObjectTypeDescriptorRepository();

            repository.RegisterUsingReflection(typeof (TestingObject));

            Assert.IsNotNull(repository.GetByTypeId(objectTypeId));
        }

        [ObjectType(objectTypeIdValue)]
        public class TestingObject
        {
        }
    }
}
// ReSharper restore InconsistentNaming
