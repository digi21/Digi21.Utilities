using System;
using System.Collections;
using System.Collections.Generic;
using Digi21.DigiNG.Entities;
using Digi21.DigiNG.IO;
using Digi21.Math;

/// <summary>
/// Esta clase encapsula un IDrawingFile para que se comporte como una lista de geometrías
/// </summary>
/// <remarks>
/// Esta clase soluciona el problema de que los importadores únicamente pueden iterarse una vez, pues están pensados para leerse una única vez para ser almacenados en un contenedor.
/// 
/// El enumerador que proporciona esta clase se encarga de ir leyendo geometrías del archivo de dibujo bajo demanda, de manera que si no se enumera el archivo
/// no se consumirá memoria.
/// </remarks>
public class MemoryDrawingFile : IDrawingFile, IDisposable
{
    private readonly IDrawingFile _archivoDibujo;
    private readonly IEnumerator<Entity> _enumerador;
    private bool _finalizadaLectura;
    private readonly object _bloqueo = new object();
    private readonly List<Entity> _contenedor = new List<Entity>();

    public MemoryDrawingFile(IDrawingFile drawingFile)
    {
        _archivoDibujo = drawingFile;
        _enumerador = drawingFile.GetEnumerator();
    }

    public IEnumerator<Entity> GetEnumerator() => new Enumerador(this);
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public Point3D W => _archivoDibujo.W;
    public Point3D SW => _archivoDibujo.SW;
    public Point3D S => _archivoDibujo.S;
    public Point3D SE => _archivoDibujo.SE;
    public Point3D E => _archivoDibujo.E;
    public Point3D NE => _archivoDibujo.NE;
    public Point3D N => _archivoDibujo.N;
    public Point3D NW => _archivoDibujo.NW;
    public Point3D Center => _archivoDibujo.Center;
    public double Depth => _archivoDibujo.Depth;
    public double Height => _archivoDibujo.Height;
    public double Width => _archivoDibujo.Width;
    public bool Valid => _archivoDibujo.Valid;
    public double Zmax => _archivoDibujo.Zmax;
    public double Ymax => _archivoDibujo.Ymax;
    public double Xmax => _archivoDibujo.Xmax;
    public double Zmin => _archivoDibujo.Zmin;
    public double Ymin => _archivoDibujo.Ymin;
    public double? Xmin => _archivoDibujo.Xmin;
    public int IndexOf(Entity entity) => _archivoDibujo.IndexOf(entity);

    public bool Visible
    {
        get => _archivoDibujo.Visible;
        set => _archivoDibujo.Visible = value;
    }

    public string ConnectionString => _archivoDibujo.ConnectionString;

    public string Path => _archivoDibujo.Path;
    string IReadOnlyDrawingFile.Path => _archivoDibujo.Path;

    public ReadOnlyComplex Add(Complex complex)
    {
        _contenedor.Add(complex);
        return _archivoDibujo.Add(complex);
    }

    public ReadOnlyPolygon Add(Polygon polygon)
    {
        _contenedor.Add(polygon);
        return _archivoDibujo.Add(polygon);
    }

    public ReadOnlyText Add(Text text)
    {
        _contenedor.Add(text);
        return _archivoDibujo.Add(text);
    }

    public ReadOnlyPoint Add(Point point)
    {
        _contenedor.Add(point);
        return _archivoDibujo.Add(point);
    }

    public ReadOnlyLine Add(Line line)
    {
        _contenedor.Add(line);
        return _archivoDibujo.Add(line);
    }

    public void Add(IEnumerable<Entity> entities)
    {
        _contenedor.AddRange(entities);
        _archivoDibujo.Add(entities);
    }

    public void Add(Entity entity)
    {
        _contenedor.Add(entity);
        _archivoDibujo.Add(entity);
    }

    public void Delete(IEnumerable<Entity> entities) => _archivoDibujo.Delete(entities);

    public void Delete(Entity entity) => _archivoDibujo.Delete(entity);

    public string Wkt => _archivoDibujo.Wkt;
    public IDictionary<string, int> DatabaseTables => _archivoDibujo.DatabaseTables;
    public bool CanWrite => _archivoDibujo.CanWrite;
    public bool CanRead => _archivoDibujo.CanRead;

    private bool MoveNext(ref int posicion)
    {
        ++posicion;
        return posicion < _contenedor.Count || CargaSiguienteGeometriaSiEsPosible();
    }

    private bool CargaSiguienteGeometriaSiEsPosible()
    {
        if (_finalizadaLectura)
            return false;

        lock (_bloqueo)
        {
            if (!_enumerador.MoveNext())
            {
                _finalizadaLectura = true;
                return false;
            }

            _contenedor.Add(_enumerador.Current);
            return true;
        }
    }

    private class Enumerador : IEnumerator<Entity>
    {
        private readonly MemoryDrawingFile _padre;
        private int _posicion = -1;

        public Enumerador(MemoryDrawingFile padre)
        {
            _padre = padre;
        }

        public void Dispose()
        {
        }

        public bool MoveNext() => _padre.MoveNext(ref _posicion);

        public void Reset() => _posicion = -1;

        public Entity Current => _padre._contenedor[_posicion];

        object IEnumerator.Current => Current;
    }

    public void Dispose()
    {
        _enumerador?.Dispose();
        if( _archivoDibujo is IDisposable disp)
            disp.Dispose();
    }
}
