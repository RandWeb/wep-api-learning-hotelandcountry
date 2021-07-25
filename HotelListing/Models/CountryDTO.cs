using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Models
{
    public class CountryCommand
    {
        [Required]
        [StringLength(maximumLength:50,ErrorMessage ="Country Name Is To long")]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 2, ErrorMessage = "Short Country Name name Is To long")]
        public string ShortName { get; set; }
    }

    public class CountryDTO : CountryCommand
    {
        public int Id { get; set; }
        public IList<HotelDTO> Hotels { get; set; }
    }
}
