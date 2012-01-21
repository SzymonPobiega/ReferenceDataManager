using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;

// ReSharper disable InconsistentNaming
namespace ReferenceDataManager.Tests
{
    [TestFixture]
    public class PerformanceTests
    {
        private const string objectTypeIdValue = "70B6B877-06E2-4FE5-8F60-C83437B3B499";
        private readonly Random random = new Random();

        [Test]
        public void It_can_handle_1000_objects_with_10_changes_each()
        {
            var dataStore = new InMemoryDataStore();

            var objectIds = GenerateObjectIds(1000);

            ChangeSetId? previousChangeSetId = null;
            ChangeSetId currentChangeSetId = ChangeSetId.NewUniqueId();
            dataStore.ChangeSets.Add(new ChangeSet(currentChangeSetId, previousChangeSetId, GenerateCreateCommands(objectIds)));
            previousChangeSetId = currentChangeSetId;
            
            for (int i = 0; i < 9; i++)
            {
                currentChangeSetId = ChangeSetId.NewUniqueId();
                dataStore.ChangeSets.Add(new ChangeSet(currentChangeSetId, previousChangeSetId, GenerateUpdateCommands(i, objectIds)));
                previousChangeSetId = currentChangeSetId;
            }

            var commandExecutor = new CommandExecutor()
                .RegisterCommandHandler(new CreateObjectCommandHandler())
                .RegisterCommandHandler(new ModifyAttributeCommandHandler());
            var objectTypeRepository = new ObjectTypeDescriptorRepository()
                .RegisterUsingReflection<TestingObject>();
            var dataFacade = new DataFacade(commandExecutor, dataStore, new IncrementalCachingSnapshotFactory());
            var objectFacade = new ObjectFacade(dataFacade, objectTypeRepository, commandExecutor);

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var currentView = objectFacade.GetSnapshot(currentChangeSetId);
            var allObjects = currentView.List<TestingObject>().ToList();
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            Assert.AreEqual(1000, allObjects.Count);
            foreach (var testingObject in allObjects)
            {
                Assert.AreEqual("8", testingObject.TextValue);
            }
        }

        private static IEnumerable<ObjectId> GenerateObjectIds(int count)
        {
            return Enumerable.Range(0, count).Select(x => ObjectId.NewUniqueId()).ToList();
        }

        private static IEnumerable<AbstractCommand> GenerateCreateCommands(IEnumerable<ObjectId> objectIds)
        {
            return objectIds.Select(x => new CreateObjectCommand(ObjectTypeId.Parse(objectTypeIdValue), x));
        }

        private static IEnumerable<AbstractCommand> GenerateUpdateCommands(int iteration, IEnumerable<ObjectId> objectIds)
        {
            return objectIds.Select(x => new ModifyAttributeCommand(x, "TextValue", iteration.ToString()));
        }

        [ObjectType(objectTypeIdValue)]
        public class TestingObject
        {
            public virtual ObjectId Id { get; set; }

            [ObjectAttribute]
            public virtual string TextValue { get; set; }
        }
    }
}
// ReSharper restore InconsistentNaming
