using Newtonsoft.Json;

namespace SDC_ALTEN_API.Model
{
    public class JsonData
    {
        [JsonProperty("Data")]
        public List<Product> Data { get; set; }
    }
}
