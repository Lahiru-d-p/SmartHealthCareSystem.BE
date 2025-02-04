﻿using SmartHealthCareSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHealthCareSystem.Domain.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
        public string? ContactEMail { get; set; }
        public Address? Address { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
    }
}
