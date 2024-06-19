// See https://aka.ms/new-console-template for more information
using Email;
using Microsoft.Office.Interop.Outlook;
using System.Text;

Console.WriteLine("Příprava poslaní email!");

string ZAKLAD = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pomoc");


if (!Directory.Exists(ZAKLAD))
    Directory.CreateDirectory(ZAKLAD);

string CESTA = "Cesty.txt";
string PathCesta = Path.Combine(ZAKLAD, CESTA);
var CestySoubory = new Soubor().Cesty(PathCesta);

string EMAIL = "Email.txt";
string PathEmail = Path.Combine(ZAKLAD, EMAIL);
var LinkyEmaily = new Soubor().CestyEmail(PathEmail);

foreach (var item in CestySoubory)
{
    var Pole = new Soubor().GetAllFilesInFolder(item);

    StringBuilder sb = new StringBuilder();
    foreach (var cs in Pole)
    {
        string hodnota = cs.ToString();
        string Vzor = "Soubor <a href=\"" + hodnota + "\"> " + Path.GetFileName(cs) + " </a> byl aktualizován<br/>";
        sb.AppendLine(Vzor);     
    }

    string email = "milan.simko@tractebel.engie.com"; // Zadej e-mailovou adresu příjemce
    //string email = "martin.csato@tractebel.engie.com";
    new Email.Email().SendEmail(LinkyEmaily, sb.ToString());

    //new Email.Email().SendEmail(email, sb.ToString());
}