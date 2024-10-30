using System;
using Microsoft.Maui.Controls;

namespace Counter
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnAddCounterClicked(object sender, EventArgs e)
        {
            string counterName = CounterNameEntry.Text?.Trim();
            if (string.IsNullOrEmpty(counterName))
            {
                DisplayAlert("Błąd", "Proszę wpisać nazwę dla licznika.", "OK");
                return;
            }

            var counterView = CreateCounterView(counterName);
            CountersContainer.Children.Add(counterView);
            CounterNameEntry.Text = string.Empty; // Czyści pole tekstowe po dodaniu licznika
        }

        private StackLayout CreateCounterView(string counterName)
        {
            var counterLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 10,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(10),
                BackgroundColor = Color.FromRgb(30, 30, 30), // Ciemne tło dla każdego licznika
                Margin = new Thickness(0, 10, 0, 0) // Przerwa pomiędzy licznikami
            };

            var label = new Label
            {
                Text = counterName,
                VerticalOptions = LayoutOptions.Center,
                TextColor = Color.FromRgb(255, 105, 180) // HotPink
            };

            var countLabel = new Label
            {
                Text = "0",
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = 50,
                TextColor = Color.FromRgb(255, 255, 255) // Biały
            };

            var incrementButton = new Button
            {
                Text = "+",
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Color.FromRgb(0, 0, 255), // Niebieski kolor
                TextColor = Color.FromRgb(255, 255, 255), // Biały
                WidthRequest = 50
            };
            incrementButton.Clicked += (s, e) => UpdateCounter(countLabel, 1);

            var decrementButton = new Button
            {
                Text = "-",
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Color.FromRgb(0, 0, 255), // Niebieski kolor
                TextColor = Color.FromRgb(255, 255, 255), // Biały
                WidthRequest = 50
            };
            decrementButton.Clicked += (s, e) => UpdateCounter(countLabel, -1);

            counterLayout.Children.Add(label);
            counterLayout.Children.Add(decrementButton);
            counterLayout.Children.Add(countLabel);
            counterLayout.Children.Add(incrementButton);

            return counterLayout;
        }

        private void UpdateCounter(Label countLabel, int change)
        {
            int currentCount = int.Parse(countLabel.Text);
            if (change > 0 && currentCount == int.MaxValue)
            {
                DisplayAlert("Limit osiągnięty", "Maksymalna wartość licznika została osiągnięta.", "OK");
                return;
            }
            currentCount += change;
            if (currentCount < 0)
            {
                currentCount = 0; // Nie pozwala na ujemne wartości
            }
            else if (currentCount > int.MaxValue)
            {
                DisplayAlert("Limit osiągnięty", "Maksymalna wartość licznika została osiągnięta.", "OK");
                return;
            }
            countLabel.Text = currentCount.ToString();
        }
    }
}
