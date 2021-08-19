using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeAccountingSystem.BLL.DTO
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
    }
}
