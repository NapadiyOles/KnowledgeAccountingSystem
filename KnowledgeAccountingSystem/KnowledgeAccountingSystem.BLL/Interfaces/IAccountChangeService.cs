﻿using KnowledgeAccountingSystem.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeAccountingSystem.BLL.Interfaces
{
    public interface IAccountChangeService
    {
        Task UpdateAccountAsync(UserModel model);
        Task DeleteAccountAsync(int id);
        int GetRoleId(int userId);
    }
}
