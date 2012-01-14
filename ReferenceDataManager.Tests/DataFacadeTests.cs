using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;

// ReSharper disable InconsistentNaming
namespace ReferenceDataManager.Tests
{
    [TestFixture]
    public class DataFacadeTests
    {
        private ICommandExecutor commandExecutor;
        private InMemoryDataStore dataStore;

        [Test]
        public void It_throws_exception_when_trying_to_load_two_change_sets_with_same_ids()
        {
            var changeSetId = ChangeSetId.NewUniqueId();
            dataStore.ChangeSets.Add(new ChangeSet(changeSetId, null, new AbstractCommand[] {}));
            dataStore.ChangeSets.Add(new ChangeSet(changeSetId, null, new AbstractCommand[] {}));

            var facade = new DataFacade(commandExecutor, dataStore);
            Assert.Throws<InvalidOperationException>(() => facade.GetById(ObjectId.NewUniqueId(), changeSetId));
        }

        [Test]
        public void It_throws_exception_when_trying_to_load_object_in_context_of_non_existing_change_set()
        {
            var objectId = ObjectId.NewUniqueId();
            var changeSetId = ChangeSetId.NewUniqueId();
            var facade = new DataFacade(commandExecutor, dataStore);

            Assert.Throws<InvalidOperationException>(() => facade.GetById(objectId, changeSetId));
        }

        [Test]
        public void It_creates_object_and_returns_it_by_id()
        {
            var changeSetId = ChangeSetId.NewUniqueId();
            var objectId = ObjectId.NewUniqueId();
            var objectTypeId = ObjectTypeId.NewUniqueId();
            var commands = new List<AbstractCommand>
                               {
                                   new CreateObjectCommand(objectTypeId, objectId)
                               };
            dataStore.ChangeSets.Add(new ChangeSet(changeSetId, null, commands));
            var facade = new DataFacade(commandExecutor, dataStore);

            var o = facade.GetById(objectId, changeSetId);

            Assert.IsNotNull(o);
        }

        [Test]
        public void If_data_store_thorws_exception_facade_state_is_reloaded()
        {
            var changeSetId = ChangeSetId.NewUniqueId();
            var objectId = ObjectId.NewUniqueId();
            var objectTypeId = ObjectTypeId.NewUniqueId();
            var commands = new List<AbstractCommand>
                               {
                                   new CreateObjectCommand(objectTypeId, objectId)
                               };
            dataStore.ChangeSets.Add(new ChangeSet(changeSetId, null, commands));
            dataStore.OnStored += (sender, args) => { throw new Exception("Some nasty exception happened AFTER storing value"); };
            var facade = new DataFacade(commandExecutor, dataStore);

            var newChangeSet = new UncommittedChangeSet(changeSetId);
            newChangeSet.Add(new ModifyAttributeCommand(objectId, "TextValue", "SomeText"));

            try
            {
                facade.Commit(newChangeSet);
            }
            catch (Exception)
            {
                //Intentionally swallowing exception
            }

            var o = facade.GetById(objectId, newChangeSet.Id); //Would throw if new change set was not loaded into memory.
        }

        [SetUp]
        public void SetUp()
        {
            dataStore = new InMemoryDataStore();
            commandExecutor = new CommandExecutor()
                .RegisterCommandHandler(new AttachObjectCommandHandler())
                .RegisterCommandHandler(new CreateObjectCommandHandler())
                .RegisterCommandHandler(new ModifyAttributeCommandHandler());
        }
    }
}
// ReSharper restore InconsistentNaming
