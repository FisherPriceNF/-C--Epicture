using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Epicture.PageApp.SimplePhoto
{
    class DBoxFavori
    {
        private Button _favori;
        private Button _precedent;
        private Grid _principal;
        private String _nameGrid;
        private Grid _parent;
        private int _position;
        private SolidColorBrush _color;
        private MainPage _page;
        private bool _etatfavori;

        // --- Propriète.
        public String Name
        {
            get { return this._nameGrid; }
            set {
                this._principal.Name = value;
                this._nameGrid = value;
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
        public DBoxFavori(String name, ref Grid parent, int position, SolidColorBrush color, ref MainPage page)
        {
            this._nameGrid = name;
            this._parent = parent;
            this._position = position;
            this._color = color;
            this._page = page;
            initGridPrincipal();
            initButtonFavori();
            initButtonPrecedent();
        }

        // --- Fonction initialisation
        private void initGridPrincipal()
        {
            this._principal = new Grid();
            this._principal.Name = this._nameGrid;
            this._principal.Background = this._color;
            this._etatfavori = false;

            if (this._parent != null)
            {
                Grid.SetRow(this._principal, this._position);
                this._parent.Children.Add(this._principal);
            }
        }

        private void initButtonFavori()
        {
            this._favori = new Button();
            this._favori.Name = "btn_favori";
            this._favori.Content = "Favori";
            Image image = new Image();
            image.Source = new BitmapImage(new Uri("http://icon-icons.com/icons2/935/PNG/512/favourites-star-outline-interface-symbol_icon-icons.com_73254.png"));
            this._favori.Content = image;
            this._favori.VerticalAlignment = VerticalAlignment.Center;
            this._favori.HorizontalAlignment = HorizontalAlignment.Right;
            this._favori.Click += _favori_Click;
            if (this._principal != null)
            {
                _principal.Children.Add(this._favori);
            }
        }

        private void initButtonPrecedent()
        {
            this._precedent = new Button();
            this._precedent.Name = "btn_favori";
            this._precedent.Content = "Favori";
            Image image = new Image();
            image.Source = new BitmapImage(new Uri("http://i-dollz.com/perso/img/icone_prev.png"));
            this._precedent.Content = image;
            this._precedent.VerticalAlignment = VerticalAlignment.Center;
            this._precedent.HorizontalAlignment = HorizontalAlignment.Left;
            this._precedent.Click += _precedent_Click;
            if (this._principal != null)
            {
                _principal.Children.Add(this._precedent);
            }
        }

        private void _precedent_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Button precedent Click a été click.");

            var Frame = Window.Current.Content as Frame;
            if (Frame != null)
            {
                Frame.Content = this._page;
                if (Frame.Content == null)
                {
                    Frame.Navigate(typeof(MainPage));
                }
            }
        }

        private void _favori_Click(object sender, RoutedEventArgs e)
        {
            Image image = new Image();
            if (_etatfavori == false)
            {
                image.Source = new BitmapImage(new Uri("http://icon-icons.com/icons2/935/PNG/512/favourites-star-outline-interface-symbol_icon-icons.com_73254.png"));
                this._favori.Content = image;
                _etatfavori = true;
            }
            else if (_etatfavori == true)
            {
                image.Source = new BitmapImage(new Uri("http://fr.seaicons.com/wp-content/uploads/2016/09/Places-favorites-icon.png"));
                this._favori.Content = image;
                _etatfavori = false;
            }
            Debug.WriteLine("Button favori Click a été click.");
        }
    }
}
