using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeAccountingSystem.BLL.DTO
{
    public class ProgrammerFullModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public IEnumerable<SkillModel> Skills { get; set; }
    }
}
