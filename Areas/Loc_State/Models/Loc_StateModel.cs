using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace database.Areas.Loc_State.Models
{
    public class Loc_StateModel
    {
        public int?  StateID  { get; set; }
       
        [Required(ErrorMessage = "State Name is Required")]
        [DisplayName("State Name")]
        public string? StateName { get; set; }

        [Required(ErrorMessage = "Country Name is Required"), DisplayName("Country Name")]
        public int? CountryID { get; set; }

        [Required(ErrorMessage = "State Code is Required")]
        [DisplayName("State Code")]
        public string? StateCode { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
    }
    public class LOC_StateDropDownModel
    {
        public int? StateID { get; set; }
        public string? StateName { get; set; }
    }
}