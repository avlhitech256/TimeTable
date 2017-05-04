using System;

namespace Domain.Data.SearchCriteria
{
    public interface ISearchCriteria
    {
        string Code { get; set; }
        string Name { get; set; }
        bool Active { get; set; }
        DateTime? CreatedFrom { get; set; }
        DateTime? CreatedTo { get; set; }
        DateTime? LastModifyFrom { get; set; }
        DateTime? LastModifyTo { get; set; }
        string UserModify { get; set; }
        bool IsEmpty { get; }
        void Clear();

        event EventHandler SearchCriteriaChanged;
        event EventHandler SearchCriteriaIsEmpty;
    }
}
