using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LondonApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LondonApi.Services
{
    public class DefaultRoomService : IRoomService
    {
        private readonly HotelApiContext _context;
        public DefaultRoomService(HotelApiContext context)
        {
            _context = context;
        }
        public async Task<Room> GetRoomAsyn(Guid id, CancellationToken ct)
        {
            var entity = await _context.Rooms.SingleOrDefaultAsync(x => x.Id == id, ct);
            if (entity == null) return null;

            return AutoMapper.Mapper.Map<Room>(entity);
        }
    }
}
