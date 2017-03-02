using System;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;

namespace Epicture
{
    class DGallery
    {
        private Grid _principal;
        private ObservableCollection<Grid> _list;
        private GridView _gridView;
        private MainPage _page;
        private Grid _parent;
        private SolidColorBrush _color;
        private int _position;
        private string nameGrid;

        // --- Constructeur
        public DGallery(String name, ref Grid parent, int row, SolidColorBrush color, MainPage page)
        {

            this.nameGrid = name;
            this._parent = parent;
            this._position = row;
            this._color = color;
            this._page = page;
            initGridPrincipal();
            _list = new ObservableCollection<Grid>();
            initGridView();
            addAllItems();
        }

        public void addPhoto(String name, String path)
        {
            Photo newPhoto = new Photo(name, path, ref this._page);
            _list.Add(newPhoto.Principal);
        }

        // Fonction Event sur un resize de la fenêtre.
        /*private void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            _sizePhoto = Window.Current.Bounds.Width / 3 * (4 / 3);
            this._principal.Height = (this._gallery.Count / _columMax + 1) * _sizePhoto;
            foreach (Photo item in this._gallery)
            {
                item.WidthPrincipal = Window.Current.Bounds.Width / 3;
                item.HeightPrincipal = Window.Current.Bounds.Width / 3;
            }
        }*/

        private void initGridView()
        {
            _gridView = new GridView();
            _gridView.Name = this.nameGrid;
            _gridView.Background = this._color;
            _gridView.ItemsSource = this._list;
            _gridView.VerticalAlignment = VerticalAlignment.Stretch;
            _gridView.HorizontalAlignment = HorizontalAlignment.Stretch;

            if (this._principal != null)
            {
                this._principal.Children.Add(_gridView);
            }
        }

        // --- Fonction Initilisation
        private void initGridPrincipal()
        {
            this._principal = new Grid();
            this._principal.Name = this.nameGrid;
            _principal.Background = this._color;
            this._principal.Width = double.NaN;

            if (this._parent != null)
            {
                Grid.SetRow(this._principal, this._position);
                this._parent.Children.Add(this._principal);
            }
        }

        private void addAllItems()
        {
            int _nbr_photo = 10;
            int nbr_choix = 6;
            int _nbr_random = 0;
            string _name = "Image ";
            string _lien = "";

            for (int i = 0; i <= _nbr_photo; i++)
            {
                int graine = DateTime.Now.Millisecond;
                Random r = new Random(graine);
                _nbr_random = r.Next(nbr_choix);

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
                    case 6:
                        {
                            _lien = @"http://www.fond-ecran-hd.net/pc-driver/1216.jpg";
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
