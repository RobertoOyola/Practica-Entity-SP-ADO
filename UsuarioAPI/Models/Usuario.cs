using System.ComponentModel.DataAnnotations;

namespace UsuarioAPI.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Secondname { get; set; }
        public string Mail { get; set; }
        public decimal Salary { get; set; }
        public int Numberx { get; set; }
    }
}
