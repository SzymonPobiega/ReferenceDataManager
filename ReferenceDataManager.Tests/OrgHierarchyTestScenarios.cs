using System.Linq;
using NUnit.Framework;
using ReferenceDataManager.Sample.OrgHierarchy;
using ReferenceDataManager.Sample.OrgHierarchy.Commands;

// ReSharper disable InconsistentNaming
namespace ReferenceDataManager.Tests
{
    [TestFixture]
    public class OrgHierarchyTestScenarios
    {
        private ObjectFacade objectFacade;
        private DataFacade dataFacade;

        [Test]
        public void Parent_and_child()
        {
            ObjectId hierarchyId;
            var builder = new ChangeSetBuilder(objectFacade, null);
            {
                var hierarchy = builder.CreateHierarchy();
                hierarchyId = hierarchy.Id;
                var parentUnit = builder.CreateUnit("Parent", new Address("Lubicz", "23", "Krakow", "PL"));
                var childUnit = builder.CreateUnit("Child", null);

                builder.SetHierarchyRoot(hierarchy, parentUnit);
                builder.SetParent(hierarchy, childUnit, parentUnit);
            } 
            var view = objectFacade.GetSnapshot(builder.PendingChanges);
            {
                var hierarchy = view.GetById<Hierarchy>(hierarchyId);
                var rootUnit = hierarchy.RootUnit;
                var childUnits = hierarchy.RootUnit.GetChildrenWithin(hierarchy);
                var firstChildUnit = childUnits.First();

                Assert.AreEqual("Parent", rootUnit.Name);
                Assert.AreEqual(new Address("Lubicz", "23", "Krakow", "PL"), rootUnit.Address);
                Assert.AreEqual("Child", firstChildUnit.Name);

                var allUnits = view.List<Unit>().ToList();
                Assert.AreEqual(2, allUnits.Count());
            }
        }

        [SetUp]
        public void SetUp()
        {
            var dataStore = new InMemoryDataStore();
            var commandExecutor = new CommandExecutor()
                .RegisterCommandHandler(new CreateUnitCommandHandler())
                .RegisterCommandHandler(new CreateHierarchyCommandHandler())
                .RegisterCommandHandler(new SetHierarchyRootCommandHandler())
                .RegisterCommandHandler(new MoveUnitCommandHandler())
                .RegisterCommandHandler(new CreateHierarchyNodeCommandHandler())
                .RegisterCommandHandler(new AttachToHierarchyCommandHandler())
                .RegisterCommandHandler(new AttachChildCommandHandler())
                .RegisterCommandHandler(new DetachChildCommandHandler())
                .RegisterCommandHandler(new SetParentCommandHandler());

            var typeRepository = new ObjectTypeDescriptorRepository()
                .RegisterUsingReflection<Unit>()
                .RegisterUsingReflection<HierarchyNode>()
                .RegisterUsingReflection<Hierarchy>();

            dataFacade = new DataFacade(commandExecutor, dataStore, new IncrementalCachingSnapshotFactory());
            objectFacade = new ObjectFacade(dataFacade, typeRepository, commandExecutor);
        }
    }
}
// ReSharper restore InconsistentNaming
