# 🌍 Travelin - Modern Tur ve Rezervasyon Yönetim Sistemi

Travelin, seyahat acenteleri ve tur operatörleri için geliştirilmiş, yüksek performanslı ve modern bir tam donanımlı (Full-Stack) web uygulamasıdır. Kullanıcı dostu arayüzü ve güçlü yönetim paneli ile turları, müşteri rezervasyonlarını ve geri bildirimleri tek bir merkezden kolayca yönetmeyi sağlar.

## ✨ Öne Çıkan Özellikler

* **Gelişmiş Yönetim Paneli:** Tailwind CSS ile tasarlanmış modern, responsive ve kullanıcı dostu Admin arayüzü.
* **Dinamik Rezervasyon Sistemi:** Müşteri rezervasyonlarını durumlarına göre (Onaylandı, Bekliyor, Reddedildi) filtreleme ve yönetme.
* **Detaylı Yorum ve Skor Moderasyonu:** Kullanıcıların Rehber, Program, Fiyat/Performans gibi alt kategorilerde verdiği oyları ve yorumları yayına alma/kaldırma (Soft Delete/Status Toggle) işlemleri.
* **Yapay Zeka Destekli Harita Üretimi:** Hugging Face API (FLUX/SDXL) ve C# ImageSharp kütüphaneleri kullanılarak, tur rotalarına özel "Hazine Haritası" konseptinde dinamik görseller oluşturma ve üzerine tipografik rotalar yazdırma.
* **MongoDB NoSQL Altyapısı:** Yüksek veri trafiğini kaldırabilen, esnek doküman tabanlı veritabanı mimarisi.
* **Çoklu Dil Desteği (Localization):** Uygulama, global bir kitleye hitap edebilmek için 5 farklı dil seçeneği ile entegre çalışır. Kullanıcıların tercihine göre arayüz anında dinamik olarak değişir.
* **Email Doğrulama — MailKit & MimeKit ile SMTP üzerinden rezervasyon bilgileri ve detayları gönderimi.
* ** Excel’e aktarım özelliği (xlsx, file-saver)

## 🛠️ Kullanılan Teknolojiler

**Backend (Sunucu Tarafı):**
* C# & ASP.NET Core MVC
* MongoDB (NoSQL Database) & MongoDB Driver
* AutoMapper (DTO eşleştirmeleri için)
* SixLabors.ImageSharp (Dinamik resim ve metin işleme)
* N-Tier Architecture (Katmanlı Mimari)
* Çoklu Dil Desteği (Localization)
* MailKit & MimeKit — Email gönderimi ve MIME işlemleri
* Excel’e aktarım özelliği (xlsx, file-saver)

**Frontend (Kullanıcı Arayüzü):**
* HTML5 / CSS3 / JavaScript
* Tailwind CSS (Modern UI/UX tasarımı)
* Razor View Engine (`.cshtml`)
* Google Material Symbols

**3. Parti Servisler:**
* Hugging Face Inference API (AI Image Generation)

## 🚀 Kurulum ve Çalıştırma

Projeyi yerel makinenizde (localhost) çalıştırmak için aşağıdaki adımları izleyin:

1.  **Projeyi Klonlayın:**
    ```bash
    git clone [https://github.com/KULLANICI_ADIN/Project3Travelin.git](https://github.com/KULLANICI_ADIN/Project3Travelin.git)
    ```

2.  **Bağımlılıkları Yükleyin:**
    Visual Studio üzerinden veya terminal kullanarak NuGet paketlerini restore edin:
    ```bash
    dotnet restore
    ```

3.  **Veritabanı Ayarları (appsettings.json):**
    `appsettings.json` dosyanızın içine MongoDB bağlantı dizenizi ve Hugging Face API anahtarınızı ekleyin:
    ```json
    {
      "ConnectionStrings": {
        "MongoDbConnection": "mongodb://localhost:27017"
      },
      "DatabaseSettings": {
        "DatabaseName": "TravelinDb"
      },
      "HuggingFace": {
        "ApiKey": "SENIN_API_ANAHTARIN"
      }
    }
    ```

