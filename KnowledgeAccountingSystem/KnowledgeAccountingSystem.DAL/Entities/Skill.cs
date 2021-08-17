namespace KnowledgeAccountingSystem.DAL.Entities
{
    public enum lvl
    {
        Low = 1,
        Middle,
        Advanced
    }

    public enum skillArea
    {
        DotNet = 1,
        ORM,
        SQL,
        HTML,
        CSS,
        Angular,
        JavaScript,
        Patterns
    }

    public class Skill
    {
        public int Id { get; set; }
        public skillArea Name { get; set; }
        public lvl Lvl { get; set; }
        public int ProgrammerId { get; set; }
        public Programmer Programmer { get; set; }
    }
}
