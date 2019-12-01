﻿using System.ComponentModel.DataAnnotations;

namespace Sindicato_v1.Models.ViewModels
{
    public class AddEmpleadosViewModel
    {
        public int id_Persona { get; set; }

        [Required]
        [Display(Name = "Cédula")]
        public int cedula { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        [RegularExpression("^[a-zA-ZáÁéÉóÓÚú]+$", ErrorMessage = "Digitar unicamente letras")]
        public string nombre { get; set; }

        [Required]
        [Display(Name = "Apellido Paterno")]
        [RegularExpression("^[a-zA-ZáÁéÉóÓÚú]+$", ErrorMessage = "Digitar unicamente letras")]
        public string primer_apellido { get; set; }

        [Required]
        [Display(Name = "Apellido Materno")]
        [RegularExpression("^[a-zA-ZáÁéÉóÓÚú]+$", ErrorMessage = "Digitar unicamente letras")]
        public string segundo_apellido { get; set; }

        [Required]
        [Display(Name = "Género")]
        public string Genero { get; set; }

        [Required]
        [Display(Name = "Nacionalidad")]
        [RegularExpression("^[a-zA-ZáÁéÉóÓÚú]+$", ErrorMessage = "Digitar unicamente letras")]
        public string nacionalidad { get; set; }

        [Required]
        [Display(Name = "Teléfono")]
        public int Telefono { get; set; }

        [Required]
        public int id_ECivil { get; set; }

        [Required]
        [Display(Name = "Dirección")]
        [RegularExpression("^[a-zA-ZáÁéÉóÓÚú]+$", ErrorMessage = "Digitar unicamente letras")]
        public string Direccion { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string correo_Electronico { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Nacimiento")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string fecha_Nac { get; set; }

        public string contrasenia { get; set; }

        ////Tipo usuario
        public int id_TipoUsu { get; set; }


        ////Rol 
        public int id_Rol { get; set; }


        ////Empleado 
        public int id_Departamento { get; set; }


        [Required]
        //[StringLength(50)]
        [Display(Name = "Cargo")]
        [RegularExpression("^[a-zA-ZáÁéÉóÓÚú]+$", ErrorMessage = "Digitar unicamente letras")]
        public string cargo { get; set; }

        [Required]
        //[StringLength(50)]
        [Display(Name = "Superior Inmediato")]
        [RegularExpression("^[a-zA-ZáÁéÉóÓÚú]+$", ErrorMessage = "Digitar unicamente letras")]
        public string superior_Inmediato { get; set; }
    }
}