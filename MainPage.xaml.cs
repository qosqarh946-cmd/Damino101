using Microsoft.Maui.Controls;

namespace Damino_101_.az;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    // "DAXİL OL" düyməsinə klik edəndə işləyən kod
    private async void OnLoginClicked(object sender, EventArgs e)
    {
        // Xanaların boş olub-olmadığını yoxlayırıq
        if (string.IsNullOrWhiteSpace(UsernameEntry.Text) || string.IsNullOrWhiteSpace(PasswordEntry.Text))
        {
            await DisplayAlert("Giriş Xətası", "Zəhmət olmasa istifadəçi adı və parolu daxil edin!", "Tamam");
            return;
        }

        // Giriş uğurludursa, birbaşa qızıl seçimi (Oyun Yaratma) pəncərəsini açırıq
        CreateCustomGame(sender, e);
    }

    // Oyun Yaratma və Qızıl Seçimi (100 - 5000)
    private async void CreateCustomGame(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("Oyun Yarat",
            "Mərc miqdarını daxil edin (100 - 5000):",
            "Yarat", "Ləğv et", "500", -1, Keyboard.Numeric);

        if (!string.IsNullOrEmpty(result))
        {
            if (int.TryParse(result, out int gold) && gold >= 100 && gold <= 5000)
            {
                // Hələ ki GamePage-i yaratmadığımız üçün bu hissəni bağlı saxlayırıq
                // await Navigation.PushAsync(new GamePage(gold));

                await DisplayAlert("Uğurlu", $"{gold} qızıl ilə oyun yaradılır...", "Tamam");
            }
            else
            {
                await DisplayAlert("Xəta", "Zəhmət olmasa 100 və 5000 arası düzgün məbləğ yazın.", "Tamam");
            }
        }
    }

    private async void FindRandomOpponent(object sender, EventArgs e)
    {
        await DisplayAlert("Onlayn", "Rəqib axtarılır...", "Ləğv et");
    }

    private async void JoinWithCode(object sender, EventArgs e)
    {
        string code = await DisplayPromptAsync("Qoşul", "Masa kodunu yazın:", "Daxil ol", "Ləğv et");
    }
}