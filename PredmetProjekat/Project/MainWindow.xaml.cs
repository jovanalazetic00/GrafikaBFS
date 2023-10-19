using Project.Classes;
using Project.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Properties
        private bool wasCleared = false;
        private List<Command> ClearList = new List<Command>();   //liste akcija koje su se izvrsavale, pa se vraca pomocu undo redo
        private List<Command> RedoList = new List<Command>();
        private List<Command> UndoList = new List<Command>();
        private List<Point> temporaryPolygonPoints = new List<Point>();
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            btnUndo.IsEnabled = false;  //da li cu moci da kliknem undo ili redo
            btnRedo.IsEnabled = false;
            btnClear.IsEnabled = false;

            CanvasHelper.Size = 200;   //pocinjem od 200 puta 200 canvasom
            CanvasHelper.Move = (int)(CanvasHelper.Size / 100);
        }

        #region Load
        private void LoadModel_Click(object sender, RoutedEventArgs e)
        {
            MainCanvas.Children.Clear();
            CanvasHelper.RenderNodes = true;
            CanvasRendererHelper.AppMainWindow = this;
            ImportHelper.ImportData();
            ImportHelper.FindMinMaxCoordinates();
            CanvasRendererHelper.DisplayPowerEntitiesOnCanvas();  //crtam enititete pa linije 
            CanvasRendererHelper.DisplayLinesOnCanvas();

            Entities.Lines.Clear();
            Entities.PowerEntities.Clear();
        }

        private void MapSize_Click(object sender, RoutedEventArgs e)
        {
            switch (CanvasHelper.Size)
            {
                case 200:
                    CanvasHelper.Size = 750;
                    break;
                case 750:
                    CanvasHelper.Size = 1500;
                    break;
                case 1500:
                    CanvasHelper.Size = 200;
                    break;
                default:
                    CanvasHelper.Size = 200;
                    break;
            }
            //da bude uvijek kvadrat
            MapSize.Content = $"Map Size ({CanvasHelper.Size}x{CanvasHelper.Size})";
            MainCanvas.Children.Clear();  //da se cisti canvas

            MainCanvas.Width = MainCanvas.Height = 2 * CanvasHelper.Size;
            CanvasHelper.Move = 2; //pomjeranje el na mapi

            AlgorithmHelper.map = new bool[CanvasHelper.Size, CanvasHelper.Size];  //nova velicina mape koja se koristi u bfsu
        }

        private void LoadModelNoNodes_Click(object sender, RoutedEventArgs e)
        {
            MainCanvas.Children.Clear();
            CanvasHelper.RenderNodes = false;
            CanvasRendererHelper.AppMainWindow = this;
            ImportHelper.ImportData();
            ImportHelper.FindMinMaxCoordinates();
            CanvasRendererHelper.DisplayPowerEntitiesOnCanvas();
            CanvasRendererHelper.DisplayLinesOnCanvas();

            Entities.Lines.Clear();
            Entities.PowerEntities.Clear();
        }
        #endregion

        #region Drawing
        private void Ellipse_Click(object sender, RoutedEventArgs e)
        {
            MainCanvas.MouseRightButtonUp -= DrawPolygonMouseUp;
            MainCanvas.MouseRightButtonUp -= AddTextMouseUp;
            MainCanvas.MouseLeftButtonUp -= AddPolygonPoints;
            MainCanvas.MouseRightButtonUp += DrawEllipseMouseUp;
        }

        private void DrawEllipseMouseUp(object sender, MouseButtonEventArgs e)
        {
            MainCanvas.MouseRightButtonUp -= DrawEllipseMouseUp;

            var mousePosition = e.GetPosition(MainCanvas);
            var drawEllipseWindow = new DrawEllipseWindow();

            drawEllipseWindow.ShowDialog();

            if (drawEllipseWindow.StrokeColor == null)
                return;

            var gridContainer = new Grid { Background = Brushes.Transparent };
            var ellipse = new Ellipse
            {
                Stroke = drawEllipseWindow.StrokeColor,
                Fill = drawEllipseWindow.FillColor,
                StrokeThickness = drawEllipseWindow.StrokeThickness,
                Opacity = drawEllipseWindow.OpacityValue,
                Width = drawEllipseWindow.Width,
                Height = drawEllipseWindow.Height,
            };

            var text = new TextBlock
            {
                Text = drawEllipseWindow.TextContent,
                Foreground = drawEllipseWindow.TextColor,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            ellipse.MouseLeftButtonUp += (s, ee) =>
            {
                var de = new DrawEllipseWindow(ellipse, text);
                de.ShowDialog();
            };

            text.MouseLeftButtonUp += (s, ee) =>
            {
                var de = new DrawEllipseWindow(ellipse, text);
                de.ShowDialog();
            };

            gridContainer.Children.Add(ellipse);
            gridContainer.Children.Add(text);

            Canvas.SetLeft(gridContainer, mousePosition.X);
            Canvas.SetTop(gridContainer, mousePosition.Y);

            MainCanvas.Children.Add(gridContainer);

            UndoList.Add(new Command 
            { 
                Undo = () => { MainCanvas.Children.Remove(gridContainer); },
                Redo = () => { MainCanvas.Children.Add(gridContainer); }
            });

            UpdateHistoryButtonAvailability();
        }

        private void Polygon_Click(object sender, RoutedEventArgs e)
        {
            MainCanvas.MouseRightButtonUp -= AddTextMouseUp;
            MainCanvas.MouseRightButtonUp -= DrawEllipseMouseUp;
            MainCanvas.MouseRightButtonUp += AddPolygonPoints;
            MainCanvas.MouseLeftButtonUp += DrawPolygonMouseUp;

            temporaryPolygonPoints.Clear();
        }

        private void AddPolygonPoints(object sender, MouseButtonEventArgs e) => temporaryPolygonPoints.Add(Mouse.GetPosition(MainCanvas));

        private void DrawPolygonMouseUp(object sender, MouseButtonEventArgs e)
        {
            MainCanvas.MouseRightButtonUp -= AddPolygonPoints;
            MainCanvas.MouseLeftButtonUp -= DrawPolygonMouseUp;

            if (temporaryPolygonPoints.Count < 3)
            {
                temporaryPolygonPoints.Clear();
                MessageBox.Show("There has to be at least 3 points.");
                return;
            }

            var drawPolygonWindow = new DrawPolygonWindow();
            drawPolygonWindow.ShowDialog();

            var newCanvas = new Canvas { Background = Brushes.Transparent };
            var newPolygon = new Polygon
            {
                Stroke = drawPolygonWindow.StrokeColor,
                Fill = drawPolygonWindow.FillColor,
                StrokeThickness = drawPolygonWindow.StrokeThickness,
                Opacity = drawPolygonWindow.OpacityValue,
                Points = new PointCollection(temporaryPolygonPoints)
            };

            double newMinX = temporaryPolygonPoints.Min(p => p.X), maxX = temporaryPolygonPoints.Max(p => p.X);
            double newMinY = temporaryPolygonPoints.Min(p => p.Y), maxY = temporaryPolygonPoints.Max(p => p.Y);

            temporaryPolygonPoints.Clear();

            var text = new TextBlock
            {
                Text = drawPolygonWindow.TextContent,
                Foreground = drawPolygonWindow.TextColor,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            text.MouseLeftButtonUp += (s, ee) =>
            {
                var newDrawPolygonWindow = new DrawPolygonWindow(newPolygon, text);
                newDrawPolygonWindow.ShowDialog();
            };

            newPolygon.MouseLeftButtonUp += (s, ee) =>
            {
                var newDrawPolygonWindow = new DrawPolygonWindow(newPolygon, text);
                newDrawPolygonWindow.ShowDialog();
            };

            Canvas.SetZIndex(newPolygon, 1);
            Canvas.SetZIndex(text, 2);
            Canvas.SetLeft(text, newMinX + (maxX - newMinX)/2);
            Canvas.SetTop(text, newMinY + (maxY - newMinY)/2);

            newCanvas.Children.Add(newPolygon);
            newCanvas.Children.Add(text);
            MainCanvas.Children.Add(newCanvas);
            
            UndoList.Add(new Command
            {
                Undo = () => { MainCanvas.Children.Remove(newCanvas);},
                Redo = () => { MainCanvas.Children.Add(newCanvas); }
            });

            UpdateHistoryButtonAvailability();
        }

        private void Text_Click(object sender, RoutedEventArgs e)
        {
            MainCanvas.MouseRightButtonUp -= DrawPolygonMouseUp;
            MainCanvas.MouseRightButtonUp -= DrawEllipseMouseUp;
            MainCanvas.MouseLeftButtonUp -= AddPolygonPoints;
            MainCanvas.MouseRightButtonUp += AddTextMouseUp;
        }

        private void AddTextMouseUp(object sender, MouseButtonEventArgs e)
        {
            MainCanvas.MouseRightButtonUp -= AddTextMouseUp;

            var addTextWindow = new AddTextWindow();
            var mousePosition = e.GetPosition(MainCanvas);
            addTextWindow.ShowDialog();

            var text = new TextBlock
            {
                Text = addTextWindow.TextContent,
                Foreground = addTextWindow.TextColor,
                FontSize = addTextWindow.TextSize
            };

            text.MouseLeftButtonUp += (s, ee) => 
            {
                var newAddTextWindow = new AddTextWindow(text);
                newAddTextWindow.ShowDialog();
            };

            Canvas.SetLeft(text, mousePosition.X);
            Canvas.SetTop(text, mousePosition.Y);

            MainCanvas.Children.Add(text);

            UndoList.Add(new Command
            {
                Undo = () => { MainCanvas.Children.Remove(text); },
                Redo = () => { MainCanvas.Children.Add(text); }
            });

            UpdateHistoryButtonAvailability();
        }
        #endregion

        #region History
        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            if (wasCleared)  //ako je clear vraca sve nazad
            {
                ClearList.ForEach(t => t.Redo());
                UndoList.AddRange(ClearList);
                ClearList.Clear();

                wasCleared = false;
            }

            else if (UndoList.Count > 0)
            {
                var command = UndoList.Last();
                UndoList.Remove(command);
                command.Undo();
                RedoList.Add(command);
            }

            UpdateHistoryButtonAvailability();
        }

        private void Redo_Click(object sender, RoutedEventArgs e)
        {
            if (RedoList.Count > 0)  //da li je redo list vece od nula i onda vrati ponovo sve 
            {
                var command = RedoList.Last();
                RedoList.Remove(command);
                command.Redo();
                UndoList.Add(command); //doda ponovo u listu
            }

            UpdateHistoryButtonAvailability();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearList.AddRange(UndoList);
            UndoList.ForEach(t => t.Undo());
            UndoList.Clear();

            wasCleared = true;
            UpdateHistoryButtonAvailability();
        }

        private void UpdateHistoryButtonAvailability()
        {
            btnUndo.IsEnabled = UndoList.Count > 0 || ClearList.Count > 0;
            btnRedo.IsEnabled = RedoList.Count > 0;
            btnClear.IsEnabled = UndoList.Count > 0;
        }
        #endregion
    }
}
