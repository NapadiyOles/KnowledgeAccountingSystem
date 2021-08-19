using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeAccountingSystem.BLL.DTO
{
    public class ProgrammerModel
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public int Surname { get; set; }
        public IEnumerable<SkillModel> Skills { get; set; }

    }
}
