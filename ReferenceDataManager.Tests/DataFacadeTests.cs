using System;
using System.Collections.Generic;
using NUnit.Framework;

// ReSharper disable InconsistentNaming
namespace ReferenceDataManager.Tests
{
    [TestFixture]
    public class DataFacadeTests
    {
        [Test]
        public void It_throws_exception_when_trying_to_load_two_change_sets_with_same_ids()
        {
            var changeSetId = ChangeSetId.NewUniqueId();
            var facade = new DataFacade();
            facade.LoadChangeSet(new ChangeSet(changeSetId, null, new AbstractCommand[] {}));
            
            Assert.Throws<InvalidOperationException>( () => facade.LoadChangeSet(new ChangeSet(changeSetId, null, new AbstractCommand[] {})) );
        }

        [Test]
        public void It_throws_exception_when_trying_to_load_object_in_context_of_non_existing_change_set()
        {
            var objectId = ObjectId.NewUniqueId();
            var changeSetId = ChangeSetId.NewUniqueId();
            var facade = new DataFacade();

            Assert.Throws<InvalidOperationException>(() => facade.GetById(changeSetId, objectId));
        }

        [Test]
        public void It_creates_object_and_returns_it_by_id()
        {
            var changeSetId = ChangeSetId.NewUniqueId();
            var objectId = ObjectId.NewUniqueId();
            var objectTypeId = Guid.NewGuid();
            var facade = new DataFacade();
            var commands = new List<AbstractCommand>
                               {
                                   new CreateObjectCommand(objectTypeId, objectId)
                               };
            facade.LoadChangeSet(new ChangeSet(changeSetId, null, commands));

            var o = facade.GetById(changeSetId, objectId);

            Assert.IsNotNull(o);
        }        
    }
}
// ReSharper restore InconsistentNaming
