using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace database.Areas.Loc_Country.Models
{
    public class Loc_CountryModel
    {
        public int? CountryID { get; set; }

        [Required(ErrorMessage = "Country Name is Required")]
        [DisplayName("Country Name")]
        public string? CountryName { get; set; }

        [Required(ErrorMessage = "Country Code is Required")]
        [DisplayName("Country Code")]
        public string? CountryCode { get; set; }
        public DateTime?Created { get; set;}

        public DateTime? Modified { get; set;}
        
    }
    public class LOC_CountryDropDownModel
    {
        
        public int? CountryID { get; set; }
    
        public string? CountryName { get; set; }
    }

}
