using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace Character_Creator_Gergel
{
    /// <summary>
    /// Interakční logika pro Starter.xaml
    /// </summary>
    public partial class Starter : Window
    {
            private bool darkmode = false;
            public Starter()
            {
                InitializeComponent();
            }

            private void CreateCharacterButton(object sender, RoutedEventArgs e)
            {
                CreateCharacter character = new(darkmode, null);
                character.ShowDialog();
            }
            //S tímhle mi pomohl jirka poněvadž to jsem nevěděl//
            private void LoadCharacter(object sender, RoutedEventArgs e)
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Multiselect = false;
                dialog.Filter = "xml files|*.xml";
                if (dialog.ShowDialog() == true)
                {
                    try
                    {
                        StreamReader reader = new StreamReader(dialog.FileName);
                        Player? player = (Player?)new XmlSerializer(typeof(Player)).Deserialize(reader.BaseStream);
                        CreateCharacter character = new(this.darkmode, player);
                        reader.Close();
                        character.ShowDialog();
                        Close();
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show("Error loading character!");
                        return;
                    }
                }
            }
            // tohle jsem zvládal z netu 😂//

            private void Dark(object sender, RoutedEventArgs e)
            {
                darkmode = true;
                Background = new SolidColorBrush(Colors.Black);
            }

            private void Light(object sender, RoutedEventArgs e)
            {
                darkmode = false;
                Background = new SolidColorBrush(Colors.White);
            }
        }
    }