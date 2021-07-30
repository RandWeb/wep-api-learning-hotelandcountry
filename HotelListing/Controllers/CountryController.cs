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
using System.Threading.Tasks;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class CountryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CountryController> _logger;
        private readonly IMapper _mapper;

        public CountryController(IUnitOfWork unitOfWork, ILogger<CountryController> logger, IMapper mapper = null)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountries([FromQuery] RequestParams requestParams)
        {
            try
            {
                var countries = await _unitOfWork.Countries.GetPagedList(requestParams);
                var results = _mapper.Map<IList<CountryDTO>>(countries);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Somthing Went Wrong in the {nameof(GetCountries)}");
                return BadRequest(ex);
                // return StatusCode(500,"Internal Server Error, Pease Try Again Later");
            }
        }
        [HttpGet("{id:int}",Name ="GetCountry")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountry(int id)
        {
            try
            {
                var country = await _unitOfWork.Countries.Get(q => q.Id == id, new List<string> { "Hotels" });
                var result = _mapper.Map<CountryDTO>(country);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Somthing Went Wrong in the {nameof(GetCountries)}");
                return BadRequest(ex);
                // return StatusCode(500,"Internal Server Error, Pease Try Again Later");
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCountry([FromBody] CreateCountryDTO createCountryDto)
        {
            try
            {
                var country = _mapper.Map<Country>(createCountryDto);
                await _unitOfWork.Countries.Insert(country);
                await _unitOfWork.Save();
                return CreatedAtRoute("GetCountry",new {id=country.Id },country);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Sothing Went Wrong in the {nameof(CreateCountry)}");
                return StatusCode(500, $"Internal Server Error, Please Try Again Later {ex}");
                throw;
            }
        }

        [Authorize]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCountry(int id,[FromBody] UpdateCountryDTO countryDTO)
        {
            try
            {
                var country = await _unitOfWork.Countries.Get(q=>q.Id==id);
                if (country == null)
                {
                    return BadRequest("Submitted Data is invalid");
                }
                _mapper.Map(countryDTO, country);
                _unitOfWork.Countries.Update(country);
                await _unitOfWork.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Sothing Went Wrong in the {nameof(UpdateCountry)}");
                return StatusCode(500, $"Internal Server Error, Please Try Again Later {ex}");
            }
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> DeleteCountry(int id)
        {
            if (id < 0) return BadRequest();
            try
            {
                var country = await _unitOfWork.Hotels.Get(q => q.Id == id);
                if (country == null)
                    return BadRequest("Submitted Data is invalid");

                await _unitOfWork.Countries.Delete(id);
                await _unitOfWork.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Sothing Went Wrong in the {nameof(DeleteCountry)}");
                return StatusCode(500, $"Internal Server Error, Please Try Again Later {ex}");
            }
        }

    }
}
