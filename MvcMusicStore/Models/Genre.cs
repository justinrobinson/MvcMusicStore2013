using System.Collections.Generic;
using Newtonsoft.Json;

namespace MvcMusicStore.Models
{
    public partial class Genre
    {
        public int GenreId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // exclude the Albums property from JSON serialization, preventing circular references 
        [JsonIgnore]
        public List<Album> Albums { get; set; }
    }
}
