using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using TaCo.Models;
using TaCo.Services;
using CommunityToolkit.Mvvm.Input;

namespace TaCo.ViewModels;


public partial class TarihViewModel : ObservableObject
{
    // Servisimize buradan ulaşacağız
    private readonly Services.DatabaseService _databaseService;

    [ObservableProperty]
    ObservableCollection<TarihKarti> kartlar;

    [ObservableProperty]
    TarihKarti aktifKart;

    private int _siradakiIndex = 0;

    // CONSTRUCTOR (Yapıcı Metod)
    // MauiProgram.cs dosyasında tanımladığımız için sistem buraya servisi otomatik gönderir.
    public TarihViewModel(DatabaseService databaseService)
    {
        _databaseService = databaseService; // Gelen servisi içeri aldık
        Kartlar = new ObservableCollection<TarihKarti>();

        //Verileri burada yüklemiyoruz çünkü 'async' işlemler constructor'da yapılmaz.
        // verileri asenkron (async) yükleyeceğiz, o yüzden burada çağırmıyoruz.

    }

    // Sayfa açılınca bu komutu çalıştıracağız: Verileri Çekme Komutu
    [RelayCommand]
    public async Task VerileriGetir()
    {
        //veritabanından listeyi iste
        var gelenVeriler = await _databaseService.KartlariGetir();

        // Eğer veritabanı boşsa (İlk açılış), örnek veri ekleyelim ki boş görünmesin
        if (gelenVeriler.Count == 0)
        {
            var ornekKart = new TarihKarti
            {
                KonuBasligi = "Örnek",
                SoruMetni = "İlk kartını eklemek için + butonuna basabilirsin!",
                CevapMetni = "Hoşgeldin",
                KartRengi = "#E0E0E0"
            };
            await _databaseService.Kaydet(ornekKart);
            gelenVeriler = await _databaseService.KartlariGetir();
        }

        // 3. Listemizi temizle ve gelen verileri ekle
        Kartlar.Clear();
        foreach (var kart in gelenVeriler)
        {
            Kartlar.Add(kart);
        }

        // 4. İlk kartı ekranda göster
        if (Kartlar.Count > 0)
            AktifKart = Kartlar[0];
    }

    public void SonrakiKart()
    {
        if (Kartlar.Count == 0) return;

        _siradakiIndex++;

        //son karta gelince başa dönelim
        if (_siradakiIndex >= Kartlar.Count)
            _siradakiIndex = 0;

        AktifKart = Kartlar[_siradakiIndex];
    }

    /*
    void VerileriYukle()
    {
        //Örnek veriler
        Kartlar.Add(new TarihKarti
        {
            KonuBasligi = "İslamiyet Öncesi",
            SoruMetni = "Türk adının anlamı Çin kaynaklarında ne olarak geçer?",
            CevapMetni = "Miğfer",
            KartRengi = "#E1BEE7"
        });

        Kartlar.Add(new TarihKarti
        {
            KonuBasligi = "Osmanlı Yükselme",
            SoruMetni = "Preveze Deniz Savaşı hangi padişah dönemindedir?",
            CevapMetni = "Kanuni Sultan Süleyman",
            KartRengi = "#C5CAE9"
        });

        Kartlar.Add(new TarihKarti
        {
            KonuBasligi = "İnkılap Tarihi",
            SoruMetni = "Mustafa Kemal'in 'Geldikleri gibi giderler' sözünü nerede söylemiştir?",
            CevapMetni = "İstanbul Boğazı'nda (Yaveri Cevat Abbas'a)",
            KartRengi = "#FFCCBC"
        });
    }
    */

    

}
