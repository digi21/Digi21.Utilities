using System;
using System.Collections.Generic;
using Digi21.Math;

namespace Digi21.Utilities
{
    /// <summary>
    /// Proporciona métodos de extensión para secuencias Point3D
    /// </summary>
    public static class UtilidadesSecuenciaPoint3D
    {
        /// <summary>
        /// Indica el índice del punbto pasado por parámetros.
        /// </summary>
        /// <param name="secuencia">Secuencia a analizar.</param>
        /// <param name="punto">Punto a localizar.</param>
        /// <returns>Índice del punto si éste está en la lista de coordenadas o -1 en caso contrario.</returns>
        public static int IndexOf2D(this IEnumerable<Point3D> secuencia, Point2D punto)
        {
            if (secuencia == null)
                throw new ArgumentNullException(nameof(secuencia));

            var indice = 0;
            foreach (var vertice in secuencia)
            {
                if (vertice.X.IsEqual(punto.X) && vertice.Y.IsEqual(punto.Y))
                    return indice;
                indice++;
            }
            return -1;
        }
    }
}
