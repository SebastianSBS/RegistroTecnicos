﻿using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models;

public class Clientes
{
    [Key]
    public int ClienteId { get; set; }
    [Required(ErrorMessage = "El campo Nombres es obligatorio..")]
    public string Nombres { get; set; }
    [Required(ErrorMessage = "El campo wasa es obligatorio..")]
    public string WhatsApp { get; set; }
}
