using System;
using System.Collections.Generic;
using System.Linq;
using Digi21.DigiNG.Entities;
using Digi21.Utilities.Properties;

namespace Digi21.Utilities
{
    /// <summary>
    /// Proporciona métodos de utilidad estáticos para facilitar el desarrollo de aplicaciones con Digi3D.NET
    /// </summary>
    public static class UtilidadesDigiNG
    {
        /// <summary>
        /// Enumera todas las entidades cargadas en la ventana de dibujo, tanto del archivo de dibujo como de los archivos de referencia cargados.
        /// </summary>
        /// <returns>IEnumerable con todas las entidades.</returns>
        public static IEnumerable<Entity> EnumeraTodasLasEntidades()
        {
            foreach(var entidad in DigiNG.DigiNG.DrawingFile)
                yield return entidad;

            foreach (var archivo in DigiNG.DigiNG.ReferenceFiles)
                foreach (var entidad in archivo)
                    yield return entidad;
        }

        /// <summary>
        /// Devuelve el índice del archivo de dibujo propietario de la entidad pasada por parámetros.
        /// </summary>
        /// <param name="entidad">Entidad para la cual queremos saber a qué archivo de dibujo pertenece.</param>
        /// <returns>Índice al archivo de dibujo al que pertenece la entidad.</returns>
        /// <exception cref="Exception">En caso de que la entidad no pertenezca a ningún archivo de dibujo, se lanza una excepción.</exception>
        public static int ArchivoPropietarioDeEntidad(Entity entidad)
        {
            var activo = DigiNG.DigiNG.DrawingFile;
            if (activo.Contains(entidad))
                return 0;

            // Cada lectura de ReferenceFiles crea objetos nuevos, así que los archivos se comparan por ruta: el Owner de una
            // entidad de un archivo de referencia es un DrawingFile, no el objeto de ReferenceFiles.
            var referencias = DigiNG.DigiNG.ReferenceFiles;
            var propietario = entidad.Owner;
            for (var i = 0; i < referencias.Length; i++)
            {
                if (propietario != null
                    ? string.Equals(propietario.Path, referencias[i].Path, StringComparison.OrdinalIgnoreCase)
                    : referencias[i].Any(e => ReferenceEquals(e, entidad)))
                    return 1 + i;
            }

            if (propietario == null && activo.Any(e => ReferenceEquals(e, entidad)))
                return 0;

            throw new Exception(Recursos.EntidadNoPerteneceNingunArchivo);
        }
    }
}
