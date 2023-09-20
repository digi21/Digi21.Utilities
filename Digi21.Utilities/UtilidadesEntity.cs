using System;
using System.Collections.Generic;
using System.Linq;
using Digi21.DigiNG.Entities;
using Digi21.Utilities.Properties;

namespace Digi21.Utilities
{
    /// <summary>
    /// Define una serie de métodos de extensión para entidades.
    /// </summary>
    public static class UtilidadesEntity
    {
        /// <summary>
        /// Indica si la entidad sobre la que se ejecuta el método de extensión tiene algún código cuyo nombre coincide con los pasados por parámetros.
        /// </summary>
        /// <param name="entidad">Entidad sobre la que queremos averiguar si dispone del código.</param>
        /// <param name="código">Nombre del código a localizar.</param>
        /// <returns>Verdadero si se localizó el código entre los códigos de la entidad.</returns>
        public static bool TieneElCódigo(this Entity entidad, string código)
        {
            return entidad.Codes.Any(cod => cod.Name == código);
        }

        /// <summary>
        /// Indica si la entidad sobre la que se ejecuta el método de extensión tiene algún código cuyo nombre coincide con los pasados por parámetros.
        /// </summary>
        /// <remarks>
        /// Esta versión del método tiene en cuenta comodines, de modo que si la entidad tiene el código 020012 e intentamos comprobar si dispone del código 02* se devolverá verdadero.
        /// </remarks>
        /// <param name="entidad">Entidad sobre la que queremos averiguar si dispone del código.</param>
        /// <param name="código">Nombre del código a localizar.</param>
        /// <returns>Verdadero si se localizó el código entre los códigos de la entidad.</returns>
        public static bool TieneElCódigoConComodín(this Entity entidad, string código)
        {
            return entidad.Codes.Any(cod => Code.Compare(cod.Name, código));
        }

        /// <summary>
        /// Devuelve verdadero si la entidad tiene alguno de los códigos de la secuencia de códigos pasada por parámetros.
        /// </summary>
        /// <param name="entidad">Entidad sobre la que queremos averiguar si dispone de algún código.</param>
        /// <param name="códigos">Secuencia de nombres de códigos a buscar.</param>
        /// <returns>Verdadero si se localizó algún código de entre los que hay en la secuencia de códigos.</returns>
        public static bool TieneAlgúnCódigo(this Entity entidad, IEnumerable<string> códigos)
        {
            return entidad.Codes.Any(código => códigos.Any(cod => código.Name == cod));
        }

        /// <summary>
        /// Devuelve verdadero si la entidad tiene alguno de los códigos de la secuencia de códigos pasada por parámetros.
        /// </summary>
        /// <remarks>
        /// Esta versión del método tiene en cuenta comodines, de modo que si la entidad tiene el código 020012 e intentamos comprobar si dispone del código 02* se devolverá verdadero.
        /// </remarks>
        /// <param name="entidad">Entidad sobre la que queremos averiguar si dispone de algún código.</param>
        /// <param name="códigos">Secuencia de nombres de códigos a buscar.</param>
        /// <returns>Verdadero si se localizó algún código de entre los que hay en la secuencia de códigos.</returns>
        public static bool TieneAlgúnCódigoConComodín(this Entity entidad, IEnumerable<string> códigos)
        {
            return entidad.Codes.Any(código => códigos.Any(cod => Code.Compare(código.Name, cod)));
        }

        /// <summary>
        /// Devuelve una enumeración de los códigos que tiene la entidad que son comunes con los códigos pasados por parámetro.
        /// </summary>
        /// <param name="entidad">Entidad a analizar.</param>
        /// <param name="códigos">Códigos a buscar en la entidad.</param>
        /// <returns>Enumeración de códigos que tiene la entidad comunes con los pasados por parámetro.</returns>
        public static IEnumerable<string> CódigosComunes(this Entity entidad, IEnumerable<string> códigos)
        {
            var lista = new List<string>();

            foreach (var código in entidad.Codes)
                foreach (var cod in códigos)
                {
                    if (!Code.Compare(código.Name, cod)) continue;

                    if( !lista.Contains(cod))
                        lista.Add(cod);
                }

            return lista;
        }

        /// <summary>
        /// Indica si la entidad pasada por parámetros tiene álgún código con la visibilidad activada en la ventana de dibujo.
        /// </summary>
        /// <param name="entidad">Entidad cuya visibilidad se desea analizar.</param>
        /// <returns>Verdadero si la entidad tiene algún código visible.</returns>
        public static bool AlgúnCódigoVisible(this Entity entidad)
        {
            return AlgúnCódigoVisible(entidad, false);
        }

