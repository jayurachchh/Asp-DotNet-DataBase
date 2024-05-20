using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace database.Areas.Loc_City.Models
{
    public class Loc_CityModel
    {
        public int? CityID {get; set;}

        [Required(ErrorMessage = "City Name is Required")]
        [DisplayName("CityName")]
        public string? CityName { get; set;}

        [Required(ErrorMessage = "State ID is Required")]
        [DisplayName("StateID")]
        public int? StateID { get; set;}

        [Required(ErrorMessage = "Country ID  is Required")]
        [DisplayName("CountryID")]
        public int? CountryID { get; set;}

        [Required(ErrorMessage = "City Code is Required")]
        [DisplayName("CityCode")]
        public string? CityCode { get; set;}
        public DateTime? CreationDate { get;set;}
        public DateTime? Modified { get; set;}   
    }
    public class LOC_CityDropDownModel
    {
        public int? CityID { get; set; }
        public string? CityName { get; set; }
    }

}
