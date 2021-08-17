using System.Collections.Generic;

namespace KnowledgeAccountingSystem.DAL.Entities
{
    public class Programmer
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int? ManagerId { get; set; }
        public Manager Manager { get; set; }
        public IEnumerable<Skill> Skills { get; set; }
    }
}
