using System;
using System.Collections.Generic;
using System.Linq;
using Digi21.DigiNG.Entities;

namespace Digi21.Utilities
{
    /// <summary>
    /// Define una serie de métodos de extensión para secuencia de entidades.
    /// </summary>
    public static class UtilidadesSecuenciaEntity
    {
        /// <summary>
        /// Devuelve una secuencia de entidades clonadas de la secuencia sobre la que se ejecuta el método de extensión.
        /// </summary>
        /// <param name="secuenciaOriginal">Secuencia con entidades sobre la que se aplica este método de extensión.</param>
        /// <returns>Secuencia de entidades cuya longitud coincidirá con la de la secuencia original con copias (clones) de las entidades en la secuencia original.</returns>
        public static IEnumerable<Entity> SecuenciaDeClones(this IEnumerable<Entity> secuenciaOriginal)
        {
            return from entidad in secuenciaOriginal
                   select entidad.Clone();
        }

        /// <summary>
        /// Devuelve el subconjunto de entidades enumeradas en la secuencia original que son de tipo ReadOnlyLine.
        /// </summary>
        /// <param name="secuenciaOriginal">Secuencia con entidades sobre la que se aplica este método de extensión.</param>
        /// <returns>Secuencia de entidades que son de tipo ReadOnlyLine</returns>
        [Obsolete("Utilizar mejor OfType<T>", false)]
        public static IEnumerable<ReadOnlyLine> SoloLíneas(this IEnumerable<Entity> secuenciaOriginal)
        {
            return from entidad in secuenciaOriginal
                   where entidad is ReadOnlyLine
                   select entidad as ReadOnlyLine;
        }

        /// <summary>
        /// Devuelve el subconjunto de entidades enumeradas en la secuencia original que son de tipo ReadOnlyPoint.
        /// </summary>
        /// <param name="secuenciaOriginal">Secuencia con entidades sobre la que se aplica este método de extensión.</param>
        /// <returns>Secuencia de entidades que son de tipo ReadOnlyPoint</returns>
        [Obsolete("Utilizar mejor OfType<T>", false)]
        public static IEnumerable<ReadOnlyPoint> SoloPuntos(this IEnumerable<Entity> secuenciaOriginal)
        {
            return from entidad in secuenciaOriginal
                   where entidad is ReadOnlyPoint
                   select entidad as ReadOnlyPoint;
        }

        /// <summary>
        /// Devuelve el subconjunto de entidades enumeradas en la secuencia original que son de tipo ReadOnlyText.
        /// </summary>
        /// <param name="secuenciaOriginal">Secuencia con entidades sobre la que se aplica este método de extensión.</param>
        /// <returns>Secuencia de entidades que son de tipo ReadOnlyText</returns>
        [Obsolete("Utilizar mejor OfType<T>", false)]
        public static IEnumerable<ReadOnlyText> SoloTextos(this IEnumerable<Entity> secuenciaOriginal)
        {
            return from entidad in secuenciaOriginal
                   where entidad is ReadOnlyText
                   select entidad as ReadOnlyText;
        }

        /// <summary>
        /// Devuelve el subconjunto de entidades enumeradas en la secuencia original que son de tipo ReadOnlyPolygon.
        /// </summary>
        /// <param name="secuenciaOriginal">Secuencia con entidades sobre la que se aplica este método de extensión.</param>
        /// <returns>Secuencia de entidades que son de tipo ReadOnlyPolygon</returns>
        [Obsolete("Utilizar mejor OfType<T>", false)]
        public static IEnumerable<ReadOnlyPolygon> SoloPolígonos(this IEnumerable<Entity> secuenciaOriginal)
        {
            return from entidad in secuenciaOriginal
                   where entidad is ReadOnlyPolygon
                   select entidad as ReadOnlyPolygon;
        }

        /// <summary>
        /// Devuelve el subconjunto de entidades enumeradas en la secuencia original que son de tipo ReadOnlyComplex.
        /// </summary>
        /// <param name="secuenciaOriginal">Secuencia con entidades sobre la que se aplica este método de extensión.</param>
        /// <returns>Secuencia de entidades que son de tipo ReadOnlyComplex</returns>
        [Obsolete("Utilizar mejor OfType<T>", false)]
        public static IEnumerable<ReadOnlyComplex> SoloComplejos(this IEnumerable<Entity> secuenciaOriginal)
        {
            return from entidad in secuenciaOriginal
                   where entidad is ReadOnlyComplex
                   select entidad as ReadOnlyComplex;
        }

