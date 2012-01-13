using System;
using NUnit.Framework;

// ReSharper disable InconsistentNaming
namespace ReferenceDataManager.Tests
{
    [TestFixture]
    public class SnapshotTests
    {
        [Test]
        public void It_can_create_object_and_get_by_id()
        {
            var objectId = Guid.NewGuid();
            var objectTypeId = Guid.NewGuid();

            var snapshot = new Snapshot();
            snapshot.Load(new CreateObjectCommand(objectTypeId, objectId));
            var o = snapshot.GetById(objectId);

            Assert.IsNotNull(o);
        }
    }
}
// ReSharper restore InconsistentNaming
