﻿using System.ComponentModel.DataAnnotations;

namespace Asreyion.Core.Areas.Account.Models;

public class LoginViewModel
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public required string Password { get; set; }

    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
}
