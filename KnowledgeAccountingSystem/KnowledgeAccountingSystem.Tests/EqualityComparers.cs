using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using KnowledgeAccountingSystem.BLL.DTO;
using KnowledgeAccountingSystem.DAL.Entities;

namespace KnowledgeAccountingSystem.Tests
{
    internal class SkillEqualityComparer : IEqualityComparer<Skill>
    {
        public bool Equals([AllowNull] Skill x, [AllowNull] Skill y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.Name == y.Name
                && x.Lvl == y.Lvl
                && x.ProgrammerId == y.ProgrammerId;
        }

        public int GetHashCode([DisallowNull] Skill obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class ProgrammerEqualityComparer : IEqualityComparer<Programmer>
    {
        public bool Equals([AllowNull] Programmer x, [AllowNull] Programmer y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.ManagerId == y.ManagerId
                && x.User.Name == y.User.Name
                && x.User.Surname == y.User.Surname
                && x.Skills.Count() == y.Skills.Count()
                && x.User.Email == y.User.Email;
        }

        public int GetHashCode([DisallowNull] Programmer obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class ManagerEqualityComparer : IEqualityComparer<Manager>
    {
        public bool Equals([AllowNull] Manager x, [AllowNull] Manager y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.Programmers.Count() == y.Programmers.Count()
                && x.User.Name == y.User.Name
                && x.User.Surname == y.User.Surname
                && x.User.Email == y.User.Email;
        }

        public int GetHashCode([DisallowNull] Manager obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class UserEqualityComparer : IEqualityComparer<User>
    {
        public bool Equals([AllowNull] User x, [AllowNull] User y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                   && string.Equals(x.Name, y.Name)
                   && string.Equals(x.Surname, y.Surname)
                   && string.Equals(x.Role, y.Role)
                   && string.Equals(x.Email, y.Email);
        }

        public int GetHashCode([DisallowNull] User obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class ProgrammerModelEqualityComparer : IEqualityComparer<ProgrammerModel>
    {
        public bool Equals([AllowNull] ProgrammerModel x, [AllowNull] ProgrammerModel y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.Name == y.Name
                && x.Surname == y.Surname;
        }

        public int GetHashCode([DisallowNull] ProgrammerModel obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class SkillModelEqualityComparer : IEqualityComparer<SkillModel>
    {
        public bool Equals([AllowNull] SkillModel x, [AllowNull] SkillModel y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.Name == y.Name
                && x.Lvl == y.Lvl
                && x.ProgrammerId == y.ProgrammerId;
        }

        public int GetHashCode([DisallowNull] SkillModel obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class ManagerModelWithoutProgrammersComparer : IEqualityComparer<ManagerModelWithoutProgrammers>
    {
        public bool Equals([AllowNull] ManagerModelWithoutProgrammers x, [AllowNull] ManagerModelWithoutProgrammers y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.Name == y.Name
                && x.Surname == y.Surname;
        }

        public int GetHashCode([DisallowNull] ManagerModelWithoutProgrammers obj)
        {
            return obj.GetHashCode();
        }
    }
}

