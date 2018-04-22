using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LondonApi.Models;

namespace LondonApi.Controllers
{
    [Route("/")]
    [ApiVersion("1.0")]
    public class RootController:Controller
    {
        [HttpGet(Name =nameof(GetRoot))]
        public IActionResult GetRoot()
        {
            var response = new
            {
                href = Url.Link(nameof(GetRoot), null),
                rooms = Link.To(nameof(RoomController.GetRooms)),
                info = Link.To(nameof(InfoControllers.GetInfo))
            };

            return Ok(response);
        }
    }
}
