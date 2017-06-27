using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace Xamarians.Maps
{
    public interface IMap
    {
        void CreateCircle(Circle circle);
        void CreatePolygon(ObservableCollection<Position> routeCoordinates);
       
    }
}
