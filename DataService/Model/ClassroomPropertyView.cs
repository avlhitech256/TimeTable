//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataService.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class ClassroomPropertyView
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int DefaultPriority { get; set; }
        public Nullable<long> UnitId { get; set; }
        public string UnitCode { get; set; }
        public string UnitShortName { get; set; }
        public string UnitName { get; set; }
    }
}
