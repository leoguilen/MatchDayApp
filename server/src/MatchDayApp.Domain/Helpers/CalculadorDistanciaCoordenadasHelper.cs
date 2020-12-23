using System;

namespace MatchDayApp.Domain.Helpers
{
    public static class CalculadorDistanciaCoordenadasHelper
    {
        private const double R = 6371e3;

        public static double CalcularParaMetros(double lat1 = 0, double long1 = 0,
            double lat2 = 0, double long2 = 0)
        {
            double radianoLat1 = lat1 * Math.PI / 180;
            double radianoLat2 = lat1 * Math.PI / 180;
            double latCalc = (lat2 - lat1) * Math.PI / 180;
            double longCalc = (long2 - long1) * Math.PI / 180;

            double a = Math.Sin(latCalc / 2) * Math.Sin(latCalc / 2) + Math.Cos(radianoLat1) * Math.Cos(radianoLat2) * Math.Sin(longCalc / 2) * Math.Sin(longCalc / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return Math.Floor(R * c);
        }

        public static bool EhProximo(double lat1 = 0, double long1 = 0, double lat2 = 0, double long2 = 0) =>
            CalcularParaMetros(lat1, long1, lat2, long2) > 10000 ? false : true;
    }
}
