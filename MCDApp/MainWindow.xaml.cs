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
            bool xMove = points.Any(i => i.Item1 < 0);
            bool yMove = points.Any(x => x.Item2 < 0);
            List<(int, int)> toDarw = new List<(int, int)>();
            if (xMove || yMove)
            {
                int xMoveLen = 0; int yMoveLen = 0;
                if (xMove)
                {
                    xMoveLen = -points.Select(i => i.Item1).Min();
                }
                if (yMove)
                {
                    yMoveLen = -points.Select(i => i.Item2).Min();
                }
                toDarw = points.Select(i => (i.Item1 + xMoveLen, i.Item2 + yMoveLen)).ToList();
            }
            else
                toDarw = points;
            ShemeChanvas.Width = toDarw.Select(i => i.Item1).Max() * blockSize;
            ShemeChanvas.Height = toDarw.Select(i => i.Item2).Max() * blockSize;

            foreach (var point in toDarw)
            {
                Border block = new Border();
                block.Height = blockSize;
                block.Width = blockSize;
                block.BorderThickness = new Thickness(3);
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
            WStr.Text = ow.WidthStr;
            HStr.Text = ow.HeigthStr;
            TopTB.Text = $"Сверху({ow.TopLimit})%";
            BotTB.Text = $"Снизу({ow.BottomLimit})%";
            LeftTB.Text = $"Слева({ow.LeftLimit})%";
            RightTB.Text = $"Справа({ow.RightLimit})%";

        }
    }
}
