using System.Collections.Generic;

namespace ReferenceDataManager.Sample.Web.Models
{
    public class ChangeSetTreeNodeViewModel
    {
        public string Title { get; set; }
        public string Id { get; set; }
        public List<ChangeSetTreeNodeViewModel> Children { get; set; }
    }
}