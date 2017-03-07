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
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Epicture.PageApp.PageGallery.GestionGallery
{
    class CPhoto
    {
        private Grid _principal;
        private TextBlock _title;
        private Image _image;
        private String _name;
        private String _path;
        private double[] _size;
        private MainPage _page;

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

        public Grid Principal
        {
            get
            {
                return _principal;
            }
        }

        public CPhoto(String name, String path, ref MainPage page)
        {
            this._name = name;
            this._path = path;
            this._page = page;
            this._size = new double[] { Window.Current.Bounds.Width / 3, Window.Current.Bounds.Width / 3 };
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

        // Initisalition page.
        private void initGridPrincipal()
        {
            this._principal = new Grid();
            this._principal.Name = this._name;
            this._principal.Width = _size[0];
            this._principal.Height = _size[1];
            this._principal.PointerPressed += ClickImage;

            SolidColorBrush color = new SolidColorBrush();
            color.Color = Color.FromArgb(255, 48, 139, 87);
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
            this._title.FontSize = 16;
            this._title.VerticalAlignment = VerticalAlignment.Top;
            this._title.HorizontalAlignment = HorizontalAlignment.Center;
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
            if (this._path != null)
            {
                Debug.WriteLine("Lien :" + _path);
                this._image.Source = new BitmapImage(new Uri(_path));
                this._image.Visibility = Visibility.Visible;
            }
            else { Debug.WriteLine("The path is null"); }
            if (this._principal != null)
            {
                _principal.Children.Add(this._image);
            }
        }

        // Evenement si on click sur une image.
        private void ClickImage(object sender, PointerRoutedEventArgs e)
        {
            Debug.WriteLine("J'ai click sur " + this._name + " de path :" + _path + " .");

            var Frame = Window.Current.Content as Frame;
            if (Frame != null)
            {
                SimplePhotoPage page = new SimplePhotoPage(this._name, this._path, ref this._page);
                Frame.Content = page;
                Window.Current.Content = Frame;
                if (Frame.Content == null)
                {
                    Frame.Navigate(typeof(SimplePhotoPage));
                }
            }
        }
    }
}
