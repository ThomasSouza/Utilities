using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleEnemy : Enemy
{
    public override void Start()
    {
        _currentWaypoint = WaypointFather.Instance.GetCurrentWaypoint(_currentWaypointIndex);
        SetMaxHealth();
        SetPhysicsSettings();
    }
}
