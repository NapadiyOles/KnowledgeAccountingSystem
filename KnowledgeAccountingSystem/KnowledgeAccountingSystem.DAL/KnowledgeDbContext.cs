using KnowledgeAccountingSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeAccountingSystem.DAL
{
    public class KnowledgeDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Programmer> Programmers { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Skill> Skills { get; set; }

        public KnowledgeDbContext() { }

        public KnowledgeDbContext(DbContextOptions<KnowledgeDbContext> opt) : base(opt)
        {
            Database.EnsureCreated();
        }
    }
}
