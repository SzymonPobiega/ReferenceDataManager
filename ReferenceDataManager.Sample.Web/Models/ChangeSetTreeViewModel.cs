using System.Collections.Generic;

namespace ReferenceDataManager.Sample.Web.Models
{
    public class ChangeSetTreeViewModel
    {
        public List<ChangeSetTreeNodeViewModel> RootNodes { get; set; }

        public ChangeSetTreeViewModel()
        {
            RootNodes = new List<ChangeSetTreeNodeViewModel>();
        }
    }
}