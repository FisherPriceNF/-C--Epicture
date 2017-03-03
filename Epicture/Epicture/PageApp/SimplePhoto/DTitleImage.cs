using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Epicture.PageApp.SimplePhoto
{
    class DTitleImage
    {
        private TextBlock _title;
        private Grid _parent;
        private int _position;
        private string _nameGrid;
        private string _path;
        private SolidColorBrush _color;
        private Grid _principal;

        public String NameGrid
        {
            get { return this._nameGrid; }
            set
            {
                this._principal.Name = value;
                this._nameGrid = value;
            }
        }

        public Grid Parent
        {
            get
            {
                return this._parent;
            }

            set
            {
                this._parent = value;
                this._parent.Children.Add(this._principal);
            }
        }

        public int Position
        {
            get
            {
                return this._position;
            }

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

        public DTitleImage(String name, ref Grid parent, int row, SolidColorBrush color)
        {
            this._nameGrid = name;
            this._parent = parent;
            this._position = row;
            this._color = color;

            initGridPrincipal();
            initTextBox();
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

        private void initTextBox()
        {
            this._title = new TextBlock();
            this._title.Name = "title";
            this._title.FontFamily = new FontFamily("Segeo UI");
            this._title.TextWrapping = TextWrapping.Wrap;
            this._title.FontSize = 26;
            this._title.Text = "";
            this._title.FontStyle = FontStyle.Normal;
            this._title.FontWeight = FontWeights.SemiBold;
            this._title.VerticalAlignment = VerticalAlignment.Center;
            this._title.HorizontalAlignment = HorizontalAlignment.Center;
            
            if (this._principal != null)
            {
                _principal.Children.Add(this._title);
            }
        }

        public void setContent(string name)
        {
            this._title.Text = name;
        }
    }
}
