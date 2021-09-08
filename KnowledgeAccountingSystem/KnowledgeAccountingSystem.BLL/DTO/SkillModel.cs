using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeAccountingSystem.BLL.DTO
{
    public class SkillModel
    {
      public int Id { get; set; }
      public  string Name { get; set; }
      public string Lvl { get; set; }

      public int ProgrammerId { get; set; }
    }
}
