using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using LondonApi.Models;

namespace LondonApi.Controllers
{
    [Route("/[controller]")]
    public class InfoControllers:Controller
    {
        private readonly HotelInfo _hotelInfo;
        public InfoControllers(IOptions<HotelInfo> hotelInfoAccessor)
        {
            _hotelInfo = hotelInfoAccessor.Value;
        }
        [HttpGet(Name =nameof(GetInfo))]
        public IActionResult GetInfo()
        {
            _hotelInfo.Href = Url.Link(nameof(GetInfo), null);
            return Ok(_hotelInfo);
        }
    }
}
