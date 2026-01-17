using SQLite;
using TaCo.Models;

namespace TaCo.Services;

public class DatabaseService
{
    //veritabanı bağlantısı
    SQLiteAsyncConnection _database;

    // veritabanını başlatan metod(tablo yoksa oluşturacak)
    async Task Init()
    {
        if (_database is not null)
            return;

        //veritabanı mobildeki yolu
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "TaCo.db3");

        _database = new SQLiteAsyncConnection(dbPath);

        //TarihKarti tablosunu oluştur(zaten varsa bir şey yapmaz)
        await _database.CreateTableAsync<TarihKarti>();
    }

    // 1.Listeleme: Tüm Kartları Getir
    public async Task<List<TarihKarti>> KartlariGetir()
    {
        await Init();
        return await _database.Table<TarihKarti>().ToListAsync();

    }

    // 2.Ekleme ve Güncelleme: Kartı kaydet

    public async Task Kaydet(TarihKarti kart)
    {
        await Init();

        // kart zaten veritabanında varsa güncelle, yoksa yeni ekle
        var mevcutKart = await _database.Table<TarihKarti>().Where(x => x.Id == kart.Id).FirstOrDefaultAsync();
        
        if (mevcutKart != null)
        {
            await _database.UpdateAsync(kart);
        }
        else
        {
            await _database.InsertAsync(kart);
        }
    }

    //3.Silme: Kartı sil

    public async Task Sil(TarihKarti kart)
    {
        await Init();
        await _database.DeleteAsync(kart);
    }
}

