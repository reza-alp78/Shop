using Core.Domain.Entity.Products;
using Dapper;
using Infrastructure.DataBaseContext;
using Infrastructure.Interfaces.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Poducts
{
    public class ImagesService : IImages
    {


        #region constructor

        private readonly ShopDbContext _db;
        private readonly DapperContext _dapper;

        public ImagesService(ShopDbContext shopDbContext, DapperContext context)
        {
            _db = shopDbContext;
            _dapper = context;
        }

        #endregion

        public async Task<Images> AddImages(Images images)
        {
            await _db.Images.AddAsync(images);
            return images;
        }

        public async Task<List<Images>> GetAllImages()
        {
            var query = "SELECT * FROM Images";
            using (var connection = _dapper.CreateConnection())
            {
                var images = await connection.QueryAsync<Images>(query);
                return images.ToList();
            }
        }

        public async Task<List<Images>> GetImagesByProductId(long productId)
        {
            var query = $"SELECT * FROM Images WHERE ProductId = {productId}";
            using (var connection = _dapper.CreateConnection())
            {
                var images = await connection.QueryAsync<Images>(query);
                return images.ToList();
            }
        }

        public Images UpdateImages(Images images)
        {
            _db.Entry(images).State = EntityState.Modified;
            return images;
        }

        public void DeleteImages(Images images)
        {
            _db.Entry(images).State = EntityState.Deleted;
        }

    }
}
