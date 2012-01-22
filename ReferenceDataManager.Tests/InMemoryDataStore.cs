using System;
using System.Collections.Generic;

namespace ReferenceDataManager.Tests
{
    public class InMemoryDataStore : IDataStore
    {
        private readonly List<ChangeSet> changeSets = new List<ChangeSet>();

        public List<ChangeSet> ChangeSets
        {
            get { return changeSets; }
        }

        public IEnumerable<ChangeSet> LoadAllChangeSets()
        {
            return changeSets;
        }

        public void Store(UncommittedChangeSet changeSet)
        {
            changeSets.Add(new ChangeSet(changeSet.Id, changeSet.ParentId, "Some comment", changeSet.Commands));
            if (OnStored != null)
            {
                OnStored(this, new EventArgs());
            }
        }

        public event EventHandler OnStored;
    }
}