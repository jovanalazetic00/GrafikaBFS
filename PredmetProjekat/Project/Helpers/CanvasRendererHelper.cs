using Project.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Win = System.Windows;
using System.Windows;
using System.Windows.Media.Animation;

namespace Project.Helpers
{
    public static class CanvasRendererHelper
    {
        #region Propeties
        private static Dictionary<long, (int, int)> positionCoordinates  = new Dictionary<long, (int, int)>();
        private static List<(long, long)> DrawnLines  = new List<(long, long)>();
        private static List<LineEntity> pendingLines = new List<LineEntity>();
        private static List<long> NodeIds { get; set; } = new List<long>();  //cuva nodove ideve da zna posle koje linije da ne crta
        private static (Ellipse, Ellipse) entityClicked = (null, null);
        private static (Brush, Brush) entityClickedBrush = (null, null);
        public static MainWindow AppMainWindow { get; set; }

        #endregion

        #region Methods
        private static void DetermineAndDrawPath(LineEntity line)  //linije bez preklapanja
        {
            if (DrawnLines.Any(t => (t.Item1 == line.FirstEnd && t.Item2 == line.SecondEnd) 
                || (t.Item2 == line.FirstEnd && t.Item1 == line.SecondEnd)))
                return;
            DrawnLines.Add((line.FirstEnd, line.SecondEnd));  //dodajem cvorove

            var start = positionCoordinates[line.FirstEnd];
            var end = positionCoordinates[line.SecondEnd]; 

            List<(int, int)> linePoints = AlgorithmHelper.FindFirstLinePoints(start, end);
            if (linePoints == null)
            {
                pendingLines.Add(line);
                return;
            }
            RenderPathOnCanvas(line, linePoints);
        }
        private static void DetermineAndDrawAlternatePath(LineEntity line)
        {
            if (!positionCoordinates.ContainsKey(line.FirstEnd) || !positionCoordinates.ContainsKey(line.SecondEnd))  //da li postoji dupliranje, da se ne iscrtava duplo
                return;  //krece iz prvog a zavrsava se na drugom

            DrawnLines.Add((line.FirstEnd, line.SecondEnd));  //dodajem liniju u iscrtane, pocetni i krajnji dio

            var start = positionCoordinates[line.FirstEnd];  //izvlacim pozicije pocetak i kraj
            var end = positionCoordinates[line.SecondEnd];

            (var marks, var linePoints) = AlgorithmHelper.FindSecondLinePointsWithMarks(start, end);  //racunam za prve
            if (linePoints == null)
                return;

            RenderPathOnCanvas(line, linePoints);
            foreach (var mark in marks)
            {
                Ellipse ellipse = new Ellipse()
                {
                    Width = 1,
                    Height = 1,
                    Stroke = Brushes.Purple,
                    StrokeThickness = 0.3,
                    Fill = Brushes.Purple,
                };
                Canvas.SetTop(ellipse, mark.Item1 * CanvasHelper.Move + .5);
                Canvas.SetLeft(ellipse, mark.Item2 * CanvasHelper.Move + .5);
                AppMainWindow.MainCanvas.Children.Add(ellipse);
            }
        }
        private static bool IsBeyondBounds((int,int) tuple)
        {
            if (tuple.Item1 < 0 || tuple.Item2 < 0 || tuple.Item1 >= CanvasHelper.Size || tuple.Item2 >= CanvasHelper.Size)
                return true;
            return false;
        }
        private static (int, int) TranslateToCanvasCoordinates(double x, double y)
        {
            return ((int)Math.Floor((CanvasHelper.CanvasMaxX - x) * CanvasHelper.CanvasOffsetX), (int)Math.Floor((y - CanvasHelper.CanvasMinY) * CanvasHelper.CanvasOffsetY));  //iz koordinata prebacuje u razmjeru koja nama treba 
            //uzima se min jer raste iz donjeg lijevog ugla
        }

