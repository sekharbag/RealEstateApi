using System.ComponentModel.DataAnnotations;

namespace RealEstateApi.Models
{
    public class Catagory
    {
        public int id { get; set; }

        [Required(ErrorMessage="category Name cant be empty")] ///this property is required and cant be null or empty
        public string Name { get; set; }

        [Required(ErrorMessage ="ImageUrl cant be empty")]
        public string ImageUrl { get; set; }

        public ICollection<Property> Properties { get; set; }
    


    }
}
