using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities
{
    public class Customer : BaseEntity
    {
        [Required]
        public string name { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string phone { get; set; }

        public string address { get; set; }
    }
}
