﻿using STAGGI_Budget_API.Models;

namespace STAGGI_Budget_API.Repositories.Interfaces
{
    public interface ISavingRepository
    {
        IEnumerable<Saving> GetAllByEmail(string email);
        Saving? GetById(int id);
        void Save(Saving saving);
    }
}
