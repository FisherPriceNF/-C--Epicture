using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Epicture
{
    class DBoxFavori
    {
        private Button _favori;
        private Grid _principal;
        private String _name;
        private Grid _parent;
        private int _position;
        private SolidColorBrush _color;

        // --- Propriète.
        public String Name
        {
            get { return this._name; }
            set {
                this._principal.Name = value;
                this._name = value;
                }
        }

        public Grid Parent
        {
            get { return this._parent; }
            set {
                this._parent = value;
                this._parent.Children.Add(this._principal);
            }
        }

        public int Position
        {
            get { return this._position; }
            set {
                this._position = value;
                Grid.SetRow(this._principal, this._position);
            }
        }

        public SolidColorBrush Color
        {
            get { return this._color; }
            set {
                this._color = value;
                _principal.Background = this._color;
                }
        }

        public Grid Principal
        {
            get { return this._principal; }
            set {
                this._principal = value;
                }
        }

        // --- Constructeur
        public DBoxFavori(String name, Grid parent, int position, SolidColorBrush color)
        {
            this._name = name;
            this._parent = parent;
            this._position = position;
            this._color = color;
            initGridPrincipal();
            initButtom();
        }

        // --- Fonction initialisation
        private void initGridPrincipal()
        {
            this._principal = new Grid();
            this._principal.Name = this._name;
            _principal.Background = this._color;

            if (this._parent != null)
            {
                Grid.SetRow(this._principal, this._position);
                this._parent.Children.Add(this._principal);
            }
        }

        private void initButtom()
        {
            this._favori = new Button();
            this._favori.Name = "btn_favori";
            this._favori.Content = "Favori";
            this._favori.VerticalAlignment = VerticalAlignment.Center;
            this._favori.HorizontalAlignment = HorizontalAlignment.Center;
            this._favori.Click += _favori_Click;
            if (this._principal != null)
            {
                Grid.SetColumn(this._favori, 2);
                _principal.Children.Add(this._favori);
            }
        }

        private void _favori_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Button favori Click a été click.");
        }
    }
}
