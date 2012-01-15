using System;
using System.Collections.Generic;

namespace ReferenceDataManager.Sample.OrgHierarchy
{
    public class InMemoryDataStore : IDataStore
    {
        private readonly List<ChangeSet> changeSets = new List<ChangeSet>();

        public List<ChangeSet> ChangeSets
        {
            get { return changeSets; }
        }

        public IEnumerable<ChangeSet> LoadAll()
        {
            return changeSets;
        }

        public void Store(UncommittedChangeSet changeSet)
        {
            changeSets.Add(new ChangeSet(changeSet.Id, changeSet.ParentId, changeSet.Commands));
        }
    }
}