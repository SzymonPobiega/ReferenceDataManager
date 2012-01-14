using Moq;
using NUnit.Framework;

// ReSharper disable InconsistentNaming
namespace ReferenceDataManager.Tests
{
    [TestFixture]
    public class ObjectFacadeTests
    {
        [Test]
        public void It_returns_same_reference_each_time_when_getting_object_with_certain_id()
        {
            var objectId = ObjectId.NewUniqueId();
            var changeSetId = ChangeSetId.NewUniqueId();
            var dataFacadeMock = new Mock<IDataFacade>();
            var objectFacade = new ObjectFacade(dataFacadeMock.Object);

            var snapshot = objectFacade.GetSnapshot(changeSetId);

            var firstReference = snapshot.GetById<TestingObject>(objectId);
            var secondReference = snapshot.GetById<TestingObject>(objectId);

            Assert.IsNotNull(firstReference);
            Assert.AreSame(firstReference, secondReference);
        }

        public class TestingObject
        {
            protected virtual string TextValue { get; set; }
        }
    }
}
// ReSharper restore InconsistentNaming
