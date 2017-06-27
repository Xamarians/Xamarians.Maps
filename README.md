# Xamarians.Maps
# Media
Cross platform library to display markers for locations, customization of marker image, Handle marker click, create polygon using list of locations and create circle.

First install package from nuget using following command -
## Install-Package Xamarians.Maps

You can integrate media tools in Xamarin Form application using following code:

 Shared Code -
 
```xaml
 xmlns:xamarians="clr-namespace:Xamarians.Maps;assembly=Xamarians.Maps"
```
 <xamarians:ExtendedMap x:Name="shape" Draw="True" />
 
```c#
using Xamarians.Maps;
```

```write this code in page constructor
```
...
            shape.CreateCircle(500, 37.79752, -122.40183);
            ObservableCollection<Position> RouteCoordinates = new ObservableCollection<Position>();
            RouteCoordinates.Add(new Position(37.785559, -122.396728));
            RouteCoordinates.Add(new Position(37.780624, -122.390541));
            RouteCoordinates.Add(new Position(37.777113, -122.394983));
            RouteCoordinates.Add(new Position(37.776831, -122.394627));
            RouteCoordinates.Add(new Position(37.785559, -122.396728));
            shape.CreatePolygon(RouteCoordinates);
            shape.SetMarkerIcon("hotspot.png", "");
            shape.PinClicked += Shape_PinClicked;
		
Android - in MainActivity file write below code -
```c#
 Xamarians.Maps.Droid.ExtendedMapRenderer.Init();
```

iOS - in AppDelegate file write below code -
```c#
 Xamarians.Maps.iOS.ExtendedMapRenderer.Init();
```
Note
```
Make sure your bundle identifier has access to map api's.
```
Write the Api key in Manifest file in Application Tag.
```
<meta-data android:name="com.google.android.geo.API_KEY" android:value="AIzaSyD5gI6RKUjJ-20MuGBpt8exJM5PfJEtQyA" />
```

