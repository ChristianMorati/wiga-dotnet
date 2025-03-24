using System;
using wiga.Models;
using System.Collections.Generic;

namespace wiga.Dto
{
    public class CompanyCreateDto
    {
        public string Name { get; set; } = null!;

        public string Cnpj { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
