using System.Collections.Generic;
using System.Linq;
using Digi21.Math;
using Digi21.DigiNG.Entities;

namespace Digi21.Utilities
{
    /// <summary>
    /// Proporciona métodos de extensión para secuencias de intersecciones
    /// </summary>
    public static class UtilidadesSecuenciaIntersecciones
    {
        /// <summary>
        /// Devuelve todas las intersecciones que pertenecen a una determinada línea pasada por parámetros para un determinado segmento de dicha línea.
        /// </summary>
        /// <param name="secuencia">Secuencia de intersecciones a analizar.</param>
        /// <param name="línea">Línea a buscar en la secuencia de intersecciones.</param>
        /// <param name="númeroSegmento">Número de segmento a localizar para la línea a buscar en la secuencia de intersecciones.</param>
        /// <returns>Enumeración con las intersecciones que tiene el segmento de la línea pasados por parámetros en la secuencia original.</returns>
        public static IEnumerable<IGrouping<Point2D, SegmentPointer>> SoloDeSegmento(this IEnumerable<IGrouping<Point2D, SegmentPointer>> secuencia, ReadOnlyLine línea, int númeroSegmento )
        {
            return 
                from intersección in secuencia 
                from segmento in intersección 
                where segmento.Line == línea && segmento.FirstVertex == númeroSegmento 
                select intersección;
        }
    }
}
