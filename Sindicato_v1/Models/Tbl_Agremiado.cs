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
    
    public partial class Tbl_Agremiado
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_Agremiado()
        {
            this.Tbl_Deduccion = new HashSet<Tbl_Deduccion>();
        }
    
        public int id_Agremiado { get; set; }
        public int id_Usuario { get; set; }
        public string profesion { get; set; }
        public string colegio_Profesional { get; set; }
        public string puesto { get; set; }
        public int afiliado { get; set; }
        public string grado_Academico { get; set; }
        public int id_LugarTrabajo { get; set; }
        public int estado { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Deduccion> Tbl_Deduccion { get; set; }
        public virtual Tbl_Departamento Tbl_Departamento { get; set; }
        public virtual Tbl_Usuario Tbl_Usuario { get; set; }
    }
}