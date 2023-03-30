using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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

namespace MCDApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int blockSize = 15;
        OvalView ow;
        public MainWindow()
        {
            InitializeComponent();
            ow = new OvalView();
            ow.OnNewSheme += Ow_OnNewSheme;
            DataContext = ow;
            UpdateTextBoxes();
            ow.Heigth = 10;
        }

        private void Ow_OnNewSheme(List<(int, int)> points)
        {
            Debug.WriteLine(JsonSerializer.Serialize(ow));
            UpdateTextBoxes();
            DrawShape(points);
        }
        void DrawShape(List<(int, int)> points)
        {
            ShemeChanvas.Children.Clear();
            
            ShemeChanvas.Width = points.Select(i => i.Item1).Max() * blockSize;
            ShemeChanvas.Height = points.Select(i => i.Item2).Max() * blockSize;
            foreach (var point in points)
            {
                Border block = new Border();
                block.Height = blockSize;
                block.Width = blockSize;
                block.BorderThickness = new Thickness(2);
                block.Background = new SolidColorBrush(Colors.Red);
                block.DataContext = point;
                block.MouseEnter += (sender, e) => { CurrentBlock.Text = (sender as Border).DataContext.ToString(); };
                Canvas.SetLeft(block, point.Item1 * blockSize);
                Canvas.SetTop(block, point.Item2 * blockSize);
                ShemeChanvas.Children.Add(block);
            }




        }
        private void UpdateTextBoxes()
        {
            //WStr.Text = ow.WidthStr;
            //HStr.Text = ow.HeigthStr;
            TopTB.Text = $"Сверху(с {ow.TopLimit})";
            BotTB.Text = $"Снизу(до {ow.BottomLimit})";
            LeftTB.Text = $"Слева(с {ow.LeftLimit})";
            RightTB.Text = $"Справа(до {ow.RightLimit})";
            
            

        }
    }
}
