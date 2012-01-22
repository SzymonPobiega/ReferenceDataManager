using System.Collections.Generic;
using System.Linq;

namespace ReferenceDataManager.Sample.Web.Models
{
    public class ChangeSetTreeViewModelFactory
    {
        private const int MaxTitleLength = 30;
        private const string TitleEllipsis = "...";

        public ChangeSetTreeViewModel Create(IList<ChangeSet> changeSets)
        {
            var roots = changeSets.Where(x => !x.ParentId.HasValue);
            return new ChangeSetTreeViewModel
            {
                RootNodes = roots.Select(x => CreateNodeModel(x, changeSets)).ToList()
            };
        }

        private static ChangeSetTreeNodeViewModel CreateNodeModel(ChangeSet changeSet, IEnumerable<ChangeSet> allChangeSets)
        {
            var children = allChangeSets.Where(x => x.ParentId == changeSet.Id);
            return new ChangeSetTreeNodeViewModel
                       {
                           Id = changeSet.Id.ToString(),
                           Title = MakeTitle(changeSet),
                           Children = children.Select(x => CreateNodeModel(x, allChangeSets)).ToList()
                       };
        }

        private static string MakeTitle(ChangeSet changeSet)
        {
            return changeSet.Comment.Length <= MaxTitleLength 
                ? changeSet.Comment
                : changeSet.Comment.Substring(0, MaxTitleLength - TitleEllipsis.Length) + TitleEllipsis;
        }
    }
}