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
        public void It_creates_object_and_returns_it_by_id()
        {
            var changeSetId = Guid.NewGuid();
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
