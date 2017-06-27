using CoreLocation;
using MapKit;
using System;
using UIKit;
using Xamarians.Maps;
using Xamarians.Maps.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;
using System.Collections.ObjectModel;
using Xamarin.Forms.Maps;
using System.Collections.Generic;
using System.Linq;

[assembly: ExportRenderer(typeof(ExtendedMap), typeof(ExtendedMapRenderer))]
namespace Xamarians.Maps.iOS
{
    public class ExtendedMapRenderer : MapRenderer
    {
        List<MKAnnotation> annotations = new List<MKAnnotation>();
        static ExtendedMap element;
        MKMapView nativeMap;
        //bool isLoaded = false;
        MapDelegate mapDelegate;
		static MKCircleRenderer circleRenderer;
		static MKPolylineRenderer polylineRenderer;
        bool isCircle;
		static bool isLoaded = false;

        public new static void Init()
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);
            if (isLoaded)
            {
                nativeMap.RemoveOverlays(nativeMap.Overlays);
            }

            if (e.NewElement == null)
                return;
            if (e.OldElement != null)
            {

                nativeMap = Control as MKMapView;

            }
            element = Element as ExtendedMap;
            mapDelegate = new MapDelegate();
            nativeMap = Control as MKMapView;
			nativeMap.Delegate = null;
            nativeMap.Delegate = mapDelegate;

            var formsMap = (ExtendedMap)e.NewElement;

			foreach (var circle in element.Circles)
            {
                var circleOverlay = MKCircle.Circle(new CLLocationCoordinate2D(circle.Position.Latitude, circle.Position.Longitude), circle.Radius);
                nativeMap.AddOverlay(circleOverlay);
            }
            CLLocationCoordinate2D[] coords = new CLLocationCoordinate2D[element.RouteCoordinates.Count];

            int index = 0;
            int idCounter = 1;
            string icon = "";
            icon = element.ImageSource;
            foreach (var position in element.RouteCoordinates)
            {
				var annot = new CustomAnnotation(new CLLocationCoordinate2D(position.Latitude, position.Longitude),element.CustomPins.FirstOrDefault().Label,"", false, icon);
                annot.Id = idCounter++;
                nativeMap.AddAnnotation(annot);
                //pinCollection.Add(annot.Id, item);
                annotations.Add(annot);
                coords[index] = new CLLocationCoordinate2D(position.Latitude, position.Longitude);
                index++;
            }

            var routeOverlay = MKPolyline.FromCoordinates(coords);
            nativeMap.AddOverlay(routeOverlay);
        }

        public class MapDelegate : MKMapViewDelegate
        {
            bool _regionChanged = false;
            public event EventHandler RegionChangedEvent;
            public event EventHandler<MKUserLocation> UserLocationChanged;
            protected string annotationId = "MapAnnotation";

            public override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
            {
                MKAnnotationView annotationView = null;
                if (annotation is MKUserLocation)
                    return null;
                var cAnnotation = annotation as CustomAnnotation;
                if (annotation is CustomAnnotation)
                {

                    // show conference annotation

                    annotationView = mapView.DequeueReusableAnnotation(annotationId);
                    if (annotationView == null)
                        annotationView = new MKAnnotationView(annotation, annotationId);
					
                    annotationView.CanShowCallout = true;


                    UIImage image = UIImage.FromBundle(((CustomAnnotation)annotation).Icon);
                    annotationView.Image = image;
                    var detailButton = UIButton.FromType(UIButtonType.InfoLight);
                    //// detailButton.SetImage(UIImage.FromFile("ic_lesson_hotspot_start.png"), UIControlState.Normal);
                    detailButton.TouchUpInside += (s, e) =>
                    {
                        if (cAnnotation.Id != 0)
                        {
                            element.HandleClick();
                        }

                    };

                    annotationView.RightCalloutAccessoryView = detailButton;
                }

                return annotationView;

            }

			public override MKOverlayRenderer OverlayRenderer(MKMapView mapView, IMKOverlay overlayWrapper)
			{
				var type = overlayWrapper.GetType();
				var overlay = overlayWrapper as IMKOverlay;
				if (overlay is MKPolyline || type == typeof(MKPolyline))
				{
					if (polylineRenderer == null)
					{

						polylineRenderer = new MKPolylineRenderer(overlay as MKPolyline);
						polylineRenderer.FillColor = UIColor.Blue;
						polylineRenderer.StrokeColor = UIColor.Red;
						polylineRenderer.LineWidth = 5;
						isLoaded = true;
					}
					return polylineRenderer;

				}
				else if (overlay is MKCircle)
				{
					if (circleRenderer == null)
					{

						circleRenderer = new MKCircleRenderer(overlay as MKCircle);
						circleRenderer.FillColor = UIColor.Red;
						circleRenderer.Alpha = 0.2f;
						isLoaded = true;
					}
					return circleRenderer;
				}
				return null;;
			}
        }



    }

    

        public class CustomAnnotation : MKAnnotation
        {
            string title, subtitle;
            CLLocationCoordinate2D coordinate;

            public EventHandler<CLLocationCoordinate2D> DragEnd;
            public EventHandler Clicked;

            public bool IsLocationIcon { get; set; }

            #region Overridden Properties


            public override CLLocationCoordinate2D Coordinate { get { return coordinate; } }
            public override string Title { get { return title; } }
            public override string Subtitle { get { return subtitle; } }

            #endregion
            public int Id { get; set; }
            public string Icon { get; set; }
            public bool IsDraggable { get; set; }


		    public CustomAnnotation(CLLocationCoordinate2D coordinate, string title, string subtitle,  bool locationIcon, string icon)
            {
                this.coordinate = coordinate;
                this.title = title;
                this.subtitle = subtitle;
                this.Icon = icon;
                this.IsLocationIcon = locationIcon;


            }

        }
}
