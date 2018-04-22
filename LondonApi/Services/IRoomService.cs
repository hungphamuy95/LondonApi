using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LondonApi.Models;
using System.Threading;

namespace LondonApi.Services
{
    public interface IRoomService
    {
        Task<Room> GetRoomAsyn(Guid id, CancellationToken ct);
    }
}
