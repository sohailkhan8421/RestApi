using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RestApi.Models
{
    public class Student
    {

        [JsonIgnore]
        public int Id { get; set; }

        [JsonIgnore]
        public int userId { get; set; }

        public int? RollNo { get; set; }


        [Column(TypeName = "varchar(125)")]
        public string FullName { get; set; }

        [Column(TypeName = "varchar(125)")]
        public string FatherName { get; set; }

        [Column(TypeName = "varchar(125)")]
        public string Email { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Phone { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Address { get; set; }

        [Column(TypeName = "varchar(125)")]
        public string City { get; set; }

        [Column(TypeName = "varchar(125)")]
        public string Course { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Description { get; set; }

        //public string? CreatedDate { get; set; }

    }
}
