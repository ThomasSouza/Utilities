using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardBulletFactory : Factory
{
    public static WizardBulletFactory Instance { get { return _instance; } }
    static WizardBulletFactory _instance;
    public override void Start()
    {
        _instance = this;
        base.Start();
    }
}
