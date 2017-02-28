using System;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.UI.Xaml.Media.Imaging;
using System.Linq;
using Windows.UI;

namespace Epicture
{
    class DGallery
    {
        private Grid _principal;
        private ScrollViewer _scrollbar;
        private List<Photo> _gallery;
        private double _sizePhoto;
        private int _line;
        private int _column;
        private int _columMax;

        // --- Constructeur
        public DGallery(String name, ref Grid parent, int row, SolidColorBrush color)
        {
            _gallery = new List<Photo>();
            _line = 0;
            _column = 0;
            _columMax = 3;
            //_sizePhoto = 400;
            _sizePhoto = Window.Current.Bounds.Width / 3 * (4 / 3);
            Window.Current.SizeChanged += Current_SizeChanged;
            initGridPrincipal(name, ref parent, color, row);
            initScollbar(this._principal);
            addAllItems();


        }

        // Fonction Event sur un resize de la fenêtre.
        private void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            _sizePhoto = Window.Current.Bounds.Width / 3 * (4 / 3);
            this._principal.Height = (this._gallery.Count / _columMax + 1) * _sizePhoto;
            foreach (Photo item in this._gallery)
            {
                item.WidthPrincipal = Window.Current.Bounds.Width / 3;
                item.HeightPrincipal = Window.Current.Bounds.Width / 3;
            }
        }

        private void OnItemClick(object element, GridClickEventArgs e)
        {
            
        }

        // --- Fonction Initilisation
        private void initGridPrincipal(String name, ref Grid parent, SolidColorBrush color, int row)
        {
            this._principal = new Grid();
            this._principal.Name = name;
            _principal.Background = color;
            this._principal.Height = (this._gallery.Count / _columMax + 1) * _sizePhoto;
            this._principal.Width = double.NaN;

            ColumnDefinition Column1 = new ColumnDefinition();
            Column1.Width = new GridLength(1, GridUnitType.Star);
            _principal.ColumnDefinitions.Add(Column1);

            ColumnDefinition Column2 = new ColumnDefinition();
            Column2.Width = new GridLength(1, GridUnitType.Star);
            _principal.ColumnDefinitions.Add(Column2);

            ColumnDefinition Column3 = new ColumnDefinition();
            Column3.Width = new GridLength(1, GridUnitType.Star);
            _principal.ColumnDefinitions.Add(Column3);

            for (int i = 0; i < this._gallery.Count / _columMax + 1; i++)
            {
                addLigne();
            }

            if (parent != null)
            {
                Grid.SetRow(this._principal, row);
                parent.Children.Add(this._principal);
            }
        }

        private void initScollbar(Grid parent)
        {
            _scrollbar = new ScrollViewer();
            //_scrollbar.Orientation = Orientation.Vertical;
            _scrollbar.Width = 10;
            SolidColorBrush color = new SolidColorBrush();
            color.Color = Colors.Black;
            _scrollbar.Background = color;
            _scrollbar.Visibility = Visibility.Visible;
            _scrollbar.HorizontalAlignment = HorizontalAlignment.Right;
            _scrollbar.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            if (parent != null)
            {
                parent.Children.Add(_scrollbar);
            }
        }

        // Fonction pour ajouter une Photo.
        public void addPhoto(String name , String path)
        {
            if (this._gallery.Count % _columMax == 0)
            {
                addLigne();
                this._principal.Height = (this._gallery.Count / _columMax + 1) * _sizePhoto;
            }
            Photo _newPhoto = new Photo(name, path, ref this._principal);
            Grid.SetColumn(_newPhoto.getPrincipal(), _column);
            Grid.SetRow(_newPhoto.getPrincipal(), _line);
            _newPhoto.display(ref this._principal);

            Debug.WriteLine("column :" + _column + " |line : " + _line);
            _column++;
            if (_column % _columMax == 0)
            {
                _column= 0;
                _line++;
            }

            this._gallery.Add(_newPhoto);
        }

        private void addLigne()
        {
            RowDefinition newRow = new RowDefinition();
            newRow.Height = new GridLength(1, GridUnitType.Star);
            newRow.MinHeight = 100;
            _principal.RowDefinitions.Add(newRow);
            
        }

        private void addAllItems()
        {
            int _nbr_photo = 9;
            int nbr_choix = 0;
            int _nbr_random = 0;
            string _name = "Image ";
            string _lien = "";

            for (int i = 0; i <= _nbr_photo; i++)
            {
                Random r = new Random(20);
                _nbr_random = r.Next(0, nbr_choix);

                Debug.WriteLine("Choix lien: " + _nbr_random);
                switch (_nbr_random)
                {
                    case 1:
                        {
                            _lien = @"http://www.1001-votes.com/vote/1234fonds/pays-1376782006273-t.jpg";
                            break;
                        }
                    case 2:
                        {
                            _lien = @"http://pandoon.info/wp-content/uploads/2012/07/fond-ecran-paysage.jpg";
                            break;
                        }
                    case 3:
                        {
                            _lien = @"https://img.hebus.com/hebus_2016/11/07/preview/1478550210_19703.jpg";
                            break;
                        }
                    case 4:
                        {
                            _lien = @"https://img.hebus.com/hebus_2016/11/25/preview/1480098988_18317.jpg";
                            break;
                        }
                    case 5:
                        {
                            _lien = @"https://img.hebus.com/hebus_2011/07/14/preview/110714221335_45.jpg";
                            break;
                        }
                    default:
                        {
                            _lien = @"http://www.1001-votes.com/vote/1234fonds/pays-1376782006273-t.jpg";
                            break;
                        }
                }
                _name = "Image " + (i + 1).ToString();
                this.addPhoto(_name, _lien);
            }
       }
    }
}
