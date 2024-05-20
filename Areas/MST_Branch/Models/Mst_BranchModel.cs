using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace database.Areas.MST_Branch.Models
{
    public class Mst_BranchModel
    {
        public int? BranchID { get; set; }

        [Required(ErrorMessage = "Branch Name is Required")]
        [DisplayName("Branch Name")]
        public string? BranchName { get; set; }

        [Required(ErrorMessage = "Branch Code is Required")]
        [DisplayName("Branch Code")]
        public string? BranchCode { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
    }
    public class Mst_BranchDropDownModel
    {
        public int?BranchID { get; set; }
        public string? BranchName { get; set; }
    }
}
