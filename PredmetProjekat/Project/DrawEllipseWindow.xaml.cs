using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Project
{
    /// <summary>
    /// Interaction logic for DrawEllipseWindow.xaml
    /// </summary>
    public partial class DrawEllipseWindow : Window
    {
        #region Properties
        private Ellipse AssociatedEllipse = null;
        private TextBlock AssociatedTextBlock = null;
        public SolidColorBrush FillColor { get; set; }
        public SolidColorBrush StrokeColor { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double StrokeThickness { get; set; }
        public double OpacityValue { get; set; }
        public string TextContent { get; set; }
        public SolidColorBrush TextColor { get; set; }
        #endregion

        #region Constructors
        public DrawEllipseWindow()  //konstruktor za pravljenje elipse
        {
            InitializeComponent();
            fillColor.SelectedColor = Colors.Black;
            strokeColor.SelectedColor = Colors.Black;
            textColor.SelectedColor = Colors.Black;
        }

        public DrawEllipseWindow(Ellipse e, TextBlock t)    //konstruktor za editovanje, prosledjujem elipsu i tekst blok
        {
            InitializeComponent();
            strokeColor.SelectedColor = ((SolidColorBrush)e.Stroke).Color;
            fillColor.SelectedColor = ((SolidColorBrush)e.Fill).Color;
            textColor.SelectedColor = ((SolidColorBrush)t.Foreground).Color;
            drawEllipseButton.Content = "Edit";
            AssociatedEllipse = e;
            AssociatedTextBlock = t;
            radiusX.Text = e.Width.ToString();
            radiusY.Text = e.Height.ToString();
            strokeThickness.Text = e.StrokeThickness.ToString();
            if (e.Opacity == 0.5) ellipseIsTransparent.IsChecked = true;
            else ellipseIsTransparent.IsChecked = false;
            textBox.Text = t.Text;
        }
        #endregion

        #region Methods
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void drawEllipseButton_Click(object sender, RoutedEventArgs e)
        {
            if(AssociatedEllipse != null)  //ako je razlicito od null, onda editujem elipsu
            {
                AssociatedEllipse.Fill = new SolidColorBrush(fillColor.SelectedColor ?? Colors.Black);
                AssociatedEllipse.Stroke = new SolidColorBrush(strokeColor.SelectedColor ?? Colors.Black);
                AssociatedEllipse.Width = double.Parse(radiusX.Text);
                AssociatedEllipse.Height = double.Parse(radiusY.Text);
                AssociatedEllipse.StrokeThickness = double.Parse(strokeThickness.Text);
                if ((bool)ellipseIsTransparent.IsChecked) AssociatedEllipse.Opacity = 0.5;
                else AssociatedEllipse.Opacity = 1;
                AssociatedTextBlock.Text = textBox.Text;
                AssociatedTextBlock.Foreground = new SolidColorBrush(textColor.SelectedColor ?? Colors.Black);
                Close();
                return;
            }

            if (fillColor.SelectedColor == null || strokeColor.SelectedColor == null || radiusX == null || radiusY == null
                || strokeThickness == null)
                return;

            try              //ovdje pravim novu elipsu 
            {
                FillColor = new SolidColorBrush(fillColor.SelectedColor ?? Colors.Black);  //ako je null stavice na crnu boju
                StrokeColor = new SolidColorBrush(strokeColor.SelectedColor ?? Colors.Black);
                Width = double.Parse(radiusX.Text);
                Height = double.Parse(radiusY.Text);
                StrokeThickness = double.Parse(strokeThickness.Text);
                if ((bool)ellipseIsTransparent.IsChecked) OpacityValue = 0.5;
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
