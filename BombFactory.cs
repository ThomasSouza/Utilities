using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombFactory : Factory
{
    public static BombFactory Instance { get { return _instance; } }
    static BombFactory _instance;
    public override void Start()
    {
        _instance = this;
        base.Start();
    }
}
