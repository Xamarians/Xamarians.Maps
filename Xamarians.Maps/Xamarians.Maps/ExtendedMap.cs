using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace Xamarians.Maps
{
    public class ExtendedMap : Map
    {
        IMap _map;
        public bool Draw { get; set; }
        public string ImageSource;
        public string PackageName;
        public ObservableCollection<Circle> Circles = new ObservableCollection<Circle>();
        public ObservableCollection<Position> RouteCoordinates = new ObservableCollection<Position>();
        public ObservableCollection<Pin> CustomPins = new ObservableCollection<Pin>();
        public event EventHandler PinClicked;


        public ExtendedMap()
        {
            //RouteCoordinates = new List<Position>();
            //Circle(1000, 37.79752, -122.40183);
        }

        internal void SetNativeContext(IMap shape)
        {
            _map = shape;
        }

        #region Methods


        public void CreateCircle(int radius, double XCoordinate, double YCoordinate)
        {
            Circles.Add(new Circle
            {
                Radius = radius,
                Position = new Position(XCoordinate, YCoordinate)
            });

            var pin = new Pin
            {
                Type = PinType.Place,
                Position = new Position(XCoordinate, YCoordinate),
                Label = "Pin",
                Address = "1"
            };

            var position = new Position(XCoordinate, YCoordinate);

            CustomPins.Add(pin);
            //Pins.Add(pin);
            MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(1.0)));
            //_map?.Circle();
        }
        public void CreatePolygon(ObservableCollection<Position> routeCoordinates)
        {
            RouteCoordinates = routeCoordinates;
            foreach (var coordinate in RouteCoordinates)
            {
                //Pins.Add(new Pin
                //{
                //    Position = coordinate,
                //    Label ="pin"
                //});
                CustomPins.Add(new Pin
                {
                    Position = coordinate,
                    Label = "pin"
                });

            }

            MoveToRegion(MapSpan.FromCenterAndRadius(new Position(37.79752, -122.40183), Distance.FromMiles(1.0)));
          
        }
        public void SetMarkerIcon(string resource, string packageName)
        {
            ImageSource = resource;
            PackageName = packageName;

        }

        public void HandleClick()
        {
            if(PinClicked!=null)
            {
                PinClicked.Invoke(this, EventArgs.Empty);
            }
        }



    }
}
#endregion