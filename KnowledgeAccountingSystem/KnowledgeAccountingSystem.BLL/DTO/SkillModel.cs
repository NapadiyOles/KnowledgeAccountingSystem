using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeAccountingSystem.BLL.DTO
{
    public class SkillModel
    {
        public class Skill
        {
            public int Id { get; set; }
            public  DAL.Entities.skillArea Name { get; set; }
            public DAL.Entities.lvl Lvl { get; set; }
            public int ProgrammerId { get; set; }
        }
    }
}
