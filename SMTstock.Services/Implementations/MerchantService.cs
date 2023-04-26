using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SMTstock.Core.DataAccess.GenericRepository.Interfaces;
using SMTstock.Core.DataAccess.UnitOfWork.Interfaces;
using SMTstock.DAL.Context;
using SMTstock.Entities.DTO.MerchantDto;
using SMTstock.Entities.Models;
using SMTstock.Entities.Utilities.Pagination;
using SMTstock.Entities.Utilities.Results.Abstract;
using SMTstock.Entities.Utilities.Results.Concrete;
using SMTstock.Services.Interfaces;

namespace SMTstock.Services.Implementations
{
    public class MerchantService : IMerchantService
    {
        private readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Merchant> _merchantRepository;
        public MerchantService(IUnitOfWork<ApplicationDbContext> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _merchantRepository = _unitOfWork.GetRepository<Merchant>();

        }

        public async Task<IDataResult<IEnumerable<MerchantDTO>>> GetAllMerchantsWithPaginationsAsync(int page)
        {
            IQueryable<Merchant> merchants= _merchantRepository.GetAll();

            //pagination
            var totalItems = await merchants.CountAsync();
            var pageList = new PagedList(totalItems, page);
            var result = await merchants.Skip((pageList.CurrentPage - 1) * pageList.PageSize).Take(pageList.PageSize).ToListAsync();

            List<MerchantDTO> merchantsDTO = new List<MerchantDTO>();

            if (result is not null && result.Count != 0)
            {
                merchantsDTO = _mapper.Map<List<MerchantDTO>>(result);
            }

            //Create and return Response
            return (merchantsDTO is not null && merchantsDTO.Count != 0)
                ? new SuccessDataResult<IEnumerable<MerchantDTO>>(merchantsDTO,pageList, "The merchant was successfully delivered.")
                : new ErrorDataResult<IEnumerable<MerchantDTO>>(merchantsDTO, pageList, "Merchants not found.");


        }
        public async Task<IDataResult<IEnumerable<MerchantDTO>>> GetAllMerchantsAsync()
        {
            List<Merchant> merchants = _merchantRepository.GetAll().ToList();
            //mapping
            List<MerchantDTO> merchantsDTO = new List<MerchantDTO>();

            if (merchants is not null && merchants.Count != 0)
            {
                merchantsDTO = _mapper.Map<List<MerchantDTO>>(merchants);
            }

            //Create and return Response
            return (merchantsDTO is not null && merchantsDTO.Count != 0)
                ? new SuccessDataResult<IEnumerable<MerchantDTO>>(merchantsDTO, "Merchants was successfully delivered.")
                : new ErrorDataResult<IEnumerable<MerchantDTO>>(merchantsDTO, "Merchant not found.");
        }

        public async Task<IDataResult<MerchantDTO>> GetMerchantByIdAsync(int id)
        {

            var Merchant = await _merchantRepository.GetByIdAsync(id, false);
            var MerchantDTO = _mapper.Map<MerchantDTO>(Merchant);
            return (MerchantDTO is not null)
                 ? new SuccessDataResult<MerchantDTO>(MerchantDTO, "Merchant succesfully get!")
                : new ErrorDataResult<MerchantDTO>("Merchant not found");
        }

        public async Task<IDataResult<MerchantDTO>> AddMerchantAsync(AddMerchantDTO addMerchantDTO)
        {
            var Merchant = _mapper.Map<Merchant>(addMerchantDTO);
            var MerchantAdd = await _merchantRepository.AddAsync(Merchant);
            if (!MerchantAdd)
            {
                return new ErrorDataResult<MerchantDTO>("Error when add Merchant");
            }
            var save = _unitOfWork.SaveChanges();
            if (save != 0)
            {
                var createdMerchantDTO = _mapper.Map<MerchantDTO>(Merchant);
                return new SuccessDataResult<MerchantDTO>(createdMerchantDTO, "Merchant succesfully created!");
            }
            else
            {
                return new ErrorDataResult<MerchantDTO>("Merchant not created");
            }
        }


        public async Task<IResult> UpdateMerchant(int id, MerchantDTO MerchantDTO)
        {
            var Merchant = await _merchantRepository.GetByIdAsync(id, false);

            if (Merchant == null)
            {
                return new ErrorResult("Merchant not found");
            }
            Merchant = _mapper.Map<Merchant>(MerchantDTO);
            if (await _merchantRepository.Update(Merchant))
            {
                var save = _unitOfWork.SaveChanges();
                if (save != 0)
                {
                    return new SuccessResult("Merchant succesfully update");
                }
                return new ErrorResult("Merchant not update");
            }

            return new ErrorResult("Merchant not update");
        }
        public async Task<IResult> RemoveMerchant(int id)
        {

            var Merchant = await _merchantRepository.GetByIdAsync(id, false);
            if (Merchant == null)
            {
                return new ErrorResult("Merchant not found");
            }
            if (await _merchantRepository.Remove(Merchant))
            {
                var save = _unitOfWork.SaveChanges();
                if (save != 0)
                {
                    return new SuccessResult("Merchant succesfully deleted");
                }
                return new ErrorResult("Merchant not deleted");
            }
            return new ErrorResult("Merchant not deleted");

        }

       
    }
}
