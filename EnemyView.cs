using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyView : EnemyViewFather
{
    public EnemyView(Enemy enemy, Slider lifebar, GameObject vfxOnDie)
    {
        _lifebar = lifebar;
        _enemy = enemy;
        _lifebar.maxValue = _enemy.maxHP;
        _lifebar.value = _enemy.maxHP;
        _vfxOnDie = vfxOnDie;

        OnConstruct();
    }

}
