using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeAccountingSystem.BLL.DTO
{
    public class ManagerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public IEnumerable<ProgrammerModel> Programmers { get; set; }

    }
}
