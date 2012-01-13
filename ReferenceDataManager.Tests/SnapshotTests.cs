using System;
using System.Linq;
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

        [Test]
        public void It_can_get_object_by_id_even_if_it_was_creates_as_part_of_previous_snapshot()
        {
            var objectId = Guid.NewGuid();
            var objectTypeId = Guid.NewGuid();

            var snapshot = new Snapshot();
            snapshot.Load(new CreateObjectCommand(objectTypeId, objectId));

            var nextSnapshot = new Snapshot(snapshot);
            var o = nextSnapshot.GetById(objectId);

            Assert.IsNotNull(o);
        }

        [Test]
        public void It_can_attach_one_object_to_another()
        {
            const string relationName = "RelationName";
            var refererObjectId = Guid.NewGuid();
            var refereeObjectId = Guid.NewGuid();
            var objectTypeId = Guid.NewGuid();

            var snapshot = new Snapshot();
            snapshot.Load(new CreateObjectCommand(objectTypeId, refererObjectId));
            snapshot.Load(new CreateObjectCommand(objectTypeId, refereeObjectId));

            snapshot.Load(new AttachObjectCommand(refererObjectId, refereeObjectId, relationName));

            var o = snapshot.GetById(refererObjectId);
            var relatedToFirst = o.GetRelated(relationName);
            Assert.IsTrue(relatedToFirst.Any(x => x == refereeObjectId));
        }
    }
}
// ReSharper restore InconsistentNaming
