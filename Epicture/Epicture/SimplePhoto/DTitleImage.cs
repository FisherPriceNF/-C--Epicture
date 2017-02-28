using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Epicture
{
    class DTitleImage
    {
        private TextBox _title;
        private Grid _principal;

        public DTitleImage(String name, Grid parent, int row, SolidColorBrush color)
        {
            initTextBox();
            initGridPrincipal(name, parent, color, row);
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

        private void initTextBox()
        {
            this._title = new TextBox();
            this._title.Name = "title";
            this._title.TextWrapping = TextWrapping.Wrap;
            this._title.Text = "";
            this._title.Margin = new Thickness(20/*Left*/, 20/*Top*/, 20/*Right*/, 20/*Bottom*/);
            if (this._principal != null)
            {
                _principal.Children.Add(this._title);
            }
        }
    }
}
