using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Epicture
{
    class DBoxImage
    {
        private Image _image;
        private Grid _principal;

        public DBoxImage(String name, Grid parent, int row, SolidColorBrush color)
        {
            initGridPrincipal(name, parent, color, row);
            initImage();
        }

        private void initGridPrincipal(String name, Grid parent, SolidColorBrush color, int row)
        {
            this._principal = new Grid();
            this._principal.Name = name;
            _principal.Background = color;

            if (parent != null)
            {
                Grid.SetRow(this._principal, row);
                parent.Children.Add(this._principal);
            }
        }

        private void initImage()
        {
            this._image = new Image();
            this._image.Name = "image";
            this._image.VerticalAlignment = VerticalAlignment.Center;
            this._image.HorizontalAlignment = HorizontalAlignment.Center;
            if (this._principal != null)
            {
                _principal.Children.Add(this._image);
            }
        }
    }
}
