using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    public class NewsApiConnection
    {
        //Endpoints
        static Dictionary<string, string> apiBaseUrl = new Dictionary<string, string>()
        {
            {"headlines", "https://newsapi.org/v2/top-headlines" },
            {"everything", "https://newsapi.org/v2/everything" },
            {"sources", "https://newsapi.org/v2/sources" }
        };

        static string[] possibleCategories = { "business", "entertainment", "general", "health", "science", "sports", "technology" };

        static string apiKey = "613718cb8da94c969fb7264155947045";

        public static async Task<string> LoadDataAsync(string category)
        {
            if (possibleCategories.Contains<string>(category))
            {
                string apiCall = apiBaseUrl["headlines"] + "?country=" + Properties.Settings.Default.NewsSourceCountryCode + "&category=" + category + "&apiKey=" + apiKey;
                Task<string> result;

                using (HttpClient client = new HttpClient())
                using (HttpResponseMessage response = await client.GetAsync(apiCall))
                using (HttpContent content = response.Content)
                {
                    result = content.ReadAsStringAsync();
                }
                return await result;
            }
            else
            {
                throw new System.ArgumentException("Category doesn't exist", "category");
            }
        }
    }
}
