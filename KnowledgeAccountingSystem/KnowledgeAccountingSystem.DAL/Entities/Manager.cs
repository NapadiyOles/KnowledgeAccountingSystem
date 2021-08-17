using System.Collections.Generic;

namespace KnowledgeAccountingSystem.DAL.Entities
{
    public class Manager
    {
        public int Id { get; set; }
        public User User { get; set; }
        public IEnumerable<Programmer> Programmers { get; set; }
    }
}
