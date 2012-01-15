using System.Linq;

namespace ReferenceDataManager.Sample.OrgHierarchy.Commands
{
    public static class OrgHierarchyAPI
    {
        public static Unit CreateUnit(this ChangeSetBuilder pendingChanges, string name, Address address)
        {
            return pendingChanges
                .AddCommandAndPreviewTarget<Unit>(new CreateUnitCommand(name, address));
        }

        public static Hierarchy CreateHierarchy(this ChangeSetBuilder pendingChanges)
        {
            return pendingChanges
                .AddCommandAndPreviewTarget<Hierarchy>(new CreateHierarchyCommand());
        }

        public static void SetHierarchyRoot(this ChangeSetBuilder pendingChanges, Hierarchy hierarchy, Unit newRoot)
        {
            var hierarchyNode = EnsureIsPartOfHierarchy(newRoot, hierarchy, pendingChanges);
            pendingChanges.Add(new SetHierarchyRootCommand(hierarchy.Id, hierarchyNode.Id));
        }

        public static void SetParent(this ChangeSetBuilder pendingChanges, Hierarchy hierarchy, Unit target, Unit newParent)
        {
            var parentHierarchyNode = EnsureIsPartOfHierarchy(newParent, hierarchy, pendingChanges);
            var hierarchyNode = EnsureIsPartOfHierarchy(target, hierarchy, pendingChanges);
            EnsureDetachedFromFormerParent(hierarchyNode, pendingChanges);
            pendingChanges
                .Add(new AttachChildCommand(parentHierarchyNode.Id, hierarchyNode.Id))
                .Add(new SetParentCommand(hierarchyNode.Id, parentHierarchyNode.Id));
        }

        private static void EnsureDetachedFromFormerParent(HierarchyNode hierarchyNode, ChangeSetBuilder pendingChanges)
        {
            if (hierarchyNode.Parent != null)
            {
                pendingChanges.Add(new DetachChildCommand(hierarchyNode.Parent.Id, hierarchyNode.Id));
            }
        }

        private static HierarchyNode EnsureIsPartOfHierarchy(Unit target, Hierarchy hierarchy, ChangeSetBuilder pendingChanges)
        {
            var hierarchyNode = target.Nodes.SingleOrDefault(x => x.Context.Id == hierarchy.Id);
            if (hierarchyNode == null)
            {
                var nodeId = ObjectId.NewUniqueId();
                return pendingChanges
                    .Add(new AttachToHierarchyCommand(target.Id, nodeId))
                    .Add(new CreateHierarchyNodeCommand(nodeId, target.Id, hierarchy.Id))
                    .GetPreview()
                    .GetById<HierarchyNode>(nodeId);
            }
            return hierarchyNode;
        }
    }
}