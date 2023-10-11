// See https://aka.ms/new-console-template for more information

using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

string token   = "";
string urlBase = "https://localhost:7078/api/";

//Obtem o token de autenticação
using (var client = new HttpClient())
{
    client.BaseAddress = new Uri(urlBase);

    string json = JsonConvert.SerializeObject(new { login = "admin", password = "admin" });

    var body = new StringContent(json, Encoding.UTF8, "application/json");

    var resposta = await client.PostAsync("Auth/login", body);
    var mensagem = resposta.Content.ReadAsStringAsync().Result;

    if (resposta.IsSuccessStatusCode)
    {
        token = mensagem;
        Console.WriteLine($"token: {token}");
    }
    else
    {
        Console.WriteLine($"Erro ao obter o token: {mensagem}");
    }
}

//Realizando o GET
using (var client = new HttpClient())
{
    client.BaseAddress = new Uri(urlBase);

    client.DefaultRequestHeaders.Authorization =
                       new AuthenticationHeaderValue("Bearer", token);

    var resposta = await client.GetAsync("Produto/produto");
    var mensagem = resposta.Content.ReadAsStringAsync().Result;

    if (resposta.IsSuccessStatusCode)
    {
        List<string> lista = JsonConvert.DeserializeObject<List<string>>(mensagem);

        foreach (string produto in lista)
        {
            Console.WriteLine(produto);
        }
    }
    else
    {
        Console.WriteLine($"Erro ao realizar o GET: {mensagem}");
    }
}


//Realizando o POST
using (var client = new HttpClient())
{
    client.BaseAddress = new Uri(urlBase);

    client.DefaultRequestHeaders.Authorization =
                       new AuthenticationHeaderValue("Bearer", token);

    string json = JsonConvert.SerializeObject(new { nome = "Produto6" });

    var body = new StringContent(json, Encoding.UTF8, "application/json");

    var resposta = await client.PostAsync("Produto/produto", body);
    var mensagem = resposta.Content.ReadAsStringAsync().Result;

    if (resposta.IsSuccessStatusCode)
    {
        List<string> lista = JsonConvert.DeserializeObject<List<string>>(mensagem);

        foreach (string produto in lista)
        {
            Console.WriteLine(produto);
        }
    }
    else
    {
        Console.WriteLine($"Erro ao realizar o POST: {mensagem}");
    }
}


//Realizando o PUT
using (var client = new HttpClient())
{
    client.BaseAddress = new Uri(urlBase);

    client.DefaultRequestHeaders.Authorization =
                       new AuthenticationHeaderValue("Bearer", token);

    string json = JsonConvert.SerializeObject(new { nomeAtual = "Produto2", nomeNovo = "Produto123" });

    var body = new StringContent(json, Encoding.UTF8, "application/json");

    var resposta = await client.PutAsync("Produto/produto", body);
    var mensagem = resposta.Content.ReadAsStringAsync().Result;

    if (resposta.IsSuccessStatusCode)
    {
        List<string> lista = JsonConvert.DeserializeObject<List<string>>(mensagem);

        foreach (string produto in lista)
        {
            Console.WriteLine(produto);
        }
    }
    else
    {
        Console.WriteLine($"Erro ao realizar o PUT: {mensagem}");
    }
}


//Realizando o DELETE
using (var client = new HttpClient())
{
    client.BaseAddress = new Uri(urlBase);

    client.DefaultRequestHeaders.Authorization =
                       new AuthenticationHeaderValue("Bearer", token);

    var resposta = await client.DeleteAsync("Produto/produto?nome=Produto3");
    var mensagem = resposta.Content.ReadAsStringAsync().Result;

    if (resposta.IsSuccessStatusCode)
    {
        List<string> lista = JsonConvert.DeserializeObject<List<string>>(mensagem);

        foreach (string produto in lista)
        {
            Console.WriteLine(produto);
        }
    }
    else
    {
        Console.WriteLine($"Erro ao realizar o DELETE: {mensagem}");
    }
}




















Console.WriteLine("Pressione qualquer tecla para encerrar...");
Console.ReadKey();
