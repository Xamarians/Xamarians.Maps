using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarians.Maps;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Sample
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TestCircle : ContentPage
	{
		public TestCircle ()
		{
                    
			InitializeComponent ();
            //shape.CreateCircle(500, 37.79752, -122.40183);
            ObservableCollection<Position> RouteCoordinates = new ObservableCollection<Position>();
            RouteCoordinates.Add(new Position(37.785559, -122.396728));
            RouteCoordinates.Add(new Position(37.780624, -122.390541));
            RouteCoordinates.Add(new Position(37.777113, -122.394983));
            RouteCoordinates.Add(new Position(37.776831, -122.394627));
            RouteCoordinates.Add(new Position(37.785559, -122.396728));
            shape.CreatePolygon(RouteCoordinates);
            shape.SetMarkerIcon("hotspot.png", "");
            shape.PinClicked += Shape_PinClicked;
		}

        private void Shape_PinClicked(object sender, EventArgs e)
        {
            DisplayAlert("Success", "You just clicked a marker", "ok");
        }
    }
}
