﻿using System.ComponentModel.DataAnnotations;

namespace SuitcaseApi.DTO.Requests
{
    public class LoginRequestDto
    {
        [Required]
        [MaxLength(200, ErrorMessage = "Max length is 200")]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}