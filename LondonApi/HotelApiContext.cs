using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LondonApi.Models;

namespace LondonApi
{
    public class HotelApiContext:DbContext
    {
        public HotelApiContext(DbContextOptions<HotelApiContext> options):base(options)
        {

        }
        public DbSet<RoomEntity> Rooms { get; set; }
    }
}
