using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Project
{
    /// <summary>
    /// Interaction logic for DrawPolygonWindow.xaml
    /// </summary>
    public partial class DrawPolygonWindow : Window
    {
        #region Properties
        private Polygon AssociatedPolygon = null;
        private TextBlock AssociatedTextBlock = null;
        public SolidColorBrush FillColor { get; set; }
        public SolidColorBrush StrokeColor { get; set; }
        public SolidColorBrush TextColor { get; set; }
        public double StrokeThickness { get; set; }
        public double OpacityValue { get; set; }
        public string TextContent { get; set; }
        #endregion

        #region Constructors
        public DrawPolygonWindow()
        {
            InitializeComponent();
            fillColor.SelectedColor = Colors.Black;
            strokeColor.SelectedColor = Colors.Black;
            textColor.SelectedColor = Colors.Black;
        }

        public DrawPolygonWindow(Polygon polygon, TextBlock text)
        {
            InitializeComponent();
            drawPolygonButton.Content = "Edit";
            AssociatedPolygon = polygon;
            AssociatedTextBlock = text;
            strokeThickness.Text = polygon.StrokeThickness.ToString();
            strokeColor.SelectedColor = ((SolidColorBrush)polygon.Stroke).Color;
            if (polygon.Opacity == 0.5) polygonIsTransparent.IsChecked = true;
            else polygonIsTransparent.IsChecked = false;
            fillColor.SelectedColor = ((SolidColorBrush)polygon.Fill).Color;
            textColor.SelectedColor = ((SolidColorBrush)text.Foreground).Color;
            textBox.Text = text.Text;
        }
        #endregion

        #region Methods
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void drawPolygonButton_Click(object sender, RoutedEventArgs e)
        {
            if (AssociatedPolygon != null)
            {
                AssociatedPolygon.Fill = new SolidColorBrush(fillColor.SelectedColor ?? Colors.Black);
                AssociatedPolygon.Stroke = new SolidColorBrush(strokeColor.SelectedColor ?? Colors.Black);
                AssociatedPolygon.StrokeThickness = double.Parse(strokeThickness.Text);
                if ((bool)polygonIsTransparent.IsChecked) AssociatedPolygon.Opacity = 0.5;
                else AssociatedPolygon.Opacity = 1;
                AssociatedTextBlock.Text = textBox.Text;
                AssociatedTextBlock.Foreground = new SolidColorBrush(textColor.SelectedColor ?? Colors.Black);
                Close();
                return;
            }

            if (fillColor.SelectedColor == null || strokeColor.SelectedColor == null || strokeThickness == null)
                return;

            try
            {
                FillColor = new SolidColorBrush(fillColor.SelectedColor ?? Colors.Black);
                StrokeColor = new SolidColorBrush(strokeColor.SelectedColor ?? Colors.Black);
                StrokeThickness = double.Parse(strokeThickness.Text);
                if ((bool)polygonIsTransparent.IsChecked) OpacityValue = 0.5;
                else OpacityValue = 1;
                TextContent = textBox.Text;
                TextColor = new SolidColorBrush(textColor.SelectedColor ?? Colors.Black);
            }
            catch (Exception)
            {
                MessageBox.Show("Please fill out all fields");
                return;
            }
            Close();
        }
        #endregion
    }
}
