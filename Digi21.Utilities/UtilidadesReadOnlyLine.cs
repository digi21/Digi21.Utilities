using System;
using System.Collections.Generic;
using Digi21.Math;
using Digi21.DigiNG.Entities;

namespace Digi21.Utilities
{
    /// <summary>
    /// Define una serie de métodos de extensión para el tipo ReadOnlyLine.
    /// </summary>
    public static class UtilidadesReadOnlyLine
    {
        /// <summary>
        /// Devuelve la distancia mínima (en el plano XY) entre una línea y un punto.
        /// </summary>
        /// <param name="línea">Línea contra la cual se proyectará el punto para calcular su distancia.</param>
        /// <param name="punto">Punto a calcular su distancia contra la línea.</param>
        /// <returns>La menor distancia a la línea.</returns>
        /// <exception cref="System.Exception">Si el punto no intersecciona con la línea.</exception>
        public static double DistanciaMínima(this ReadOnlyLine línea, Point3D punto)
        {
            double? mejorDistancia = null;

            for (var i = 0; i < línea.Points.Count - 1; i++)
            {
                var segmento = new Segment(línea.Points[i], línea.Points[i + 1]);

                if (!segmento.Window.Intersect2D(punto))
                    continue;

                var distancia = segmento.Distance2D(punto);

                mejorDistancia = mejorDistancia == null ? distancia : System.Math.Min(mejorDistancia.Value, distancia);
            }

            if (!mejorDistancia.HasValue)
                throw new Exception(Recursos.ElPuntoNoInterseccionaConLaLinea);

            return mejorDistancia.Value;
        }

        /// <summary>
        /// Devuelve el índice al segmento de la línea más cercano al punto pasado por parámetro.
        /// </summary>
        /// <param name="línea">Línea contra la cual se proyectará el punto para calcular su distancia.</param>
        /// <param name="punto">Punto a calcular su distancia contra la línea.</param>
        /// <returns>Índice al mejor segmento.</returns>
        /// <exception cref="System.Exception">Si el punto no intersecciona con la línea.</exception>
        public static int SegmentoMásCercano(this ReadOnlyLine línea, Point3D punto)
        {
            int? mejorSegmento = null;
            double? mejorDistancia = null;

            for (var i = 0; i < línea.Points.Count - 1; i++)
            {
                var segmento = new Segment(línea.Points[i], línea.Points[i + 1]);

                if (!segmento.Window.Intersect2D(punto))
                    continue;

                var distancia = segmento.Distance2D(punto);
                if (mejorDistancia != null && distancia >= mejorDistancia.Value) continue;

                mejorSegmento = i;
                mejorDistancia = distancia;
            }

            if (!mejorSegmento.HasValue)
                throw new Exception(Recursos.ElPuntoNoInterseccionaConLaLinea);

            return mejorSegmento.Value;
        }

        /// <summary>
        /// Indica si las dos líneas son contínuas en el espacio.
        /// </summary>
        /// <param name="líneaA">Primera línea para analizar la continuidad.</param>
        /// <param name="líneaB">Segunda línea para analizar la continuidad.</param>
        /// <returns>Verdadero si las dos líneas son contínuas. Falso en caso contrario.</returns>
        public static bool TieneContinuidadCon(this ReadOnlyLine líneaA, ReadOnlyLine líneaB)
        {
            return líneaA.FirstVertex == líneaB.FirstVertex ||
                   líneaA.LastVertex == líneaB.FirstVertex ||
                   líneaA.FirstVertex == líneaB.LastVertex ||
                   líneaA.LastVertex == líneaB.LastVertex;
        }

        /// <summary>
        /// Indica si las dos líneas son contínuas en el plano XYZ.
        /// </summary>
        /// <param name="líneaA">Primera línea para analizar la continuidad.</param>
        /// <param name="líneaB">Segunda línea para analizar la continuidad.</param>
        /// <returns>Verdadero si las dos líneas son contínuas. Falso en caso contrario.</returns>
        public static bool TieneContinuidad2DCon(this ReadOnlyLine líneaA, ReadOnlyLine líneaB)
        {
            return (Point2D)líneaA.FirstVertex == (Point2D)líneaB.FirstVertex ||
                   (Point2D)líneaA.LastVertex == (Point2D)líneaB.FirstVertex ||
                   (Point2D)líneaA.FirstVertex == (Point2D)líneaB.LastVertex ||
                   (Point2D)líneaA.LastVertex == (Point2D)líneaB.LastVertex;
        }

        /// <summary>
        /// Devuelve la distancia mínima en el plano XY entre los extremos de las dos líneas.
        /// </summary>
        /// <param name="líneaA">Primera línea para analizar la distancia.</param>
        /// <param name="lineaB">Segunda línea para analizar la distancia.</param>
        /// <returns>Menor de las distancias entre las dos líneas.</returns>
        public static double DistanciaMínimaExtremo(this ReadOnlyLine líneaA, ReadOnlyLine lineaB)
        {
            var distancia = double.MaxValue;

            distancia = System.Math.Min(distancia, ((Point2D)líneaA.FirstVertex - (Point2D)lineaB.FirstVertex).Module);
            distancia = System.Math.Min(distancia, ((Point2D)líneaA.FirstVertex - (Point2D)lineaB.LastVertex).Module);
            distancia = System.Math.Min(distancia, ((Point2D)líneaA.LastVertex - (Point2D)lineaB.FirstVertex).Module);
            distancia = System.Math.Min(distancia, ((Point2D)líneaA.LastVertex - (Point2D)lineaB.LastVertex).Module);

            return distancia;
        }

        /// <summary>
        /// Calcula los extremos más cercanos de las dos líneas pasadas por parámetro.
        /// </summary>
        /// <param name="líneaA">Primera línea para analizar la distancia.</param>
        /// <param name="líneaB">Segunda línea para analizar la distancia.</param>
        /// <param name="vérticeA">Índice al vertice de la primera línea que está más cerca del extremo de la segunda línea.</param>
        /// <param name="vérticeB">Índice al vertice de la segunda línea que está más cerca del extremo de la primera línea.</param>
        public static void ExtremosMásCercanos(ReadOnlyLine líneaA, ReadOnlyLine líneaB, out int vérticeA, out int vérticeB)
        {
            var a = ((Point2D)líneaA.FirstVertex - (Point2D)líneaB.FirstVertex).Module;
            var b = ((Point2D)líneaA.FirstVertex - (Point2D)líneaB.LastVertex).Module;
            var c = ((Point2D)líneaA.LastVertex - (Point2D)líneaB.FirstVertex).Module;
            var d = ((Point2D)líneaA.LastVertex - (Point2D)líneaB.LastVertex).Module;

            var valores = new List<double> { a, b, c, d };
            valores.Sort();

            if (a.IsEqual(valores[0]))
            {
                vérticeA = 0;
                vérticeB = 0;
            }
            else if (b.IsEqual(valores[0]))
            {
                vérticeA = 0;
                vérticeB = líneaB.Points.Count - 1;
            }
            else if (c.IsEqual(valores[0]))
            {
                vérticeA = líneaA.Points.Count - 1;
                vérticeB = 0;
            }
            else
            {
                vérticeA = líneaA.Points.Count - 1;
                vérticeB = líneaB.Points.Count - 1;
            }
        }
    }
}
