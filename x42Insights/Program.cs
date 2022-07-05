using System.ComponentModel;
using System.Text.Json;
using x42Insights;

 
var client = new InsightsRestClient();

var richlist = new List<RichModel>();
var lastStakedList = new List<LastStakedModel>();

var limit = 100;

for (int i = 0; i < 11; i++)
{
    var deposits = await client.GetRichList(limit, i * limit);
    richlist.AddRange(deposits);

}

limit = 50;

var itemnumber = 0;

foreach (var item in richlist)
{
    Console.WriteLine(itemnumber++);
 
       var transactions = await client.GetLast(item.Address, limit, 0);

       var lastStaked = transactions.OrderBy(l => l.Confirmations).FirstOrDefault();

        if (lastStaked != null || transactions.Count == 0)
        {
            if (transactions.Count > 0) {
                lastStakedList.Add(new LastStakedModel() { Address = item.Address, Balance = item.Balance / 100000000, Confirmations = lastStaked.Confirmations });
            }
       }
         
    
}

string json = JsonSerializer.Serialize(lastStakedList);
File.WriteAllText(@"D:\tmp\insights.json", json);

var lines = new List<string>();
IEnumerable<PropertyDescriptor> props = TypeDescriptor.GetProperties(typeof(LastStakedModel)).OfType<PropertyDescriptor>();
var header = string.Join(",", props.ToList().Select(x => x.Name));
lines.Add(header);
var valueLines = lastStakedList.Select(row => string.Join(",", header.Split(',').Select(a => row.GetType().GetProperty(a).GetValue(row, null))));
lines.AddRange(valueLines);
File.WriteAllLines(@"D:\tmp\insights.csv", lines.ToArray());


Console.WriteLine(lastStakedList.Count);
