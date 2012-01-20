using System.Linq;
using NUnit.Framework;

// ReSharper disable InconsistentNaming
namespace ReferenceDataManager.Tests
{
    [TestFixture]
    public class CompositeCommandExecutionContextTests
    {
        [Test]
        public void It_returns_new_context_object_id()
        {
            var objectId = ObjectId.NewUniqueId();
            var composite = new CompositeCommandExecutionContext();

            var contextReference = composite.GetFor(objectId);

            Assert.IsNotNull(contextReference);
        }

        [Test]
        public void It_returns_same_instance_of_context_for_particular_object_id()
        {
            var objectId = ObjectId.NewUniqueId();
            var composite = new CompositeCommandExecutionContext();

            var firstReference = composite.GetFor(objectId);
            var secondReference = composite.GetFor(objectId);

            Assert.AreSame(firstReference, secondReference);
        }

        [Test]
        public void It_return_all_unempty_contexts()
        {
            var composite = new CompositeCommandExecutionContext();
            composite.GetFor(ObjectId.NewUniqueId()).Create(ObjectTypeId.NewUniqueId());
            composite.GetFor(ObjectId.NewUniqueId()).Create(ObjectTypeId.NewUniqueId());
            composite.GetFor(ObjectId.NewUniqueId());

            Assert.AreEqual(2, composite.GetAll().Count());
        }
    }
}
// ReSharper restore InconsistentNaming
