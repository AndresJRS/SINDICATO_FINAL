//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sindicato_v1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tbl_Empleado
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_Empleado()
        {
            this.Tbl_Gestion = new HashSet<Tbl_Gestion>();
        }
    
        public int id_Empleado { get; set; }
        public string cargo { get; set; }
        public string superior_Inmediato { get; set; }
        public int id_LugarTrabajo { get; set; }
        public int id_Usuario { get; set; }
        public int estado { get; set; }
        public Nullable<int> total_Vacaciones { get; set; }
        public Nullable<int> cant_AusenciasJustificadas { get; set; }
        public Nullable<int> cant_AusenciasInjustificadas { get; set; }
        public Nullable<int> vac_Utilizadas { get; set; }
        public Nullable<int> vac_Restantes { get; set; }
    
        public virtual Tbl_Departamento Tbl_Departamento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Gestion> Tbl_Gestion { get; set; }
        public virtual Tbl_Usuario Tbl_Usuario { get; set; }
    }
}