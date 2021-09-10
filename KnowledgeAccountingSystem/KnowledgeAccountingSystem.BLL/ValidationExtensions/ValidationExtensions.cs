using KnowledgeAccountingSystem.BLL.DTO;
using KnowledgeAccountingSystem.BLL.Validation;
using KnowledgeAccountingSystem.DAL.Entities;
using KnowledgeAccountingSystem.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace KnowledgeAccountingSystem.BLL.ValidationExtensions
{
    public static class ValidationExtensions
    {
        static string cond = @"(\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)";
        public static bool IsModelInvalid(this UserModel model)
        {
            if (string.IsNullOrEmpty(model.Email) ||
                string.IsNullOrEmpty(model.Name) ||
                string.IsNullOrEmpty(model.Surname) ||
                string.IsNullOrEmpty(model.Password) ||
                !char.IsUpper(model.Name[0]) ||
                !char.IsUpper(model.Surname[0]) ||
                !Regex.IsMatch(model.Email, cond)
                )
                return true;
            else
                return false;
        }

        public static bool IsSkillExist(this SkillModel model, IUnitOfWork context, int id)
        {
            Thread.Sleep(500);
            skillArea skillName = ToEnum<skillArea>(model.Name);
            if (context.SkillRepository.GetAllProgrammersSkillsById(id).Select(x => x.Name).Contains(skillName))
                return true;
            else
                return false;
        }
        private static T ToEnum<T>(string value)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), value, true);
            }
            catch
            {
                throw new KnowledgeAccountException($"value { value } was not found", HttpStatusCode.Unauthorized);
            }
        }
        public static bool IsSkillModelNotValid(this SkillModel model)
        {
            skillArea skillName = ToEnum<skillArea>(model.Name);
            lvl skillLevel = ToEnum<lvl>(model.Lvl);
            if (Enum.GetValues(typeof(skillArea)).Cast<skillArea>().Contains(skillName) &&
                Enum.GetValues(typeof(lvl)).Cast<lvl>().Contains(skillLevel))
                return false;
            else
                return true;
        }

        public static bool IsAccountNotExist(this int id, IUnitOfWork context)
        {
            if (context.UserRepository.FindAll().Select(x => x.Id).Contains(id))
                return false;
            else
                return true;
        }

        public static bool IsEmailAlreadyExist(this string email, IUnitOfWork context)
        {
            if (context.UserRepository.FindAll().Select(x => x.Email).Contains(email))
                return true;
            else
                return false;
        }
    }
}