        /// <summary>
        /// Devuelve el subconjunto de entidades enumeradas en la secuencia original que solapan con la ventana pasada por parámetros.
        /// </summary>
        /// <param name="secuenciaOriginal">Enumeración de entidades a analizar.</param>
        /// <param name="wnd">Ventana contra la cual se quiere analizar el solape.</param>
        /// <typeparam name="T">Tipo de entidad para el cual está especializado este método.</typeparam>
        /// <returns>Enumeración de entidades que solapan con la ventana pasada por parámetros.</returns>
        public static IEnumerable<T> QueSolapanCon<T>(this IEnumerable<T> secuenciaOriginal, Math.IWindow2D wnd )
            where T : Entity
        {
            return from entidad in secuenciaOriginal
                   where Math.Window2D.Intersects(entidad, wnd)
                   select entidad;
        }

        /// <summary>
        /// Devuelve el subconjunto de entidades enumeradas en la secuencia original que tienen el código pasado por parámetros.
        /// </summary>
        /// <param name="secuenciaOriginal">Secuencia con entidades sobre la que se aplica este método de extensión.</param>
        /// <param name="código">Código a localizar.</param>
        /// <returns>Subconjunto de entidades de la secuencia sobre la que se aplica el método de extensión que tienen el código pasado por parámetros.</returns>
        public static IEnumerable<T> QueTenganElCódigo<T>(this IEnumerable<T> secuenciaOriginal, string código)
            where T : Entity
        {
            return from entidad in secuenciaOriginal
                   where entidad.TieneElCódigo(código)
                   select entidad;
        }

        /// <summary>
        /// Devuelve el subconjunto de entidades enumeradas en la secuencia original que no tienen el código pasado por parámetros.
        /// </summary>
        /// <param name="secuenciaOriginal">Secuencia con entidades sobre la que se aplica este método de extensión.</param>
        /// <param name="código">Código a localizar.</param>
        /// <typeparam name="T">Tipo de entidad para el cual está especializado este método</typeparam>
        /// <returns>Subconjunto de entidades de la secuencia sobre la que se aplica el método de extensión que no tienen el código pasado por parámetros.</returns>
        public static IEnumerable<T> QueNoTenganElCódigo<T>(this IEnumerable<T> secuenciaOriginal, string código)
            where T : Entity
        {
            return from entidad in secuenciaOriginal
                   where !entidad.TieneElCódigo(código)
                   select entidad;
        }

        /// <summary>
        /// Devuelve el subconjunto de entidades enumeradas en la secuencia original que tienen el código pasado por parámetros.
        /// </summary>
        /// <remarks>
        /// Esta versión del método tiene en cuenta comodines, de modo que si la entidad tiene el código 020012 e intentamos comprobar si dispone del código 02* se devolverá verdadero.
        /// </remarks>
        /// <param name="secuenciaOriginal">Secuencia con entidades sobre la que se aplica este método de extensión.</param>
        /// <param name="código">Código a localizar.</param>
        /// <returns>Subconjunto de entidades de la secuencia sobre la que se aplica el método de extensión que tienen el código pasado por parámetros.</returns>
        public static IEnumerable<T> QueTenganElCódigoConComodín<T>(this IEnumerable<T> secuenciaOriginal, string código)
            where T : Entity
        {
            return from entidad in secuenciaOriginal
                   where entidad.TieneElCódigoConComodín(código)
                   select entidad;
        }

        /// <summary>
        /// Devuelve el subconjunto de entidades enumeradas en la secuencia original que tienen algún código de entre la secuencia de códigos pasada por parámetros.
        /// </summary>
        /// <param name="secuenciaOriginal">Secuencia con entidades sobre la que se aplica este método de extensión.</param>
        /// <param name="códigos">Secuencia de códigos a localizar.</param>
        /// <returns>Subconjunto de entidades de la secuencia sobre la que se aplica el método de extensión que tienen algunos de los códigos pasados por parámetros.</returns>
        public static IEnumerable<T> QueTenganAlgúnCódigo<T>(this IEnumerable<T> secuenciaOriginal, IEnumerable<string> códigos)
            where T : Entity
        {
            return from entidad in secuenciaOriginal
                   where entidad.TieneAlgúnCódigo(códigos)
                   select entidad;
        }

        /// <summary>
        /// Devuelve el subconjunto de entidades enumeradas en la secuencia original que no tienen ningún código de entre la secuencia de códigos pasada por parámetros.
        /// </summary>
        /// <param name="secuenciaOriginal">Secuencia con entidades sobre la que se aplica este método de extensión.</param>
        /// <param name="códigos">Secuencia de códigos a localizar.</param>
        /// <typeparam name="T">Tipo de entidad para el cual está especializado este método.</typeparam>
        /// <returns>Subconjunto de entidades de la secuencia sobre la que se aplica el método de extensión que no tienen ninguno de los códigos pasados por parámetros.</returns>
        public static IEnumerable<T> QueNoTenganAlgúnCódigo<T>(this IEnumerable<T> secuenciaOriginal, IEnumerable<string> códigos)
            where T : Entity
        {
            return from entidad in secuenciaOriginal
                   where !entidad.TieneAlgúnCódigo(códigos)
                   select entidad;
        }

