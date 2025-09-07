using Newtonsoft.Json;

public class BookResponse
{
    [JsonProperty("books")]
    public List<BookItem> Books { get; set; }
}

public class BookItem
{
    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("subtitle")]
    public string Subtitle { get; set; }

    [JsonProperty("isbn13")]
    public string ISBN13 { get; set; }

    [JsonProperty("price")]
    public string Price { get; set; }

    [JsonProperty("image")]
    public string Image { get; set; }

    [JsonProperty("url")]
    public string Url { get; set; }

    [JsonProperty("authors")]
    public string Authors { get; set; }

    [JsonProperty("publisher")]
    public string Publisher { get; set; }

    [JsonProperty("year")]
    public string Year { get; set; }

    [JsonProperty("desc")]
    public string Desc { get; set; }
}
