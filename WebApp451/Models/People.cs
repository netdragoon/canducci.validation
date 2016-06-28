using System.ComponentModel.DataAnnotations;
using Canducci.MVC.Validation;

namespace WebApp451.Models
{
    public class People
    {
        [Required]
        public string Name { get; set; }

        [ValidationCPF(ErrorMessage = "CPF inválido")]
        public string Cpf { get; set; }

        [ValidationCNPJ(ErrorMessage = "CNPJ inválido")]
        public string Cnpj { get; set; }
    }
}