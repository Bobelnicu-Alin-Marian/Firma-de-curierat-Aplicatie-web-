using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FirmaCurierat.Models
{
    public class Tarif
    {
        [Key]
        public int Id_tarif { get; set; }

        [Required(ErrorMessage = "Categoria de greutate este obligatorie.")]
        [Display(Name = "Categorie Greutate")]
        public string CategorieGreutate { get; set; }

        [Required(ErrorMessage = "Prețul local este obligatoriu.")]
        [Display(Name = "Preț Local (Buc/Ilfov)")]
        [DataType(DataType.Currency)]
        public decimal PretLocal { get; set; }

        [Required(ErrorMessage = "Prețul național este obligatoriu.")]
        [Display(Name = "Preț Național (RO)")]
        [DataType(DataType.Currency)]
        public decimal PretNational { get; set; }

        [Required(ErrorMessage = "Prețul internațional este obligatoriu.")]
        [Display(Name = "Preț Internațional (UE)")]
        [DataType(DataType.Currency)]
        public decimal PretInternational { get; set; }
        public ICollection<Colet>? Colete { get; set; }
    }
}