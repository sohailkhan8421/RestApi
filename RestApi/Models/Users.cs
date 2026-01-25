using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestApi.Models
{
    public class Users
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(125)")]
        public string FullName { get; set; }

        [Column(TypeName = "varchar(125)")]
        public string Email { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Phone { get; set; }

        [Column(TypeName = "varchar(125)")]
        public string Username { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Password { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string? token { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Address { get; set; }

        [Column(TypeName = "varchar(125)")]
        public string City { get; set; }

        [Column(TypeName = "varchar(125)")]
        public string Role { get; set; }

        public string? CreatedDate { get; set; }

    }
}