        private static (int, int) ComputeNewCoordinates(int x, int y)  //jedan iterator trazi na jednoj pozciji, a drugi okolo
        {

            for (int it = 1; ; it++)  //da li je it van opsega
            {
                for (int i = x - it; i <= x + it; i++)  //i se smanjuje za operator
                {
                    if (i < 0 || i >= CanvasHelper.Size)
                        continue;
                    for (int j = y - it; j <= y + it; j++)
                    {
                        if (j < 0 || j >= CanvasHelper.Size)  //moraju biti oba u okviru 
                            continue;
                        if (!positionCoordinates.Any(t => t.Value == (i, j))) //ako ne postoji takva pozicija, vracam tu poziciju i tu je tacka
                            return (i, j);
                    }
                }
            }
        }
        private static void RenderEntityOnCanvas(PowerEntity entity)
        {
            if (!CanvasHelper.RenderNodes && entity.GetType() == typeof(NodeEntity))  //Ako ne renderujem nodove, i type entity je nodeEntity
            {
                NodeIds.Add(entity.Id);  //dodajem ga u listu
                return;
            }

            AppMainWindow.Dispatcher.Invoke(() =>
            {
                Ellipse ellipse = new Ellipse()  //ovdje pravim entitet, visina sirina, boja, tool tip
                {
                    Width = 2,
                    Height = 2,
                    Stroke = Brushes.Black,
                    StrokeThickness = 0.2,
                    ToolTip = $"ID: {entity.Id}\nName: {entity.Name}\nType: ",
                    Tag = entity.Id.ToString()  //za animacije
                };
                if (entity.GetType() == typeof(NodeEntity))   //da se razlikuju svicevi nodovi.. i boje njihove
                {
                    ellipse.Fill = Brushes.Red;
                    ellipse.ToolTip += "Node";
                }
                else if (entity.GetType() == typeof(SwitchEntity))
                {
                    ellipse.Fill = Brushes.Green;
                    ellipse.ToolTip += "Switch";
                }
                else if (entity.GetType() == typeof(SubstationEntity))
                {
                    ellipse.Fill = Brushes.Blue;
                    ellipse.ToolTip += "Substation";
                }

                int x, y;
                (x, y) = TranslateToCanvasCoordinates(entity.X, entity.Y);
                if (positionCoordinates.Any(t => t.Value == (x, y)) || IsBeyondBounds((x, y)))  //ako vec nesto postoji na toj poziciji ili dali izlazi iz okvira
                    (x, y) = ComputeNewCoordinates(x, y);  //onda radimo mijenjanje pozicije

                positionCoordinates[entity.Id] = (x, y);  //dodjeljujem tacku
                Canvas.SetTop(ellipse, x * CanvasHelper.Move);  //setujem top i left, krece s lijeva
                Canvas.SetLeft(ellipse, y * CanvasHelper.Move);
                AppMainWindow.MainCanvas.Children.Add(ellipse);  //dodaj elipsu u children
            });
        }
        public static void DisplayPowerEntitiesOnCanvas()
        {
            NodeIds.Clear();  //ocistim nodove ako ih ima  
            foreach (var node in Entities.PowerEntities)  //za svaki node iz power entities se poziva renderEntityOnCanvas
                RenderEntityOnCanvas(node);
        }
        private static void EnrichWithScaleAndColorAnimation(Storyboard storyboard, Ellipse ellipse)
        {
            var time = new Duration(TimeSpan.FromSeconds(1));
            var scaleX = new DoubleAnimation { To = 2.0, Duration = time };
            ellipse.RenderTransform = new ScaleTransform { ScaleX = 1 };
            Storyboard.SetTarget(scaleX, ellipse);
            Storyboard.SetTargetProperty(scaleX, new PropertyPath("(Ellipse.RenderTransform).(ScaleTransform.ScaleX)"));
            storyboard.Children.Add(scaleX);

            var scaleY = new DoubleAnimation { To = 2.0, Duration = time };
            ellipse.RenderTransform = new ScaleTransform { ScaleY = 1 };
            Storyboard.SetTarget(scaleY, ellipse);
            Storyboard.SetTargetProperty(scaleY, new PropertyPath("(Ellipse.RenderTransform).(ScaleTransform.ScaleY)"));
            storyboard.Children.Add(scaleY);

            var color = new ColorAnimation { To = Colors.DarkOrange, Duration = time };
            Storyboard.SetTarget(color, ellipse);
            Storyboard.SetTargetProperty(color, new PropertyPath("(Shape.Fill).(SolidColorBrush.Color)"));
            storyboard.Children.Add(color);
        }
        private static void RenderPathOnCanvas(LineEntity line, List<(int,int)> points)  //koristi se animacija
        {
            AppMainWindow.Dispatcher.Invoke(() =>
            {
                var path = new Path
                {
                    Stroke = line.IsUnderground ? Brushes.Blue : Brushes.OrangeRed,
                    StrokeThickness = 0.4,
                    ToolTip = $"ID: {line.Id}\nName: {line.Name}"
                };

                path.MouseRightButtonUp += (s, e) =>
                {
                    Ellipse first = null, second = null;
                    foreach (var item in AppMainWindow.MainCanvas.Children)
                    {
                        if(item is Ellipse)
                        {
                            var el = (Ellipse)item;
                            if (el.Tag.ToString() == line.FirstEnd.ToString())
                                first = el;
                            else if (el.Tag.ToString() == line.SecondEnd.ToString())
                                second = el;
                        }
                        if (first != null && second != null)
                            break;
                    }
                    if(first != null && second != null)
                    {
                        if(entityClicked.Item1 != null)
                        {
                            entityClicked.Item1.Fill = entityClickedBrush.Item1;
                            entityClicked.Item2.Fill = entityClickedBrush.Item2;

                            entityClicked.Item1.RenderTransform = new ScaleTransform { ScaleY = 1, ScaleX = 1 };
                            entityClicked.Item2.RenderTransform = new ScaleTransform { ScaleY = 1, ScaleX = 1 };
                        }

                        entityClicked = (first, second);
                        entityClickedBrush = (first.Fill.CloneCurrentValue(), second.Fill.CloneCurrentValue());
                        
                        var storyboard = new Storyboard();
                        EnrichWithScaleAndColorAnimation(storyboard, first);
                        EnrichWithScaleAndColorAnimation(storyboard, second);
                        storyboard.Begin();
                    }
                };
                var geometry = new PathGeometry();
                var figure = new PathFigure
                {
                    StartPoint = new Win.Point(points.First().Item2 * CanvasHelper.Move + CanvasHelper.Move / 2,
                        points.First().Item1 * CanvasHelper.Move + CanvasHelper.Move / 2)
                };

                for (int i = 1; i < points.Count; i++)
                    figure.Segments.Add(new LineSegment(new Win.Point(points[i].Item2 * CanvasHelper.Move + CanvasHelper.Move / 2, 
                        points[i].Item1 * CanvasHelper.Move + CanvasHelper.Move / 2), true));

                geometry.Figures.Add(figure);
                path.Data = geometry;
                Panel.SetZIndex(path, -1);
                AppMainWindow.MainCanvas.Children.Add(path);
            });
        }
        public static void DisplayLinesOnCanvas()
        {
            //nadjem sve linije koje se nalaze medju tackama
            var allLines = Entities.Lines.FindAll(t => positionCoordinates.ContainsKey(t.FirstEnd) && positionCoordinates.ContainsKey(t.SecondEnd))
                .OrderBy(t => !t.IsUnderground).ToList(); //one koje su podzemne da budu prve

            allLines.RemoveAll(t => NodeIds.Contains(t.FirstEnd));  //izbacuje sve koji pocinju od svica, nodeEntity

            foreach (var line in allLines)
            {
                DetermineAndDrawPath(line);
            }

            foreach (var line in pendingLines)
            {
                DetermineAndDrawAlternatePath(line);
            }

            pendingLines.Clear();
            positionCoordinates.Clear();
            DrawnLines.Clear();
        }
        #endregion
    }
}
