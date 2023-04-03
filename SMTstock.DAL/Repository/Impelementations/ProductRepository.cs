using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SMTstock.Core.DataAccess;
using SMTstock.Core.DataAccess.GenericRepository.Impelementations;
using SMTstock.Core.DataAccess.GenericRepository.Interfaces;
using SMTstock.Core.DataAccess.UnitOfWork.Interfaces;
using SMTstock.DAL.Context;
using SMTstock.DAL.Repository.Interfaces;
using SMTstock.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTstock.DAL.Repository.Impelementations
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }


    }

}