        /// <summary>
        /// Devuelve el subconjunto de entidades enumeradas en la secuencia original que tienen algún código de entre la secuencia de códigos pasada por parámetros.
        /// </summary>
        /// <remarks>
        /// Esta versión del método tiene en cuenta comodines, de modo que si la entidad tiene el código 020012 e intentamos comprobar si dispone del código 02* se devolverá verdadero.
        /// </remarks>
        /// <param name="secuenciaOriginal">Secuencia con entidades sobre la que se aplica este método de extensión.</param>
        /// <param name="códigos">Secuencia de códigos a localizar.</param>
        /// <returns>Subconjunto de entidades de la secuencia sobre la que se aplica el método de extensión que tienen algunos de los códigos pasados por parámetros.</returns>
        public static IEnumerable<T> QueTenganAlgúnCódigoConComodín<T>(this IEnumerable<T> secuenciaOriginal, IEnumerable<string> códigos)
            where T : Entity
        {
            return from entidad in secuenciaOriginal
                   where entidad.TieneAlgúnCódigoConComodín(códigos)
                   select entidad;
        }

        /// <summary>
        /// Devuelve el subconjunto de entidades enumeradas en la secuencia original que están eliminadas.
        /// </summary>
        /// <param name="secuenciaOriginal">Secuencia con entidades sobre la que se aplica este método de extensión.</param>
        /// <typeparam name="T">Tipo de entidad para el cual está especializado este método de extensión.</typeparam>
        /// <returns>Enumeración con las entidades de la secuencia original que están eliminadas.</returns>
        public static IEnumerable<T> Eliminadas<T>(this IEnumerable<T> secuenciaOriginal)
            where T : Entity
        {
            return from entidad in secuenciaOriginal
                   where entidad.Deleted
                   select entidad;
        }

        /// <summary>
        /// Devuelve el subconjunto de entidades enumeradas en la secuencia original que no están eliminadas.
        /// </summary>
        /// <param name="secuenciaOriginal">Secuencia con entidades sobre la que se aplica este método de extensión.</param>
        /// <typeparam name="T">Tipo de entidad para el cual está especializado este método de extensión.</typeparam>
        /// <returns>Enumeración con las entidades de la secuencia original que no están eliminadas.</returns>
        public static IEnumerable<T> NoEliminadas<T>(this IEnumerable<T> secuenciaOriginal)
            where T : Entity
        {
            return from entidad in secuenciaOriginal
                   where !entidad.Deleted
                   select entidad;
        }

        /// <summary>
        /// Devuelve el subconjunto de entidades enumeradas en la secuencia original que son visibles.
        /// </summary>
        /// <param name="secuenciaOriginal">Secuencia con entidades sobre la que se aplica este método de extensión.</param>
        /// <typeparam name="T">Tipo de entidad para el cual está especializado este método de extensión.</typeparam>
        /// <returns>Enumeración con las entidades de la secuencia original que son visibles.</returns>
        public static IEnumerable<T> Visibles<T>(this IEnumerable<T> secuenciaOriginal)
            where T : Entity
        {
            return from entidad in secuenciaOriginal
                   where entidad.AlgúnCódigoVisible()
                   select entidad;
        }

        /// <summary>
        /// Devuelve una secuencia de ReadOnlyLine explotando la entidad si es necesario.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="secuenciaOriginal"></param>
        /// <returns></returns>
        public static IEnumerable<ReadOnlyLine> ExplotaAReadOnlyLine<T>(this IEnumerable<T> secuenciaOriginal)
        {
            foreach (var entidad in secuenciaOriginal)
            {
                if (entidad is ReadOnlyLine)
                    yield return entidad as ReadOnlyLine;
                else if (entidad is ReadOnlyPolygon)
                {
                    var polígono = entidad as ReadOnlyPolygon;

                    // Devolvemos primero el contorno exterior como una línea cerrada
                    var línea = new Line(polígono.Codes);
                    línea.Points.Add(polígono.Points);
                    yield return línea;

                    // Devolvemos cada uno de los huecos que son líneas cerradas
                    foreach (var hueco in polígono.Holes)
                        yield return hueco;
                } else if (entidad is ReadOnlyComplex)
                {
                    var complejo = entidad as ReadOnlyComplex;

                    foreach (var subentidad in complejo.Entities)
                    {
                        if (subentidad is ReadOnlyLine)
                            yield return subentidad as ReadOnlyLine;
                    }
                }
            }
        }

    }
}