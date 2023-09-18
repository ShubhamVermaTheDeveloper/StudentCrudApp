using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASPCoreWebAPICRUD.Models
{
    public partial class SHUBHAM_STUDENT_CRUD
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string Gender { get; set; }

        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        public long AadharNo { get; set; }

    }
}
