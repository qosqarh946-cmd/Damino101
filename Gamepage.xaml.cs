using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace Damino_101_.az;

public partial class GamePage : ContentPage // Bura NewPage1-dən GamePage-ə dəyişdirildi
{
    List<string> setOfDominoes = new List<string>();
    Random rnd = new Random();

    public GamePage() // Konstruktor adı GamePage olaraq düzəldildi
    {
        InitializeComponent();
    }

    // Əgər MainPage-dən qızıl (gold) miqdarı göndərirsənsə, bu konstruktoru da bura əlavə edirik
    public GamePage(int gold) : this()
    {
        // StatusLabel-in dolması üçün InitializeComponent-dən sonra işləməlidir
    }

    private async void OnShuffleAndDealClicked(object sender, EventArgs e)
    {
        // 1. Daşları yarat
        setOfDominoes.Clear();
        TableLayout.Children.Clear();
        PlayerHandLayout.Children.Clear();

        for (int i = 0; i <= 6; i++)
            for (int j = i; j <= 6; j++)
                setOfDominoes.Add($"{i}:{j}");

        StatusLabel.Text = "Daşlar qarışdırılır...";

        // 2. MASADA QARIŞDIRMA (Vizual)
        List<Frame> visualTiles = new List<Frame>();
        for (int i = 0; i < 28; i++)
        {
            var tile = new Frame
            {
                BackgroundColor = Colors.White,
                WidthRequest = 40,
                HeightRequest = 60,
                CornerRadius = 5,
                Content = new BoxView { Color = Colors.Gray } // Arxa tərəfi
            };
            TableLayout.Children.Add(tile);
            visualTiles.Add(tile);

            // Təsadüfi yerə qoy
            AbsoluteLayout.SetLayoutBounds(tile, new Rect(rnd.Next(50, 250), rnd.Next(50, 300), 40, 60));
        }

        // Qarışdırma animasiyası
        for (int r = 0; r < 5; r++)
        {
            foreach (var tile in visualTiles)
            {
                // Task-lar listi yaradıb eyni anda hərəkət etdirmək daha səliqəli olar
                tile.TranslateTo(rnd.Next(-50, 50), rnd.Next(-50, 50), 100);
            }
            await Task.Delay(100);
        }

        // 3. PAYLAMA (Animasiya ilə aşağıya)
        StatusLabel.Text = "Sizə 7 daş paylanır...";
        var shuffled = setOfDominoes.OrderBy(x => rnd.Next()).ToList();

        for (int i = 0; i < 7; i++)
        {
            string currentTile = shuffled[i];
            var flyingTile = visualTiles[i];

            // Daşı aşağı, əlimizə doğru uçururuq
            await flyingTile.TranslateTo(0, 500, 300);
            flyingTile.IsVisible = false;

            // Əlimizə (PlayerHandLayout) əlavə edirik
            var handTile = new Frame
            {
                BackgroundColor = Colors.White,
                Padding = 5,
                Margin = 2,
                Content = new Label { Text = currentTile, TextColor = Colors.Black, FontAttributes = FontAttributes.Bold }
            };
            PlayerHandLayout.Children.Add(handTile);
        }

        StatusLabel.Text = "Oyun başladı! Sizin növbəniz.";
    }
}