﻿using System.ComponentModel.DataAnnotations;

namespace CrudAppUsingASPCoreWebAPI.Models
{
  
        public class Student
        {
            public int id { get; set; }

        [Required(ErrorMessage = "Please Enter a Name")]
        [RegularExpression(@"^[^\d]+$", ErrorMessage = "Name can not contain numbers or special characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter a Father Name")]
        [RegularExpression(@"^[^\d]+$", ErrorMessage = "Name can not contain numbers or special characters")]
        public string FatherName { get; set; }
        [Required(ErrorMessage = "Please Enter a Mother Name")]
        [RegularExpression(@"^[^\d]+$", ErrorMessage = "Name can not contain numbers or special characters")]
        public string MotherName { get; set; }
        [Required(ErrorMessage = "Please Select a Gender.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Please Select DOB")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Please Enter a Aadhar Number.")]
        //[Range(10000000000, 999999999999, ErrorMessage = "Input valid Adhar No consist of 12 Digit")]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "Input valid Adhar No consist of 12 Digit")]
        public long AadharNo { get; set; }
        } 
}
