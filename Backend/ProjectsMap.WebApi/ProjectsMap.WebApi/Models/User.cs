using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectsMap.WebApi.Models
{
    public class User
    {
        public User()
        {
            
        }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        [Key]
        public int UserId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public DateTime Created { get; set; }

    }
}