using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Project
{
    /// <summary>
    /// Interaction logic for AddTextWindow.xaml
    /// </summary>
    public partial class AddTextWindow : Window
    {
        #region Properties
        public string TextContent { get; set; }
        private TextBlock AssociatedTextBlock = null;
        public double TextSize { get; set; }
        public SolidColorBrush TextColor { get; set; }
        #endregion

        #region Constructors
        public AddTextWindow()
        {
            InitializeComponent();
            textColor.SelectedColor = Colors.Black;
        }

        public AddTextWindow(TextBlock text)
        {
            InitializeComponent();
            AssociatedTextBlock = text;
            textBox.Text = text.Text;
            textColor.SelectedColor = ((SolidColorBrush)text.Foreground).Color;
            textSize.Text = text.FontSize.ToString();
            addTextButton.Content = "Edit";
        }
        #endregion

        #region Methods
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void addText_Click(object sender, RoutedEventArgs e)
        {
            if (AssociatedTextBlock != null)
            {
                AssociatedTextBlock.Text = textBox.Text;
                AssociatedTextBlock.Foreground = new SolidColorBrush(textColor.SelectedColor ?? Colors.Black);
                AssociatedTextBlock.FontSize = double.Parse(textSize.Text);
                Close();
                return;
            }
            if (string.IsNullOrEmpty(textBox.Text) || textColor.SelectedColor == null || textSize.Text == null)
                return;

            try
            {
                TextContent = textBox.Text;
                TextColor = new SolidColorBrush(textColor.SelectedColor ?? Colors.Black);
                TextSize = double.Parse(textSize.Text);
                if (TextSize < 0 || string.IsNullOrEmpty(TextContent))
                    throw new Exception();
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
