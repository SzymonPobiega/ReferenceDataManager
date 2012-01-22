using System.Collections.Generic;

namespace ReferenceDataManager
{
    public interface ITimelineStore
    {
        void Store(Timeline timeline);
        void Delete(Timeline timeline);
        IEnumerable<Timeline> LoadAll();
    }
}