using System.Collections.Generic;
using System.Linq;
using Digi21.Math;

namespace Digi21.Utilities
{
    /// <summary>
    /// Proporciona métodos de extensión para secuencias Point2D
    /// </summary>
    public static class UtilidadesSecuenciaPoint2D
    {
        /// <summary>
        /// Indica si la secuencia contiene el punto pasado por parámetros.
        /// </summary>
        /// <param name="secuencia">Secuencia a analizar.</param>
        /// <param name="punto">Punto a localizar.</param>
        /// <returns>Verdadero si la secuencia contiene el punto pasado por parámetros.</returns>
        public static bool ContieneElPunto(this IEnumerable<Point2D> secuencia, Point2D punto)
        {
            return secuencia.Any(puntoEnSecuencia => punto == puntoEnSecuencia);
        }
    }
}
