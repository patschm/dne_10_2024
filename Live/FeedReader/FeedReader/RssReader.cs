using System.Xml;
using System.Xml.Serialization;

namespace FeedReader;

public class RssReader
{
    private readonly IHttpClientFactory? _httpClientFactory;
    private HttpClient? _client;
    private XmlSerializer serializer = new XmlSerializer(typeof(Item));

    public RssReader(HttpClient client)
    {
           _client = client;
    }
    public RssReader(IHttpClientFactory httpFactory)
    {
        _httpClientFactory = httpFactory;
    }
    public async IAsyncEnumerable<Item> ReadRssAsync()
    {
        if (_httpClientFactory != null)
        {
            _client = _httpClientFactory.CreateClient("nu");
        }
        var result = await _client!.GetAsync("rss/algemeen");
        if (result.IsSuccessStatusCode)
        {
            var reader = XmlReader.Create(result.Content.ReadAsStream());
            //List<Item> items = new List<Item>();
            while (reader.ReadToFollowing("item"))
            {
                var item = ProcessItem(reader.ReadSubtree());
                if (item != null)
                    //items.Add(item);
                    yield return item;
            }
        }
    }
    private Item? ProcessItem(XmlReader xmlReader)
    {
        return serializer.Deserialize(xmlReader) as Item;
    }
}
