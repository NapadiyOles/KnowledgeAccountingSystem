using KnowledgeAccountingSystem.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeAccountingSystem.BLL.Interfaces
{
    public interface IAdminServise
    {
        Task ChangeRoleAsync(int userId);
        IEnumerable<UserModel> GetAllUsers();
    }
}
