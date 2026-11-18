using System;
using System.Collections.Generic;
using System.Text;

namespace UserManagementSystem.Domain.Dtos
{
    public class UserUpdateInputDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string NationalCode { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
