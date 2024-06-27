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

        public async Task<ShopDto> CreateShop(CreateShopRequest request)
        {
            var shop = _mapper.Map<Shop>(request);

            _context.Shops.Add(shop);

            await _context.SaveChangesAsync();

            return _mapper.Map<ShopDto>(shop);
        }

        public async Task<ShopDto> DeleteShopById(int id)
        {
            var shop = await _context.Shops.FindAsync(id);

            _context.Shops.Remove(shop);

            await _context.SaveChangesAsync();

            return _mapper.Map<ShopDto>(shop);
        }

        public async Task<ListShopDto> GetAllAsync()
        {
            List<Shop> shop = await _context.Shops.ToListAsync();
            
            ListShopDto listShopDto = new ListShopDto()
            {
                shopList = _mapper.Map<List<ShopDto>>(shop)
            };

            return listShopDto;
        }

        public async Task<ShopDto> GetByIdAsync(int id)
        {
            var shop = await _context.Shops.Where(s => s.Id == id).FirstOrDefaultAsync();
            
            return _mapper.Map<ShopDto>(shop);
        }

        public async Task<ShopDto> GetByNameAsync(string name)
        {
            var shop = await _context.Shops.Where(s => s.Name.Equals(name)).FirstOrDefaultAsync();
            
            return _mapper.Map<ShopDto>(shop);
        }

        public async Task<ShopDto> UpdateShop(int id, UpdateShopRequest request)
        {
            var shop = await _context.Shops.FindAsync(id);

            shop.Name= request.Name ?? shop.Name;
            shop.Location=request.Location ?? shop.Location;
            shop.Employees=request.Employees ?? shop.Employees;

            _context.Shops.Update(shop);

            await _context.SaveChangesAsync();

            return _mapper.Map<ShopDto>(shop);
        }
    }
}
