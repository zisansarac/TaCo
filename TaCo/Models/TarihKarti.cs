using SQLite;
namespace TaCo.Models;

public class  TarihKarti 
{
    [PrimaryKey]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string KonuBasligi { get; set; }
    public string SoruMetni { get; set; }
    public string CevapMetni { get; set; }
    public string ZorlukSeviyesi { get; set; }
    public string KartRengi { get; set; } = "#F3E5F5";
    public bool BilindiMi { get; set; } = false;
    
}