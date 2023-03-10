using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeAccountingSystem.BLL.DTO
{
    public class ProgrammerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public IEnumerable<SkillModel> Skills { get; set; }

    }
}
