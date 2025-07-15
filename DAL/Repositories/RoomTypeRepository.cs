using DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class RoomTypeRepository
    {
        private readonly FuminiHotelManagementContext _context;
        public RoomTypeRepository(FuminiHotelManagementContext context)
        {
            _context = context;
        }

        public IEnumerable<RoomType> GetAll()
        {
            return _context.RoomTypes.ToList();
        }
    }
} 