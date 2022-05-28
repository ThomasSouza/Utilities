using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectPool<T>
{
    Func<T> _factoryMethod;       // Contiene la logica de creacion del objeto
    List<T> _currentStock;        // Lista con todos los objetos en desuso
    bool _isDynamic;              // Si es dinamico quiere decir que se crea una vez que se acaba el pool, not implemented
    Action<T> _turnOnCallback;
    Action<T> _turnOffCallback;

    public ObjectPool(Func<T> factoryMethod, Action<T> turnOnCallback, Action<T> turnOffCallback, int inicialStock)
    {
        _factoryMethod = factoryMethod;
        _turnOnCallback = turnOnCallback;
        _turnOffCallback = turnOffCallback;
        //_isDynamic = isDynamic;

        _currentStock = new List<T>();

        for (int i = 0; i < inicialStock; i++)
        {
            T obj = _factoryMethod();

            _turnOffCallback(obj);

            _currentStock.Add(obj);
        }
    }

    public T GetObject()
    {
        var result = default(T);
        if (_currentStock.Count > 0)
        {
            result = _currentStock[0];
            _currentStock.RemoveAt(0);
            _turnOnCallback(result);
        }
        else
        {
            result = _factoryMethod();
        }

        return result;
    }

    public void ReturnObject(T obj)
    {
        _turnOffCallback(obj);
        _currentStock.Add(obj);
    }
}
