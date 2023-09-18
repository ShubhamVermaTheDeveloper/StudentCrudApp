using System.ComponentModel.DataAnnotations;    // Namespace provides various attributes for validating data

namespace StudentCrudApp.Models
{
    public class StudentsMOD    // Model class
    {
        public int Id { get; set; }     // Id for unique identify

        [Required(ErrorMessage = "Please Enter a Name")]     //validate the input
        [RegularExpression(@"^[^\d]+$", ErrorMessage = "Name can not contain numbers or special characters")]
        public String Name { get; set; }

        [Required(ErrorMessage = "Please Enter a Father Name")]
        [RegularExpression(@"^[^\d]+$", ErrorMessage = "Name can not contain numbers or special characters")]
        public String FatherName { get; set; }

        [Required(ErrorMessage = "Please Enter a Mother Name")]
        [RegularExpression(@"^[^\d]+$", ErrorMessage = "Name can not contain numbers or special characters")]
        public String MotherName { get; set; }

        
        [Required(ErrorMessage = "Please Select a Gender.")]
        public String Gender { get; set; }

        [Required(ErrorMessage = "Please Select DOB")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Please Enter a Aadhar Number.")]
        [Range(10000000000, 999999999999, ErrorMessage = "Input valid Adhar No consist of 12 Digit")]
        public long AadharNo { get; set; }

    }
}
