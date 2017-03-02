using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml;
using System;
using System.Diagnostics;
using Windows.UI;
using Windows.UI.Xaml.Media.Imaging;

namespace Epicture
{
    class DBoxSearch
    {
       private Grid _principal;
       private TextBox _text_search;
       private Button _btn_search;

        // --- Propriète
        public Grid Principal
        {
            get { return this._principal; }
            set {
                if (value != null)
                {
                    this._principal = value;
                    this._principal.Children.Add(this._text_search);
                    this._principal.Children.Add(this._btn_search);
                }
            }
        }

        // --- Constructeur 
        public DBoxSearch(String name, ref Grid parent, int row, SolidColorBrush color)
        {
            iniGridPrincipal(name, ref parent, color, row);
            initTextBox();
            initButtom();
        }

        // --- Fonction Initilisation.
        private void iniGridPrincipal(String name, ref Grid parent, SolidColorBrush color, int row)
        {
            this._principal = new Grid();
            this._principal.Name = name;
            _principal.Background = color;

            ColumnDefinition rowTextSearch = new ColumnDefinition();
            rowTextSearch.Width = new GridLength(100, GridUnitType.Star);
            _principal.ColumnDefinitions.Add(rowTextSearch);

            ColumnDefinition rowVide = new ColumnDefinition();
            rowVide.Width = new GridLength(1, GridUnitType.Star);
            _principal.ColumnDefinitions.Add(rowVide);

            ColumnDefinition rowBtnSearch = new ColumnDefinition();
            rowBtnSearch.Width = new GridLength(10, GridUnitType.Star);
            _principal.ColumnDefinitions.Add(rowBtnSearch);

            if (parent != null)
            {
                Grid.SetRow(this._principal, row);
                parent.Children.Add(this._principal);
            }
        }

        private void initTextBox()
        {
            this._text_search = new TextBox();
            this._text_search.Name = "text_search";
            this._text_search.FontFamily = new FontFamily("Segeo UI");
            this._text_search.TextWrapping = TextWrapping.Wrap;
            this._text_search.TextAlignment = TextAlignment.Justify;
            this._text_search.Text = "";
            this._text_search.Height = 33;
            this._text_search.Margin = new Thickness(20/*Left*/, 20/*Top*/, 20/*Right*/, 17/*Bottom*/);
            SolidColorBrush colorFondText = new SolidColorBrush();
            colorFondText.Color = Color.FromArgb(255, 48, 139, 87);
            this._text_search.Background = colorFondText;
            this._text_search.VerticalAlignment = VerticalAlignment.Center;
            if (this._principal != null)
            {
                Grid.SetColumn(this._text_search, 0);
                _principal.Children.Add(this._text_search);
            }
        }

        private void initButtom()
        {
            this._btn_search = new Button();
            this._btn_search.Name = "btn_search";
            this._btn_search.Click += _btn_search_Click;
            Image image = new Image();
            image.Source = new BitmapImage(new Uri("https://cdn.pixabay.com/photo/2014/04/03/00/39/magnifying-308995_960_720.png"));
            this._btn_search.Content = image;
            this._btn_search.VerticalAlignment = VerticalAlignment.Center;
            this._btn_search.HorizontalAlignment = HorizontalAlignment.Center;

            this._btn_search.Foreground = new SolidColorBrush(Windows.UI.Colors.Beige);
            if (this._principal != null)
            {
                Grid.SetColumn(this._btn_search, 2);
                _principal.Children.Add(this._btn_search);
            }
        }

        private void _btn_search_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Button search Click a été click.");
            if (this._text_search.Text != "")
            {
                Debug.WriteLine("Info search: " + this._text_search.Text);
            }
        }
    }
}