        /// <summary>
        /// Indica si la entidad pasada por parámetros tiene álgún código con la visibilidad activada en la ventana de dibujo.
        /// </summary>
        /// <param name="entidad">Entidad cuya visibilidad se desea analizar.</param>
        /// <param name="códigosDesconocidosSonVisibles">Indica si los códigos desconocidos se consideran visibles.</param>
        /// <returns>Verdadero si la entidad tiene algún código visible.</returns>
        public static bool AlgúnCódigoVisible(this Entity entidad, bool códigosDesconocidosSonVisibles)
        {
            foreach (var código in entidad.Codes)
            {
                try
                {
                    if (código.Visible != 0)
                        return true;
                }
                catch (Exception)
                {
                    if (códigosDesconocidosSonVisibles)
                        return true;
                }
            }

            return false;
        }


        /// <summary>
        /// Devuelve una secuencia de ReadOnlyLine explotando la entidad si es necesario.
        /// </summary>
        /// <param name="entidad">Entidad a explotar.</param>
        /// <returns></returns>
        public static IEnumerable<ReadOnlyLine> ExplotaAReadOnlyLine(this Entity entidad)
        {
            switch (entidad)
            {
                case ReadOnlyLine line:
                    yield return line;
                    break;
                case ReadOnlyPolygon polígono:
                    // Devolvemos primero el contorno exterior como una línea cerrada
                    var línea = new Line(polígono.Codes);
                    línea.Points.AddRange(polígono.Points);
                    yield return línea;

                    // Devolvemos cada uno de los huecos que son líneas cerradas
                    foreach (var hueco in polígono.Holes)
                        yield return hueco;
                    break;
                case ReadOnlyComplex complejo:
                    foreach (var subentidad in complejo.Entities)
                    {
                        if (subentidad is ReadOnlyLine line)
                            yield return line;
                    }

                    break;
            }
        }

        /// <summary>
        /// Indica si los códigos de las dos entidades son idénticos (el orden de los códigos no importa).
        /// </summary>
        /// <param name="entidadA">Primera entidad a analizar.</param>
        /// <param name="entidadB">Segunda entidad a analizar.</param>
        /// <param name="listaErrores">Listado de errores localizados.</param>
        /// <returns>Verdadero si las dos entidades tienen los mismos códigos.</returns>
        public static bool CódigosIdénticos(this Entity entidadA, Entity entidadB, List<string> listaErrores=null)
        {
            if (entidadA.Codes.Count != entidadB.Codes.Count) {
                if( null == listaErrores )
                    return false;
                listaErrores.Add(Recursos.LasDosEntidadesTienenUnNumeroDistintoDeCodigos);
            }

            //var atributosA = entidadA.Owner.DatabaseTables.DatabaseAttributes;
            //var atributosB = entidadB.DatabaseAttributes;
            //foreach (var código in atributosA)
            //{
            //    if (!atributosB.ContainsKey(código.Key))
            //    {
            //        if (null == listaErrores)
            //            return false;
            //        listaErrores.Add(string.Format("No se localizó el código {0} en una de las entidades", código.Key));
            //    }

            //    foreach (var campo in código.Value)
            //    {
            //        if (!atributosB[código.Key].ContainsKey(campo.Key))
            //        {
            //            if (null == listaErrores)
            //                return false;

            //            listaErrores.Add(string.Format("No se localizó el campo {0} en el código {1} de una de las entidades",
            //                campo.Key,
            //                código.Key));
            //        }

            //        object valorA = campo.Value;
            //        object valorB = atributosB[código.Key][campo.Key];

            //        if (valorA.GetType() != valorB.GetType())
            //        {
            //            if (null == listaErrores)
            //                return false;

            //            listaErrores.Add(string.Format("El tipo de valor almacenado en el campo {0} en del código {1} es distinto en ambas entidades",
            //                campo.Key,
            //                código.Key));
            //        }

            //        if ((valorA as IComparable).CompareTo(valorB) != 0)
            //        {
            //            if (null == listaErrores)
            //                return false;

            //            listaErrores.Add(string.Format("Los valores del campo {0} para el código {1} son distintos",
            //                campo.Key,
            //                código.Key));
            //        }
            //    }
            //}
            //if (null == listaErrores)
            //    return true;

            return listaErrores == null ? true : listaErrores.Count == 0;
        }
    }
}
