using System.Collections.Generic;

namespace SuitcaseApi.DTO.Responses
{
    public class SuitcaseDetailsResponseDto
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public List<ItemResponseDto> Items { get; set; }
    }
}