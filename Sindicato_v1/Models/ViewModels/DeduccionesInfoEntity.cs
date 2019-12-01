using System;

namespace Sindicato_v1.Models.ViewModels
{
    public class DeduccionesInfoEntity
    {
        public int id_Ded { get; set; }
        public DateTime fecha_ded { get; set; }
        public decimal monto { get; set; }
        public string id_Ag { get; set; }
        public int estado { get; set; }
    }
}