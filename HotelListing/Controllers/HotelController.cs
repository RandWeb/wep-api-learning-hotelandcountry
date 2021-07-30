using AutoMapper;
using HotelListing.Data.Entities;
using HotelListing.Models;
using HotelListing.Repository;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IMapper _mapper;

        public HotelController(IUnitOfWork unitOfWork, ILogger<HotelController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetHotels()
        {
            try
            {
                var hotels = await _unitOfWork.Hotels.GetAll(null,null,new List<string> { "Country",});
                var results = _mapper.Map<IList<HotelDTO>>(hotels);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Somthing Went Wrong in the {nameof(GetHotels)}");
                return BadRequest(ex);
                // return StatusCode(500,"Internal Server Error, Pease Try Again Later");

            }
        }
        [HttpGet("{id:int}",Name = "GetHotel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetHotel(int id)
        {
            try
            {
                var hotel = await _unitOfWork.Hotels.Get(q => q.Id == id, new List<string> { "Country" });
                var result = _mapper.Map<HotelDTO>(hotel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Somthing Went Wrong in the {nameof(GetHotels)}");
                return BadRequest(ex);
                // return StatusCode(500,"Internal Server Error, Pease Try Again Later");

            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateHotel([FromBody] CreateHotelDTO hotelDto)
        {
            try
            {
                var hotel = _mapper.Map<Hotel>(hotelDto);
                await _unitOfWork.Hotels.Insert(hotel);
                await _unitOfWork.Save();
                return CreatedAtRoute("GetHotel", new { id = hotel.Id },hotel);
                    }
            catch (Exception ex) 
            {
                _logger.LogError(ex, $"Sothing Went Wrong in the {nameof(CreateHotel)}");
                return StatusCode(500,$"Internal Server Error, Please Try Again Later {ex}");
                throw;
            }
        }
        [Authorize]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> UpdateHotel(int id,[FromBody] UpdateHotelDTO hotelDto)
        {
            try
            {
                var hotel = await _unitOfWork.Hotels.Get(q => q.Id == id);
                if (hotel == null)
                    return BadRequest("Submitted Data is invalid");

                _mapper.Map(hotelDto,hotel);
                _unitOfWork.Hotels.Update(hotel);
                await _unitOfWork.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Sothing Went Wrong in the {nameof(UpdateHotel)}");
                return StatusCode(500, $"Internal Server Error, Please Try Again Later {ex}");
            }
        } 
        [Authorize]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> DeleteHotel(int id)
        {
            if (id < 0) return BadRequest();
            try
            {
                var hotel = await _unitOfWork.Hotels.Get(q => q.Id == id);
                if (hotel == null)
                    return BadRequest("Submitted Data is invalid");

                await _unitOfWork.Hotels.Delete(id);
                await _unitOfWork.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Sothing Went Wrong in the {nameof(DeleteHotel)}");
                return StatusCode(500, $"Internal Server Error, Please Try Again Later {ex}");
            }
        }
    }
}
