using System;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Epicture
{
    class Photo
    {
        private Grid _principal;
        private Grid _grid;
        private TextBlock _title;
        private Image _image;
        private String _name;
        private String _path;
        private double[] _size;

        public double WidthPrincipal
        {
            set
            {
                this._principal.Width = value;
                this._size[0] = value;
            }  
        }

        public double HeightPrincipal
        {
            set
            {
                this._principal.Height = value;
                this._size[1] = value;
            }
        }

        public Photo(String name, String path, ref Grid Parent)
        {
            this._name = name;
            this._path = path;
            // Window.Current.Bounds.Height
            this._size = new double[] { Window.Current.Bounds.Width / 3, Window.Current.Bounds.Width / 3 };
            //this._size = new double[] { 300, 300};
            initGridPrincipal();
            initImage();
            initTextBox();
        }

        public String getName()
        {
            return this._name;
        }

        public Grid getPrincipal()
        {
            return this._principal;
        }

        public Grid display(ref Grid parent)
        {
            this._title.Text = this._name;
            this.LoadImage();
            
            if (parent != null)
            {
                parent.Children.Add(this._principal);
            }
            return this._principal;
        }

        private void initGridPrincipal()
        {
            this._principal = new Grid();
            this._principal.Name = this._name;
            this._principal.Width = _size[0];
            this._principal.Height = _size[1];

            SolidColorBrush color = new SolidColorBrush();
            Random r = new Random();
            byte red = (byte)r.Next(0, 255);
            byte green = (byte)r.Next(0, 255);
            byte blue = (byte)r.Next(0, 255);
            color.Color = Color.FromArgb(255, red, green, blue);
            this._principal.Background = color;

            RowDefinition rowBoxPhoto = new RowDefinition();
            rowBoxPhoto.Height = new GridLength(3, GridUnitType.Star);
            _principal.RowDefinitions.Add(rowBoxPhoto);

            RowDefinition rowTitle = new RowDefinition();
            rowTitle.Height = new GridLength(1, GridUnitType.Star);
            _principal.RowDefinitions.Add(rowTitle);
        }

        
        private void initTextBox()
        {
            this._title = new TextBlock();
            this._title.Name = "title " + this._name;
            this._title.FontFamily = new FontFamily("Segeo UI");
            this._title.TextWrapping = TextWrapping.Wrap;
            this._title.Text = this._name;
            this._title.Width = 100;
            this._title.Height = 20;
            this._title.FontSize = 16;
            this._title.VerticalAlignment = VerticalAlignment.Top;
            if (this._principal != null)
            {
                Grid.SetRow(this._title, 1);
                _principal.Children.Add(this._title);
            }
        }

        private void initImage()
        {
            this._image = new Image();
            this._image.Name = "image " + this._name;
            this._image.MaxWidth = _size[0];
            this._image.MaxHeight = _size[1] - 20;
            this._image.Stretch = Stretch.Fill;
            if (this._principal != null)
            {
                Grid.SetRow(this._image, 0);
                _principal.Children.Add(this._image);
            }
        }

        private async void LoadImage()
        {
            if (this._path != null)
            {
                this._image.Source = new BitmapImage(new Uri(_path));
                this._image.Visibility = Visibility.Visible;
            } 
            else
            {
                Debug.WriteLine("The path is null.");
            }
        }
    }
}
