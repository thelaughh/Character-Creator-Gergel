using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace Character_Creator_Gergel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CreateCharacter : Window
    {
        public CreateCharacter(bool darkmode, Player? playerToLoad)
        {
            InitializeComponent();

            if (darkmode)
            {
                Background = new SolidColorBrush(Colors.Black);
            }
            else
            {
                Background = new SolidColorBrush(Colors.White);
            }

            RaceBox.ItemsSource = new List<string> {
            "Human", "Dwarf",
        };

            ClassBox.ItemsSource = new List<string> {
            "Archer", "Warrior", "Wizard",
        };

            FactionBox.ItemsSource = new List<string> {
            "Red", "Blue",
        };

            RaceBox.SelectedIndex = 0;
            RaceBox.SelectionChanged += Refresh;
            ClassBox.SelectedIndex = 0;
            ClassBox.SelectionChanged += Refresh;

            FactionBox.SelectionChanged += FactionBoxOnSelectionChanged;
            FactionBox.SelectedIndex = 0;

            if (playerToLoad != null)
            {
                RaceBox.SelectedItem = playerToLoad.Race;
                ClassBox.SelectedItem = playerToLoad.Class;
                FactionBox.SelectedItem = playerToLoad.Faction;
                PlayerName.Text = playerToLoad.Name;
            }


            try
            {
                CharacterImg.Source = new BitmapImage(new Uri($"Imgs/{RaceBox.SelectedItem}/{ClassBox.SelectedItem}.jpg", UriKind.RelativeOrAbsolute));
            }
            catch { }
        }

        private void Refresh(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                CharacterImg.Source = new BitmapImage(new Uri($"Imgs/{RaceBox.SelectedItem}/{ClassBox.SelectedItem}.jpg", UriKind.RelativeOrAbsolute));
            }
            catch { }

            CharacterImg.InvalidateVisual();
        }

        private void SaveCharacter(object sender, RoutedEventArgs e)
        {
            Player player = new Player();
            player.Name = this.PlayerName.Text;
            try
            {
                player.Class = ClassBox.SelectedItem.ToString();
                player.Faction = FactionBox.SelectedItem.ToString();
                player.Race = RaceBox.SelectedItem.ToString();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error saving character!");
                return;
            }

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.AddExtension = true;
            dialog.Filter = "xml files|*.xml";
            if (dialog.ShowDialog() == true)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Player));
                try
                {
                    StreamWriter writer = new StreamWriter(dialog.FileName);
                    serializer.Serialize(writer.BaseStream, player);
                    writer.Close();
                }
                catch
                {
                    MessageBox.Show("Error serializing character!");
                    return;
                }
            }
        }

        private void FactionBoxOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FactionBox.SelectedIndex == 0)
            {
                borderFaction.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                borderFaction.BorderBrush = new SolidColorBrush(Colors.Blue);
            }
        }
    }
}
