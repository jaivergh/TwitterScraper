using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace ScraperApp
{
	class Program
	{
		static async Task Main(string[] args)
		{
			var url = "https://twitter.com/Twitter";
			using (var httpClient = new HttpClient()) 
			{
				var response = await httpClient.GetAsync(url);
				if (response.IsSuccessStatusCode)
				{
					var htmlBody = await response.Content.ReadAsStringAsync();
					var htmlDocument = new HtmlDocument();
					htmlDocument.LoadHtml(htmlBody);
					var personalInfo = htmlDocument.GetElementbyId("init-data").Attributes["value"].Value;
					personalInfo = HttpUtility.HtmlDecode(personalInfo);

					dynamic jsonStuff = JObject.Parse(personalInfo);

					dynamic perfilDeUsuario = jsonStuff["profile_user"];

					Console.WriteLine("Información:");
					Console.WriteLine($"Nombre: {perfilDeUsuario.name}");
					Console.WriteLine($"Nombre en pantalla: {perfilDeUsuario.screen_name}");
					Console.WriteLine($"Localización: {perfilDeUsuario.location}");
					Console.WriteLine($"Seguidores: {perfilDeUsuario.followers_count}");
				}
			}

			Console.ReadLine();
		}
	}
}
