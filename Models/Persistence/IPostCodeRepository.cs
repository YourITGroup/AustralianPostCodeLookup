﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace PostCodeSearch.Models.Persistence
{
    public interface IPostCodeRepository
    {
        void ClearTable();
        Task<bool> ContainsData();
        Task<IEnumerable<PostCodes>> Find(string filter);
        Task<IEnumerable<PostCodes>> FindByLGA(string filter);
        Task InsertBatch(IEnumerable<PostCodes> data);
        void SavePostCode(PostCodes postCode);
    }
}