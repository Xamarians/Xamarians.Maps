using System.Collections.Generic;
using Xamarin.Forms.Maps.Android;
using Android.Gms.Maps;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Platform.Android;
using Android.Gms.Maps.Model;
using Xamarin.Forms;
using Xamarians.Maps.Droid;
using Xamarians.Maps;
using System;
using System.Collections.ObjectModel;
using Android.Graphics;
using Android.Graphics.Drawables;
using System.Reflection;

[assembly: ExportRenderer(typeof(ExtendedMap), typeof(ExtendedMapRenderer))]
namespace Xamarians.Maps.Droid
{
    public class ExtendedMapRenderer : MapRenderer, IOnMapReadyCallback, IMap
    {
        ExtendedMap element;
        GoogleMap map;

        //bool isLoaded = false;
        ObservableCollection<Position> routeCoordinates;
        //bool isCircle;
        // ExtendedMap formsMap;
        //Circle circle;

        public static void Init()
        {
            var tt = new ExtendedMap();
        }

        protected override void OnMapReady(GoogleMap googleMap)
        {
            map = googleMap;
            //element.SetNativeContext(this);
            map.MarkerClick += Map_MarkerClick;
            CreatePolygon(element.RouteCoordinates);
            foreach (var circle in element.Circles)
            {
                CreateCircle(circle);
            }
            element.Circles.CollectionChanged += (ss, ee) =>
            {
                if (ee.NewItems == null)
                    return;
                foreach (Circle circle in ee.NewItems)
                {
                    CreateCircle(circle);
                }
            };
            //map.Clear();
            var bd = Forms.Context.Resources.GetDrawable(element.ImageSource) as BitmapDrawable;
            foreach (var pin in element.CustomPins)
            {
                var marker = new MarkerOptions();
                marker.SetPosition(new LatLng(pin.Position.Latitude, pin.Position.Longitude));
                marker.SetTitle(pin.Label);
                marker.SetSnippet(pin.Address);
                if (bd != null && bd.Bitmap != null)
                    marker.SetIcon(BitmapDescriptorFactory.FromBitmap(bd.Bitmap));
                map.AddMarker(marker);
            }
            bd.Dispose();
        }

        private void Map_MarkerClick(object sender, GoogleMap.MarkerClickEventArgs e)
        {
            if (e.Marker == null)
                return;
            element.HandleClick();
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement == null)
                return;
            element = Element as ExtendedMap;
        }

        public void CreateCircle(Circle circle)
        {
            if (circle == null)
                return;
            var circleOptions = new CircleOptions();
            circleOptions.InvokeCenter(new LatLng(circle.Position.Latitude, circle.Position.Longitude));
            circleOptions.InvokeRadius(circle.Radius);
            circleOptions.InvokeFillColor(0X00FFFFFF);
            circleOptions.InvokeStrokeColor(0X66FF0000);
            circleOptions.InvokeStrokeWidth(3);
            map.AddCircle(circleOptions);
        }

        public void CreatePolygon(ObservableCollection<Position> RouteCoordinates)
        {
            routeCoordinates = RouteCoordinates;
            var polylineOptions = new PolylineOptions();
            polylineOptions.InvokeColor(0x66FF0000);

            foreach (var position in routeCoordinates)
            {

                polylineOptions.Add(new LatLng(position.Latitude, position.Longitude));
            }

            map.AddPolyline(polylineOptions);
        }


    }
}