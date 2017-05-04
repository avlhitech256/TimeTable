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
    
    public partial class Chair
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Chair()
        {
            this.ChairToSpecializations = new HashSet<ChairToSpecialization>();
        }
    
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Nullable<long> FacultyId { get; set; }
        public bool Active { get; set; }
        public System.DateTimeOffset Created { get; set; }
        public System.DateTimeOffset LastModify { get; set; }
        public string UserModify { get; set; }
    
        public virtual Faculty Faculty { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChairToSpecialization> ChairToSpecializations { get; set; }
    }
}
