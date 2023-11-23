using Newtonsoft.Json;

namespace SDC_ALTEN_API.Model
{
    public class JsonData
    {
        [JsonProperty("data")]
        public List<Product> Data { get; set; }
    }
}
