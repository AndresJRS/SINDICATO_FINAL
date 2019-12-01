﻿using System.ComponentModel.DataAnnotations;

namespace Sindicato_v1.Models.ViewModels
{
    public class AddCompaniaViewModel
    {
        public int id_Comp { get; set; }

        [Required]
        [Display(Name = "Razón social")]
        [RegularExpression("^[a-zA-ZáÁéÉóÓÚú]+$", ErrorMessage = "Digitar unicamente letras")]
        public string razon_Soc { get; set; }

        [Required]
        [Display(Name = "Cédula Jurídica")]
        public int cedula_Jud { get; set; }

        [Required]
        [Display(Name = "Nombre de Compañia")]
        [RegularExpression("^[a-zA-ZáÁéÉóÓÚú]+$", ErrorMessage = "Digitar unicamente letras")]
        public string nombre_Comp { get; set; }

        [Required]
        [Display(Name = "Dirección")]
        [RegularExpression("^[a-zA-ZáÁéÉóÓÚú]+$", ErrorMessage = "Digitar unicamente letras")]
        public string direc { get; set; }

        [Required]
        [Display(Name = "Teléfono")]
        public int tel { get; set; }

        [Required]
        [Display(Name = "Correo Electrónico")]
        [EmailAddress]
        public string correo { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public int estado { get; set; }
    }
}