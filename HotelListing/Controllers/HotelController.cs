using AutoMapper;
using HotelListing.Models;
using HotelListing.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HotelController> _logger;
        private readonly IMapper mapper;

        public HotelController(IUnitOfWork unitOfWork, ILogger<HotelController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetHotels()
        {
            try
            {
                var hotels = await _unitOfWork.Hotels.GetAll();
                var results = mapper.Map<IList<HotelDTO>>(hotels);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Somthing Went Wrong in the {nameof(GetHotels)}");
                return BadRequest(ex);
                // return StatusCode(500,"Internal Server Error, Pease Try Again Later");

            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetHotel(int id)
        {
            try
            {
                var hotel = await _unitOfWork.Hotels.Get(q=>q.Id==id);
                var result = mapper.Map<HotelDTO>(hotel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Somthing Went Wrong in the {nameof(GetHotels)}");
                return BadRequest(ex);
                // return StatusCode(500,"Internal Server Error, Pease Try Again Later");

            }
        }
    }
}
