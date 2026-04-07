using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace mapa
{
    public partial class MainWindow : Window
    {
        List<string> mesta = new List<string>()
        {
            "Praha",
            "Brno",
            "Ostrava"
        };
        List<Button> cityButtons;

        List<string> remainingCities;

        Random rnd = new Random();
        string aktivniMesto;

        int totalRounds;
        int score = 0;

        public MainWindow()
        {
            InitializeComponent();

            cityButtons = new List<Button> { btnPraha, btnBrno, btnOstrava };

            totalRounds = cityButtons.Count;

            remainingCities = new List<string>(mesta);

            NovaOtazka();
        }

        void NovaOtazka()
        {
            if (remainingCities.Count == 0)
            {
                MessageBox.Show($"Kvíz dokončen. Skóre: {score}/{totalRounds}", "Výsledek");
                foreach (var b in cityButtons) b.IsEnabled = false;
                txtMesto.Text = $"Hotovo. Skóre: {score}/{totalRounds}";
                return;
            }

            int idx = rnd.Next(remainingCities.Count);
            aktivniMesto = remainingCities[idx];
            remainingCities.RemoveAt(idx);

            int roundsPlayed = totalRounds - remainingCities.Count;
            txtMesto.Text = $"Kolo {roundsPlayed}/{totalRounds} — Najdi město: {aktivniMesto}  | Skóre: {score}";
        }

        private void Mapa_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(this);
            MessageBox.Show("X: " + (int)p.X + " Y: " + (int)p.Y, "Souřadnice");
        }

        private void City_Click(object sender, RoutedEventArgs e)
        {
            if (aktivniMesto == null) return;

            Button btn = sender as Button;
            string kliknuteMesto = btn?.Content?.ToString();

            if (kliknuteMesto == null) return;

            if (kliknuteMesto == aktivniMesto)
            {
                score++;
                MessageBox.Show("Správně!", "Odpověď");
            }
            else
            {
                MessageBox.Show($"Špatně! Správná odpověď byla: {aktivniMesto}", "Odpověď");
            }

            NovaOtazka();
        }
    }
}