4.  **Projeyi Başlatın:**
    Visual Studio üzerinden `F5`'e basarak veya CLI üzerinden aşağıdaki komutla projeyi ayağa kaldırın:
    ```bash
    dotnet run
    ```

## 📂 Proje Mimarisi

Proje, Sürdürülebilirlik (Maintainability) ve Temiz Kod (Clean Code) prensiplerine uygun olarak Katmanlı Mimari ile geliştirilmiştir:

* **Controllers:** Gelen HTTP isteklerini karşılar ve View'lara veri taşır.
* **Services:** İş kurallarının (Business Logic) ve veritabanı işlemlerinin yürütüldüğü katmandır.
* **Models/Entities:** MongoDB koleksiyonlarının C# tarafındaki karşılıklarıdır.
* **DTOs (Data Transfer Objects):** Sadece ihtiyaç duyulan verilerin (Örn: `ResultBookingDto`) taşınmasını ve güvenliği sağlar.
* **Views:** Tailwind ile güçlendirilmiş, kullanıcıya sunulan Razor arayüzleridir.

## 📸 Ekran Görüntüleri

<img width="800"  alt="Image" src="https://github.com/user-attachments/assets/455076fe-960a-4389-8c8d-7537ee97308d" />

<img width="800" alt="Image" src="https://github.com/user-attachments/assets/3247cd16-b2d1-4cb0-9753-96d35e9b212e" />

<img width="800" alt="Image" src="https://github.com/user-attachments/assets/4bc28fc5-f9da-42b2-9d97-6fabc842dd5b" />

<img width="800" alt="Image" src="https://github.com/user-attachments/assets/c0c75e33-618f-40c3-86e0-22e48e282845" />

<img width="800" alt="Image" src="https://github.com/user-attachments/assets/90adfcdc-98d7-4a43-902c-5f49b3b665c1" />

<img width="800" alt="Image" src="https://github.com/user-attachments/assets/4d6d9aa0-923b-4a73-8eb9-df9bd684e936" />

<img width="800" alt="Image" src="https://github.com/user-attachments/assets/b9e29c31-ec76-411c-b3f7-b6d800168584" />

<img width="800" alt="Image" src="https://github.com/user-attachments/assets/e00bc942-ddfe-460b-91a7-e7db143101f9" />


## `Yönetim Paneli `

<img width="800"   alt="Image" src="https://github.com/user-attachments/assets/a0bf694a-e1dc-4c2b-9f64-fcc04c94e8dd" />

<img width="800"   alt="Image" src="https://github.com/user-attachments/assets/9532843f-874f-4799-9378-69c7a7c33050" />

<img width="800"   alt="Image" src="https://github.com/user-attachments/assets/a239ef39-ac95-471c-bbd2-0ec88316e621" />

<img width="800"   alt="Image" src="https://github.com/user-attachments/assets/d41cedfc-e12d-4207-bf39-704aa1336657" />

<img width="800"   alt="Image" src="https://github.com/user-attachments/assets/ce57762d-bb3c-416f-90c8-e67df4d0c08d" />

<img width="800"   alt="Image" src="https://github.com/user-attachments/assets/9a2ef959-8741-4a99-95ec-2ae196c21c31" />

<img width="800"   alt="Image" src="https://github.com/user-attachments/assets/7a38d2fb-2550-4c18-81fd-75818fc455e8" />

<img width="800"   alt="Image" src="https://github.com/user-attachments/assets/e60641d0-e2b0-4927-9df5-3c1ae6d2332b" />

<img width="800"   alt="Image" src="https://github.com/user-attachments/assets/a9cc61f7-43fc-4f87-942b-5f639417c621" />

---

## 👨‍💻 Geliştirici

**Özkan Yılmaz**
* .NET Backend & Full-Stack Developer
* LinkedIn: [linkedin.com/in/devozkanyilmaz/](https://linkedin.com/in/devozkanyilmaz/)