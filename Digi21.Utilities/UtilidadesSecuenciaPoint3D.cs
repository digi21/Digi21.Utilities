using System;
using System.Collections.Generic;
using Digi21.Math;

namespace Digi21.Utilities
{
    public static class UtilidadesSecuenciaPoint3D
    {
        public static int IndexOf2D(this IEnumerable<Point3D> secuencia, Point2D punto)
        {
            if (secuencia == null)
                throw new ArgumentNullException(nameof(secuencia));

            var indice = 0;
            foreach (var vertice in secuencia)
            {
                if (vertice.X == punto.X && vertice.Y == punto.Y)
                    return indice;
                indice++;
            }
            return -1;
        }
    }
}
