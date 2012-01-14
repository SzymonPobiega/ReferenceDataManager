using System;
using NUnit.Framework;

// ReSharper disable InconsistentNaming
namespace ReferenceDataManager.Tests
{
    [TestFixture]
    public class ObjectTypeDescriptorRepositoryTests
    {
        [Test]
        public void It_returns_null_for_not_registered_type_id()
        {
            var objectTypeId = ObjectTypeId.NewUniqueId();
            var repository = new ObjectTypeDescriptorRepository();

            var notRegistered = repository.GetByTypeId(objectTypeId);

            Assert.IsNull(notRegistered);
        }

        [Test]
        public void It_returns_object_type_by_its_id()
        {
            var objectTypeId = ObjectTypeId.NewUniqueId();
            var repository = new ObjectTypeDescriptorRepository();
            var typeDescriptor = new ObjectTypeDescriptor(typeof (object), objectTypeId, new AttributeDescriptor[] {},
                                                          new RelationDescriptor[] {});

            repository.RegisterTypeDescriptor(typeDescriptor);
            var registered = repository.GetByTypeId(objectTypeId);

            Assert.IsNotNull(registered);
        }

        [Test]
        public void It_throws_exception_when_trying_to_register_two_types_with_same_id()
        {
            var objectTypeId = ObjectTypeId.NewUniqueId();
            var repository = new ObjectTypeDescriptorRepository();
            var firstTypeDescriptor = new ObjectTypeDescriptor(typeof(object), objectTypeId, new AttributeDescriptor[] { }, new RelationDescriptor[] { });
            var secondTypeDescriptor = new ObjectTypeDescriptor(typeof(int), objectTypeId, new AttributeDescriptor[] { }, new RelationDescriptor[] { });
            repository.RegisterTypeDescriptor(firstTypeDescriptor);

            Assert.Throws<InvalidOperationException>(() => repository.RegisterTypeDescriptor(secondTypeDescriptor));
        }
    }
}
// ReSharper restore InconsistentNaming
