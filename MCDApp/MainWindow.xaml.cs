using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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
        public bool mark = true;
        int blockSize = 15;
        OvalView ov;
        PolygonView pv;
        LineView lv;

        public MainWindow()
        {
            InitializeComponent();
            ov = new OvalView();
            ov.OnNewSheme += Ow_OnNewSheme;
            pv = new PolygonView();
            pv.OnNewSheme += Ow_OnNewSheme;
            lv = new LineView();
            lv.OnNewSheme += Ow_OnNewSheme;
            OvalTab.DataContext = ov;
            PolygonTab.DataContext = pv;
            LineTab.DataContext = lv;
            //mnctab.DataContext = pw;
            UpdateTextBoxes();
            ov.Heigth = 10;
        }

        private void Ow_OnNewSheme(List<IntPoint> points)
        {
            UpdateTextBoxes();
            DrawShape(points);
        }
        void DrawShape(List<IntPoint> points)
        {
            ShemeChanvas.Children.Clear();

            ShemeChanvas.Width = points.Select(i => i.X).Max() * blockSize;
            ShemeChanvas.Height = points.Select(i => i.Y).Max() * blockSize;
            Dictionary<IntPoint, string> markup = new Dictionary<IntPoint, string>();

            if (mark)
            {

                var lined = points.OrderBy(i => i.Y).GroupBy(i => i.Y);
                foreach (var l in lined)
                {
                    if (l.Count() < 3)
                        continue;
                    var line = l.OrderBy(i => i.X).ToList();
                    var linePoint = new List<int>() { 0 };
                    for (int i = 0; i < line.Count; i++)
                    {
                        if (i != line.Count - 1 && line[i].X != line[i + 1].X - 1)
                        {
                            linePoint.Add(i);
                            linePoint.Add(i + 1);
                        }
                    }
                    linePoint.Add(line.Count - 1);

                    var linesIds = linePoint.Chunk(2);
                    foreach (var microLineId in linesIds)
                    {
                        var micloLine = line.Skip(microLineId[0]).Take(microLineId[1] - microLineId[0] + 1).ToList();
                        if (micloLine.Count > 2)
                        {
                            markup.Add(micloLine.First(), "←");
                            markup.Add(micloLine.Last(), "→");
                            markup.Add(micloLine[micloLine.Count / 2], micloLine.Count.ToString());
                        }
                    }
                }
                lined = points.OrderBy(i => i.X).GroupBy(i => i.X);
                foreach (var l in lined)
                {
                    if (l.Count() < 3)
                        continue;
                    var line = l.OrderBy(i => i.Y).ToList();
                    var linePoint = new List<int>() { 0 };
                    for (int i = 0; i < line.Count; i++)
                    {
                        if (i != line.Count - 1 && line[i].Y != line[i + 1].Y - 1)
                        {
                            linePoint.Add(i);
                            linePoint.Add(i + 1);
                        }
                    }
                    linePoint.Add(line.Count - 1);

                    var linesIds = linePoint.Chunk(2);
                    foreach (var microLineId in linesIds)
                    {
                        var micloLine = line.Skip(microLineId[0]).Take(microLineId[1] - microLineId[0] + 1).ToList();
                        if (micloLine.Count > 2)
                        {

                            markup.Add(micloLine.First(), "↑");
                            markup.Add(micloLine.Last(), "↓");
                            markup.Add(micloLine[micloLine.Count / 2], micloLine.Count.ToString());
                        }
                    }
                }
            }
            foreach (var point in points)
            {
                Border block = new Border();
                block.Height = blockSize;
                block.Width = blockSize;
                block.BorderThickness = new Thickness(2);
                block.Background = new SolidColorBrush(Colors.LightGray);
                block.DataContext = point;
                block.MouseEnter += (sender, e) => { CurrentBlock.Text = (sender as Border).DataContext.ToString(); };
                if (mark)
                {
                    if (markup.TryGetValue(point, out var text))
                        block.Child = new TextBlock() { Text = text, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, FontSize = 10 };
                }

                Canvas.SetLeft(block, point.X * blockSize);
                Canvas.SetTop(block, point.Y * blockSize);
                ShemeChanvas.Children.Add(block);
            }
            ShemeChanvas.Width = blockSize * (points.Select(i => i.X).Max() + 5);
            ShemeChanvas.Height = blockSize * (points.Select(i => i.Y).Max() + 5);

            TotalTB.Text = points.Count.ToString();
        }
        private void UpdateTextBoxes()
        {
            //WStr.Text = ow.WidthStr;
            //HStr.Text = ow.HeigthStr;
            TopTB.Text = $"Сверху(с {ov.TopLimit})";
            BotTB.Text = $"Снизу(до {ov.BottomLimit})";
            LeftTB.Text = $"Слева(с {ov.LeftLimit})";
            RightTB.Text = $"Справа(до {ov.RightLimit})";
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ShemeChanvas == null)
                return;
            ShemeChanvas.RenderTransform = new ScaleTransform() { ScaleX = e.NewValue / 100, ScaleY = e.NewValue / 100 };
            ShemeChanvas.LayoutTransform = new ScaleTransform() { ScaleX = e.NewValue / 95, ScaleY = e.NewValue / 95 };
            ScaleTB.Text = Math.Round(e.NewValue).ToString() + "%";
        }
    }
}
