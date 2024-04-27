using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopCrudApi.Data;
using ShopCrudApi.Dto;
using ShopCrudApi.Shops.Model;
using ShopCrudApi.Shops.Repository.interfaces;

namespace ShopCrudApi.Shops.Repository
{
    public class ShopRepository: IShopRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ShopRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Shop> CreateShop(CreateShopRequest request)
        {
            var shop = _mapper.Map<Shop>(request);

            _context.Shops.Add(shop);

            await _context.SaveChangesAsync();

            return shop;
        }

        public async Task<Shop> DeleteShopById(int id)
        {
            var shop = await _context.Shops.FindAsync(id);

            _context.Shops.Remove(shop);

            await _context.SaveChangesAsync();

            return shop;
        }

        public async Task<IEnumerable<Shop>> GetAllAsync()
        {
            return await _context.Shops.ToListAsync();
        }

        public async Task<Shop> GetByIdAsync(int id)
        {
            return await _context.Shops.FirstOrDefaultAsync(obj => obj.Id.Equals(id));
        }

        public async Task<Shop> GetByNameAsync(string name)
        {
            return await _context.Shops.FirstOrDefaultAsync(obj => obj.Name.Equals(name));
        }

        public async Task<Shop> UpdateShop(int id, UpdateShopRequest request)
        {
            var shop = await _context.Shops.FindAsync(id);

            shop.Name= request.Name ?? shop.Name;
            shop.Location=request.Location ?? shop.Location;
            shop.Employees=request.Employees ?? shop.Employees;

            _context.Shops.Update(shop);

            await _context.SaveChangesAsync();

            return shop;
        }
    }
}
