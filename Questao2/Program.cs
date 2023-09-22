using Newtonsoft.Json;
using System.Net.Http;

public class Program
{
    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team "+ teamName +" scored "+ totalGoals.ToString() + " goals in "+ year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static int getTotalScoredGoals(string team, int year)
    {
        string baseUrl = "https://jsonmock.hackerrank.com/api/football_matches";
        int currentPage = 1;
        int goals = 0;

        using (HttpClient httpClient = new HttpClient())
        {
            while (true)
            {
                HttpResponseMessage response = httpClient.GetAsync($"{baseUrl}?team1={team}&year={year}&page={currentPage}").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<dynamic>(responseBody);

                    var resultado = result.ToObject<Resultado>();

                    List<Jogo> jogosDaPagina = result.data.ToObject<List<Jogo>>();
                    goals += jogosDaPagina.Select(x => x.team1goals).Sum();

                    if (currentPage >= resultado.total_pages)
                    {
                        break; 
                    }

                    currentPage++;
                }
                else
                {
                    Console.WriteLine("A solicitação HTTP não foi bem-sucedida.");
                    break;
                }
            }
        } 

        currentPage = 1;
        using (HttpClient httpClient = new HttpClient())
        {
            while (true)
            {
                HttpResponseMessage response = httpClient.GetAsync($"{baseUrl}?team2={team}&year={year}&page={currentPage}").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<dynamic>(responseBody);

                    var resultado = result.ToObject<Resultado>();

                    List<Jogo> jogosDaPagina = result.data.ToObject<List<Jogo>>();
                    goals += jogosDaPagina.Select(x => x.team2goals).Sum();

                    if (currentPage >= resultado.total_pages)
                    {
                        break; 
                    }

                    currentPage++;
                }
                else
                {
                    Console.WriteLine("A solicitação HTTP não foi bem-sucedida.");
                    break;
                }
            }
        }

        return goals;
    }

    class Resultado
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }
        public List<Jogo> data { get; set; }
    }

    class Jogo
    {
        public string competition { get; set; }
        public int year { get; set; }
        public string round { get; set; }
        public string team1 { get; set; }
        public string team2 { get; set; }
        public int team1goals { get; set; }
        public int team2goals { get; set; }
    }   

}