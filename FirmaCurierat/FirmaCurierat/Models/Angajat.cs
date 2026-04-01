using System.ComponentModel.DataAnnotations;

namespace FirmaCurierat.Models
{
    public class Angajat
    {
        [Key]
        public int Id_angajat { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
    }

    public class Operator : Angajat
    {
        public string Departament { get; set; }
    }

    public class Curier : Angajat
    {
        public string Categorie_permis { get; set; }
    }
}