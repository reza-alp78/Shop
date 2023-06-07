using Core.Domain.Entity.CategoriesAndProducts;
using Dapper;
using Infrastructure.DataBaseContext;
using Infrastructure.Interfaces.CategoriesAndProducts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.CategoriesAndProducts
{
    public class ProductRegisterService : IProductRegister
    {

        #region constructor

        private readonly ShopDbContext _db;
        private readonly DapperContext _dapper;

        public ProductRegisterService(ShopDbContext shopDbContext, DapperContext context)
        {
            _db = shopDbContext;
            _dapper = context;
        }

        #endregion

        public async Task<ProductRegister> AddProductRegister(ProductRegister productRegister)
        {
            await _db.ProductRegisters.AddAsync(productRegister);
            return productRegister;
        }

        public ProductRegister UpdateCategoryProductProperty(ProductRegister productRegister)
        {
            _db.Entry(productRegister).State = EntityState.Modified;
            return productRegister;
        }

        public void DeleteCategoryProductProperty(ProductRegister productRegister)
        {
            _db.Entry(productRegister).State = EntityState.Deleted;
        }
       
    }
}
