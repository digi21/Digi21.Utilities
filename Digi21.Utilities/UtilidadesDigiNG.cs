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
            if (DigiNG.DigiNG.DrawingFile.Contains(entidad))
                return 0;

            for (var i = 0; i < DigiNG.DigiNG.ReferenceFiles.Length; i++)
                if (DigiNG.DigiNG.ReferenceFiles[i].Contains(entidad))
                    return 1 + i;

            throw new Exception(Recursos.EntidadNoPerteneceNingunArchivo);
        }
    }
}
