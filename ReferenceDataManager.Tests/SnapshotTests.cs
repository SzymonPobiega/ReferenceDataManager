using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

// ReSharper disable InconsistentNaming
namespace ReferenceDataManager.Tests
{
    [TestFixture]
    public class SnapshotTests
    {
        private ICommandExecutor commandExecutor;

        [Test]
        public void It_returns_null_for_non_exiting_object()
        {
            var snapshot = new IncrementalCachingSnapshot(NullSnapshot.Instance, commandExecutor, new UncommittedChangeSet(null, "Some comment"));

            var nonExisting = snapshot.GetById(ObjectId.NewUniqueId());

            Assert.IsNull(nonExisting);
        }

        [Test]
        public void It_can_create_object_and_get_by_id()
        {
            var objectId = ObjectId.NewUniqueId();
            var objectTypeId = ObjectTypeId.NewUniqueId();

            var changeSet = new UncommittedChangeSet(null, "Some comment")
                .Add(new CreateObjectCommand(objectTypeId, objectId));

            var snapshot = new IncrementalCachingSnapshot(NullSnapshot.Instance, commandExecutor, changeSet);
            var o = snapshot.GetById(objectId);

            Assert.IsNotNull(o);
        }

        [Test]
        public void It_can_get_object_by_id_even_if_it_was_creates_as_part_of_previous_snapshot()
        {
            var objectId = ObjectId.NewUniqueId();
            var objectTypeId = ObjectTypeId.NewUniqueId();

            var changeSet = new UncommittedChangeSet(null, "Some comment")
                .Add(new CreateObjectCommand(objectTypeId, objectId));

            var snapshot = new IncrementalCachingSnapshot(NullSnapshot.Instance, commandExecutor, changeSet);

            var nextSnapshot = new IncrementalCachingSnapshot(snapshot, commandExecutor, new UncommittedChangeSet(changeSet.Id, "Some comment"));
            var o = nextSnapshot.GetById(objectId);

            Assert.IsNotNull(o);
        }

        [Test]
        public void It_can_attach_one_object_to_another()
        {
            const string relationName = "RelationName";
            var refererObjectId = ObjectId.NewUniqueId();
            var refereeObjectId = ObjectId.NewUniqueId();
            var objectTypeId = ObjectTypeId.NewUniqueId();

            var changeSet = new UncommittedChangeSet(null, "Some comment")
                .Add(new CreateObjectCommand(objectTypeId, refererObjectId))
                .Add(new CreateObjectCommand(objectTypeId, refereeObjectId))
                .Add(new AttachObjectCommand(refererObjectId, refereeObjectId, relationName));

            var snapshot = new IncrementalCachingSnapshot(NullSnapshot.Instance, commandExecutor, changeSet);
            
            var o = snapshot.GetById(refererObjectId);
            var relatedToFirst = o.GetRelated(relationName);
            Assert.IsTrue(relatedToFirst.Any(x => x == refereeObjectId));
        }

        [Test]
        public void It_can_attach_one_object_to_another_event_if_it_was_created_as_part_of_previous_snapshot()
        {
            const string relationName = "RelationName";
            var refererObjectId = ObjectId.NewUniqueId();
            var refereeObjectId = ObjectId.NewUniqueId();
            var objectTypeId = ObjectTypeId.NewUniqueId();

            var changeSet = new UncommittedChangeSet(null, "Some comment")
                .Add(new CreateObjectCommand(objectTypeId, refererObjectId))
                .Add(new CreateObjectCommand(objectTypeId, refereeObjectId));

            var snapshot = new IncrementalCachingSnapshot(NullSnapshot.Instance, commandExecutor, changeSet);

            var nextChangeSet = new UncommittedChangeSet(changeSet.Id, "Some comment")
                .Add(new AttachObjectCommand(refererObjectId, refereeObjectId, relationName));
            var nextSnapshot = new IncrementalCachingSnapshot(snapshot, commandExecutor, nextChangeSet);

            var currentObjectState = nextSnapshot.GetById(refererObjectId);
            Assert.IsTrue(currentObjectState.GetRelated(relationName).Any(x => x == refereeObjectId));

            var previousObjectState = snapshot.GetById(refererObjectId);
            Assert.IsFalse(previousObjectState.GetRelated(relationName).Any(x => x == refereeObjectId));
        }

        [Test]
        public void It_stores_and_returns_object_attributes()
        {
            const string attributeName = "Attribute";
            var objectId = ObjectId.NewUniqueId();
            var objectTypeId = ObjectTypeId.NewUniqueId();

            var changeSet = new UncommittedChangeSet(null, "Some comment")
                .Add(new CreateObjectCommand(objectTypeId, objectId))
                .Add(new ModifyAttributeCommand(objectId, attributeName, "SomeValue"));

            var snapshot = new IncrementalCachingSnapshot(NullSnapshot.Instance, commandExecutor, changeSet);
            var o = snapshot.GetById(objectId);

            Assert.AreEqual("SomeValue", o.GetAttributeValue(attributeName));
        }

        [Test]
        public void Property_values_set_on_later_change_sets_override_those_set_on_earlier_change_sets()
        {
            const string property = "Property";
            var objectId = ObjectId.NewUniqueId();
            var objectTypeId = ObjectTypeId.NewUniqueId();

            var changeSet = new UncommittedChangeSet(null, "Some comment")
                .Add(new CreateObjectCommand(objectTypeId, objectId))
                .Add(new ModifyAttributeCommand(objectId, property, "SomeValue"));

            var snapshot = new IncrementalCachingSnapshot(NullSnapshot.Instance, commandExecutor, changeSet);

            var nextChangeSet = new UncommittedChangeSet(changeSet.Id, "Some comment")
                .Add(new ModifyAttributeCommand(objectId, property, "OverridingValue"));

            var nextSnapshot = new IncrementalCachingSnapshot(snapshot, commandExecutor, nextChangeSet);

            var o = nextSnapshot.GetById(objectId);

            Assert.AreEqual("OverridingValue", o.GetAttributeValue(property));
        }

        [SetUp]
        public void SetUp()
        {
            commandExecutor = new CommandExecutor()
                .RegisterCommandHandler(new AttachObjectCommandHandler())
                .RegisterCommandHandler(new CreateObjectCommandHandler())
                .RegisterCommandHandler(new ModifyAttributeCommandHandler());
        }
    }
}
// ReSharper restore InconsistentNaming
