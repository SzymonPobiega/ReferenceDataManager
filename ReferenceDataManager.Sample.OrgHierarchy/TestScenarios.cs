using System.Linq;
using NUnit.Framework;

// ReSharper disable InconsistentNaming
namespace ReferenceDataManager.Sample.OrgHierarchy
{
    [TestFixture]
    public class TestScenarios
    {
        private ObjectFacade objectFacade;
        private DataFacade dataFacade;

        [Test]
        public void Parent_and_child()
        {
            ObjectId hierarchyId;
            var changeSetBuilder = new ChangeSetBuilder(objectFacade, null);
            {
                var hierarchy = changeSetBuilder.CreateHierarchy();
                hierarchyId = hierarchy.Id;
                var parentUnit = changeSetBuilder.CreateUnit("Parent", new Address("Lubicz", "23", "Krakow", "PL"));
                var childUnit = changeSetBuilder.CreateUnit("Child", null);

                changeSetBuilder.SetHierarchyRoot(hierarchy, parentUnit);
                changeSetBuilder.SetParent(hierarchy, childUnit, parentUnit);
            } 
            var view = objectFacade.GetSnapshot(changeSetBuilder.PendingChanges);
            {
                var hierarchy = view.GetById<Hierarchy>(hierarchyId);
                var rootNode = hierarchy.Root;
                var rootUnit = rootNode.Unit;
                var children = rootNode.Children;
                var childUnits = children.Select(x => x.Unit);
                var firstChildUnit = childUnits.FirstOrDefault();

                Assert.AreEqual("Parent", rootUnit.Name);
                Assert.AreEqual(new Address("Lubicz", "23", "Krakow", "PL"), rootUnit.Address);
                Assert.AreEqual("Child", firstChildUnit.Name);
            }
        }

        [SetUp]
        public void SetUp()
        {
            var dataStore = new InMemoryDataStore();
            var commandExecutor = new CommandExecutor()
                .RegisterCommandHandler(new CreateUnitCommandHandler())
                .RegisterCommandHandler(new CreateObjectCommandHandler())
                .RegisterCommandHandler(new DeleteObjectCommandHandler())
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

            dataFacade = new DataFacade(commandExecutor, dataStore);
            objectFacade = new ObjectFacade(dataFacade, typeRepository, commandExecutor);
        }
    }
}
// ReSharper restore InconsistentNaming
