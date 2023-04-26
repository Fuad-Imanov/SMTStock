using SMTstock.Entities.DTO.CategoryDto;
using SMTstock.Entities.DTO.MerchantDto;
using SMTstock.Entities.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTstock.Services.Interfaces
{
    public interface IMerchantService
    {
        Task<IDataResult<IEnumerable<MerchantDTO>>> GetAllMerchantsWithPaginationsAsync(int page);
        Task<IDataResult<IEnumerable<MerchantDTO>>> GetAllMerchantsAsync();
        Task<IDataResult<MerchantDTO>> GetMerchantByIdAsync(int id);
        Task<IDataResult<MerchantDTO>> AddMerchantAsync(AddMerchantDTO addMerchantDTO);
        Task<IResult> UpdateMerchant(int id, MerchantDTO merchantDTO);
        Task<IResult> RemoveMerchant(int id);
    }
}
