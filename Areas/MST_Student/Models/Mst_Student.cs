using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace database.Areas.MST_Student.Models
{
    public class Mst_Student
    {
        public int? StudentID { get; set; }

        [Required(ErrorMessage = "Branch Name is Required")]
        [DisplayName("Branch Name")]
        public int? BranchID { get; set; }

        [Required(ErrorMessage = "City Name is Required")]
        [DisplayName("City Name")]
        public int? CityID { get; set; }

        [Required(ErrorMessage = "Student Name is Required")]
        [DisplayName("StudentName Name")]

        public string? StudentName { get; set;}

        [Required(ErrorMessage = "Mobile No Student Name is Required")]
        [DisplayName("Mobile No Student")]
        public string? MobileNoStudent { get; set;}

        [Required(ErrorMessage = "Student Email is Required")]
        [DisplayName("Student Email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Mobile No Father is Required")]
        [DisplayName("Mobile No Father Name")]
        public string? MobileNoFather { get; set; }

        [Required(ErrorMessage = "Student Address is Required")]
        [DisplayName("Student Address")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Student BirthDate is Required")]
        [DisplayName("Student BirthDate")]
        public DateTime? BirthDate { get; set;}

        [Required(ErrorMessage = "Student Age is Required")]
        [DisplayName("Student Age")]
        public int? Age { get; set; }

        [Required(ErrorMessage = "Student IsActive is Required")]
        [DisplayName("Student IsActive")]
        public bool? IsActive { get; set; }

        [Required(ErrorMessage = "Student Gender is Required")]
        [DisplayName("Student Gender")]
        public string? Gender { get; set; }

        [Required(ErrorMessage = "Student Password is Required")]
        [DisplayName("Student Password")]
        public string? Password { get; set; }
        public DateTime? Created { get; set;}
        public DateTime? Modified { get; set;}
    }

    
}
