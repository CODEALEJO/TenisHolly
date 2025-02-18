using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TenisHolly.DTOs;

namespace TenisHolly.Helpers
{
    public static class JsonHelper
    {
        private static readonly JsonSerializerOptions Options = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public static List<SizeDetailDTO> ParseSizes(string jsonSizes)
        {
            try
            {
                if (string.IsNullOrEmpty(jsonSizes))
                {
                    return new List<SizeDetailDTO>();
                }

                return JsonSerializer.Deserialize<List<SizeDetailDTO>>(jsonSizes, Options) 
                    ?? new List<SizeDetailDTO>();
            }
            catch (JsonException)
            {
                // Si el JSON no es válido, intentamos parsear el formato antiguo
                return ParseLegacySizes(jsonSizes);
            }
        }

        private static List<SizeDetailDTO> ParseLegacySizes(string sizes)
        {
            // Maneja el formato antiguo de tallas (si existe)
            var sizesList = new List<SizeDetailDTO>();
            
            if (!string.IsNullOrEmpty(sizes))
            {
                var sizeArray = sizes.Split(',', StringSplitOptions.RemoveEmptyEntries);
                foreach (var size in sizeArray)
                {
                    sizesList.Add(new SizeDetailDTO
                    {
                        Sizes = size.Trim(),
                        Quantity = 1,
                        Gender = "Unspecified"
                    });
                }
            }

            return sizesList;
        }
    }
}