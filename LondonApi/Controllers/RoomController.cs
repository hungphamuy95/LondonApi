using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LondonApi.Models;
using LondonApi.Services;

namespace LondonApi.Controllers
{
    [Route("/[controller]")]
    public class RoomController:Controller
    {
        private readonly IRoomService _roomService;
        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }
        [HttpGet(Name =nameof(GetRooms))]
        public IActionResult GetRooms()
        {
            throw new NotImplementedException();
        }

        // /rooms/{roomId}
        [HttpGet("{roomId}", Name =nameof(GetRoomByIdAsync))]
        public async Task<IActionResult> GetRoomByIdAsync(Guid roomId, CancellationToken ct)
        {
            var room = await _roomService.GetRoomAsyn(roomId, ct);
            if (room == null) return NotFound();
            return Ok(room);
        }
    }
}
