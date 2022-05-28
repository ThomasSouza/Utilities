using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    public Bullet bulletPrefab;
    public ObjectPool<Bullet> pool;
    public int initialStock = 5;
    public virtual void Start()
    {
        pool = new ObjectPool<Bullet>(BulletCreator, Bullet.TurnOn, Bullet.TurnOff, initialStock);
    }

    //Funcion que contiene la logica de la creacion de la bala
    public Bullet BulletCreator()
    {
        return Instantiate(bulletPrefab);
    }

    //Funcion que va a ser llamada cuando el objeto tenga que ser devuelto al Pool
    public void ReturnBullet(Bullet b)
    {
        pool.ReturnObject(b);
    }
}
