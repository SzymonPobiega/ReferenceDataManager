using System.Linq;
using NUnit.Framework;

// ReSharper disable InconsistentNaming
namespace ReferenceDataManager.Tests
{
    [TestFixture]
    public class CommandsByObjectTypeCollectionTests
    {
        [Test]
        public void After_executing_commands_all_objects_of_requested_type_are_present_in_the_composite_context()
        {
            var requestedType = ObjectTypeId.NewUniqueId();

            var collection = new CommandsByObjectTypeCollection();
            var commandExecutor = new CommandExecutor()
                .RegisterCommandHandler(new CreateObjectCommandHandler())
                .RegisterCommandHandler(new ModifyAttributeCommandHandler());

            collection.Add(new CreateObjectCommand(requestedType, ObjectId.NewUniqueId()));
            collection.Add(new CreateObjectCommand(requestedType, ObjectId.NewUniqueId()));

            collection.Add(new CreateObjectCommand(ObjectTypeId.NewUniqueId(), ObjectId.NewUniqueId()));
            collection.Add(new CreateObjectCommand(ObjectTypeId.NewUniqueId(), ObjectId.NewUniqueId()));

            var compositeContext = new CompositeCommandExecutionContext();

            collection.ExecuteCommands(requestedType, commandExecutor, compositeContext);

            var objectStates = compositeContext.GetAll();
            Assert.AreEqual(2, objectStates.Count(x => x.TypeId == requestedType));
        }
    }
}
// ReSharper restore InconsistentNaming
