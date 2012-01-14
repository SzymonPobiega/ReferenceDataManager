using System;
using NUnit.Framework;

// ReSharper disable InconsistentNaming
namespace ReferenceDataManager.Tests
{
    [TestFixture]
    public class ObjectIdentityMapTests
    {
        [Test]
        public void It_returns_null_for_a_non_existing_object()
        {
            var objectId = ObjectId.NewUniqueId();
            var map = new ObjectIdentityMap();

            var nonExisting = map.GetById(objectId);

            Assert.IsNull(nonExisting);
        }

        [Test]
        public void It_returns_object_by_its_id()
        {
            var objectId = ObjectId.NewUniqueId();
            var o = new object();
            var map = new ObjectIdentityMap();

            map.Put(objectId, o);
            var existing = map.GetById(objectId);

            Assert.AreSame(o, existing);
        }

        [Test]
        public void It_throws_exception_when_trying_to_put_objects_with_same_id()
        {
            var objectId = ObjectId.NewUniqueId();
            var map = new ObjectIdentityMap();
            map.Put(objectId, new object());

            Assert.Throws<InvalidOperationException>(() => map.Put(objectId, new object()));
        }
    }
}
// ReSharper restore InconsistentNaming
