using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

public class ImageServices
{
    private readonly HttpClient _http;
    private readonly string _token;

    public ImageServices(HttpClient httpClient, IConfiguration config)
    {
        _http = httpClient;
        _token = config["HuggingFace:ApiKey"];
    }

    public async Task<string> GenerateImageAsync(string prompt)
    {
        try
        {
            var request = new { inputs = prompt };
            var json = JsonSerializer.Serialize(request);

            _http.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);

            // Hugging Face'te genelde en hızlı yanıt veren / ayakta kalan modeller
            string[] models = new[]
            {
                "black-forest-labs/FLUX.1-dev",
                "black-forest-labs/FLUX.1-schnell",
                "stabilityai/stable-diffusion-xl-base-1.0",
                "stabilityai/stable-diffusion-3.5-large"
            };

            foreach (var model in models)
            {
                try
                {
                    Console.WriteLine($"\n🔄 Deneniyor: {model}");
                    string endpoint = $"https://router.huggingface.co/hf-inference/models/{model}";

                    // ÇÖZÜM 1: StringContent her döngüde YENİDEN oluşturulmalı
                    using var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                    var response = await _http.PostAsync(endpoint, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var imageBytes = await response.Content.ReadAsByteArrayAsync();
                        var base64 = Convert.ToBase64String(imageBytes);
                        Console.WriteLine($"✅ Başarılı: {model}");
                        return $"data:image/jpeg;base64,{base64}";
                    }
                    else
                    {
                        // ÇÖZÜM 2: Hugging Face neden reddediyor, mesajı okuyalım
                        string errorDetail = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"❌ Başarısız ({response.StatusCode}): {errorDetail}");

                        // Eğer hata "Model is currently loading" (Model yükleniyor) ise, 
                        // asenkron olarak kısa bir süre bekleyip diğer modele geçmek iyi bir fikirdir.
                    }
                }
                catch (Exception innerEx)
                {
                    // Kod içinde patlayan bir şey varsa onu görelim
                    Console.WriteLine($"⚠️ İç Hata ({model}): {innerEx.Message}");
                }

                await Task.Delay(1500); // Modeller arası geçişte API'yi yormamak için biraz bekle
            }

            Console.WriteLine("❌ Tüm model denemeleri başarısız oldu.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Genel Hata: {ex.Message}");
        }

        // Başarısız olursa varsayılan resmi dön
        return "/travelin/images/placeholder-map.jpg";
    }
}