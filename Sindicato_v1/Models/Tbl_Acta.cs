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
    
    public partial class Tbl_Acta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_Acta()
        {
            this.Tbl_Acta_Usuario = new HashSet<Tbl_Acta_Usuario>();
        }
    
        public int id_Acta { get; set; }
        public string tipo_Acta { get; set; }
        public System.DateTime fecha_Cre { get; set; }
        public string ubicacion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Acta_Usuario> Tbl_Acta_Usuario { get; set; }
    }
}