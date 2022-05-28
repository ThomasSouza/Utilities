using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : Factory
{
    public static BulletFactory Instance { get { return _instance; } }
    static BulletFactory _instance;
    public override void Start()
    {
        _instance = this;
        base.Start();
    }
}
