using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Epicture
{
    class DBoxImage
    {
        private Image _image;
        private Grid _parent;
        private int _position;
        private string _nameGrid;
        private string _path;
        private string _name;
        private SolidColorBrush _color;
        private Grid _principal;

        public String NameGrid
        {
            get { return this._nameGrid; }
            set
            {
                this._principal.Name = value;
                this._image.Name = "image " + value;
                this._nameGrid = value;
            }
        }

        public Grid Parent
        {
            get { return this._parent; }
            set
            {
                this._parent = value;
                this._parent.Children.Add(this._principal);
            }
        }

        public int Position
        {
            get { return this._position; }
            set
            {
                this._position = value;
                Grid.SetRow(this._principal, this._position);
            }
        }

        public SolidColorBrush Color
        {
            get { return this._color; }
            set
            {
                this._color = value;
                this._principal.Background = this._color;
            }
        }

        public DBoxImage(String name, ref Grid parent, int row, SolidColorBrush color)
        {

            this._nameGrid = name;
            this._parent = parent;
            this._position = row;
            this._color = color;

            initGridPrincipal();
            initImage();
        }

        private void initGridPrincipal()
        {
            this._principal = new Grid();
            this._principal.Name = this._nameGrid;
            _principal.Background = this._color;

            if (this._parent != null)
            {
                Grid.SetRow(this._principal, this._position);
                this._parent.Children.Add(this._principal);
            }
        }

        private void initImage()
        {
            this._image = new Image();
            this._image.Name = "image " + this._nameGrid;
            //this._image.MaxWidth = this._principal.ActualWidth;
            //this._image.MaxHeight = this._principal.ActualHeight;
            this._image.Stretch = Stretch.Fill;
            this._image.VerticalAlignment = VerticalAlignment.Center;
            this._image.HorizontalAlignment = HorizontalAlignment.Center;
            if (this._principal != null)
            {
                _principal.Children.Add(this._image);
            }
        }

        public void display(string path)
        {
            this._image.Name = "image " + this._name;
            this._path = path;
            this._image.Source = new BitmapImage(new Uri(path));
        }
    }
}
