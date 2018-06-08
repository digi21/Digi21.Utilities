using System.Collections.Generic;
using Digi21.DigiNG.Entities;
using Digi21.DigiNG.IO;

namespace Digi21.Utilities
{
    /// <summary>
    /// Proporciona métodos de extensión para secuencias de UtilidadesSecuenciaReadOnlyFile
    /// </summary>
    public static class UtilidadesSecuenciaReadOnlyFile
    {
        /// <summary>
        /// Devuelve una secuencia con todas las entidades de los archivos pasados por parámetros.
        /// </summary>
        /// <param name="archivos">Archivos cuyas entidades queremos convertir en secuencia.</param>
        /// <returns>Secuencia de entidades que son la concatenación de todas las entidades de todos los archivos pasados por parámetro.</returns>
        public static IEnumerable<Entity> EnumeraEntidades(this IEnumerable<IReadOnlyDrawingFile> archivos)
        {
            foreach (var archivo in archivos)
                foreach(var entidad in archivo)
                    yield return entidad;
        }

        /// <summary>
        /// Devuelve una secuencia con todas las entidades de un determinado tipo (T) de los archivos pasados por parámetros.
        /// </summary>
        /// <param name="archivos">Archivos cuyas entidades queremos convertir en secuencia.</param>
        /// <typeparam name="T">Tipo de entidad a localizar.</typeparam>
        /// <returns>Secuencia de entidades que son la concatenación de todas las entidades de tipo (T) de todos los archivos pasados por parámetro.</returns>
        public static IEnumerable<T> EnumeraEntidades<T>(this IEnumerable<IReadOnlyDrawingFile> archivos)
            where T:class
        {
            foreach (var archivo in archivos)
            {
                foreach (var entidad in archivo)
                {
                    if( entidad is T )
                        yield return entidad as T;
                }
            }
        }
    }
}
