using TaCo.ViewModels;

namespace TaCo;

public partial class MainPage : ContentPage
{
    // YAPICI METOD: Sistem buraya TarihViewModel'i otomatik getirecek (Dependency Injection)
    public MainPage(TarihViewModel viewModel)
    {
        InitializeComponent();
        // XAML'dan sildiğimiz bağlantıyı burada manuel yapıyoruz
        BindingContext = viewModel;
    }

    // Karta tıklayınca cevabı gösterir/gizler
    private void OnKartTapped(object sender, EventArgs e)
    {
        // CevapLabel görünürlüğünü tersine çevir (Görünüyorsa gizle, gizliyse göster)
        CevapLabel.IsVisible = !CevapLabel.IsVisible;

        // Tıkla yazısını gizle
        TiklaYazisi.IsVisible = !CevapLabel.IsVisible;
    }

    // Sıradaki soruya geçince cevabı tekrar gizle
    private void OnSiradakiClicked(object sender, EventArgs e)
    {
        CevapLabel.IsVisible = false;
        TiklaYazisi.IsVisible = true;

        if (BindingContext is TarihViewModel viewModel)
        {
            viewModel.SonrakiKart();
        }
    }
    // Sayfa ekranda görününce çalışır
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Verileri veritabanından çekmek için komutu çalıştır
        if (BindingContext is TarihViewModel vm)
        {
            await vm.VerileriGetirCommand.ExecuteAsync(null);
        }
    }


}
