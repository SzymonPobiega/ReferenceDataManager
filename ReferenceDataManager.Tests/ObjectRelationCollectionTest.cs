using System;
using System.Linq;
using NUnit.Framework;

// ReSharper disable InconsistentNaming
namespace ReferenceDataManager.Tests
{
    [TestFixture]
    public class ObjectRelationCollectionTest
    {
        [Test]
        public void It_can_store_multiple_objects_in_one_relation()
        {
            const string relationName = "Relation";
            var relationCollection = new ObjectRelationCollection();
            var firstObjectId = ObjectId.NewUniqueId();
            var secondObjectId = ObjectId.NewUniqueId();

            relationCollection.Attach(firstObjectId, relationName);
            relationCollection.Attach(secondObjectId, relationName);

            var related = relationCollection.GetRelated(relationName);
            Assert.IsTrue(related.Contains(firstObjectId));
            Assert.IsTrue(related.Contains(secondObjectId));
        }

        [Test]
        public void It_can_store_multiple_relations()
        {
            var relationCollection = new ObjectRelationCollection();
            var firstObjectId = ObjectId.NewUniqueId();
            var secondObjectId = ObjectId.NewUniqueId();

            relationCollection.Attach(firstObjectId, "RelationOne");
            relationCollection.Attach(secondObjectId, "RelationTwo");

            Assert.IsTrue(relationCollection.GetRelated("RelationOne").Contains(firstObjectId));
            Assert.IsTrue(relationCollection.GetRelated("RelationTwo").Contains(secondObjectId));
        }
    }
}
// ReSharper restore InconsistentNaming
