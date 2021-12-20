using System;
using System.Net.Http;
using System.Threading.Tasks;

using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace ConsoleApp2 {
	class Program {
		static async Task Main(string[] args)
		{
			Console.WriteLine("Hello World!");

			string usersInfo;
			using (var client = new HttpClient())
			{
				var result = await client.GetAsync(@"https://gocchandesu-back.azurewebsites.net/api/GetUsers?group_id=4ba0ca6f-246a-4e0c-ba05-adb73624110c");
				usersInfo = result.Content.ReadAsStringAsync().Result;
				var aaa = new HttpResponseMessage() {
					Content = new StringContent(usersInfo, System.Text.Encoding.UTF8, "application/json")
				};

				string jsonString = JToken.Parse(aaa.Content.ReadAsStringAsync().Result).ToString();

				List<Dictionary<string, object>> jsonDictinary = new List<Dictionary<string, object>>();
				var weatherForecast = JArray.Parse(jsonString);
				foreach (var childToken in weatherForecast.Children())
				{
					var personDic = new Dictionary<string, object>();
					foreach(JProperty grandChild in childToken.Children()) 
					{
						personDic.Add(grandChild.Name.ToString(), grandChild.Value.ToString());
					}
					jsonDictinary.Add(personDic);
				}
			}
		

		}

	
	}
}
