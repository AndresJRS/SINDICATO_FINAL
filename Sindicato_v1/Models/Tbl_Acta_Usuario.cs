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
    
    public partial class Tbl_Acta_Usuario
    {
        public int id_A_U { get; set; }
        public int id_Acta { get; set; }
        public int id_Usuario { get; set; }
    
        public virtual Tbl_Acta Tbl_Acta { get; set; }
        public virtual Tbl_Usuario Tbl_Usuario { get; set; }
    }
}