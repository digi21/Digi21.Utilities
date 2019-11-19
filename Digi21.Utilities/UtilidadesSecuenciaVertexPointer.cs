using System.Collections.Generic;
using System.Linq;
using Digi21.DigiNG.Entities;

namespace Digi21.Utilities
{
    /// <summary>
    /// Proporciona métodos de extensión para secuencias de VertexPointer
    /// </summary>
    public static class UtilidadesSecuenciaVertexPointer
    {
        /// <summary>
        /// Indica si una determinada línea llega a un nodo de la secuencia original.
        /// </summary>
        /// <param name="secuencia">Secuencia a analizar.</param>
        /// <param name="line">Línea a localizar.</param>
        /// <returns></returns>
        public static bool LíneaLlegaANodo(this IEnumerable<VertexPointer> secuencia, ReadOnlyLine line)
        {
            return secuencia.Any(vp => vp.Line == line);
        }
    }
}
