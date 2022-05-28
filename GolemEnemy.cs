using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GolemEnemy : Enemy
{
    public Enemy enemyPrefab;

    public override void Start()
    {
        GetDataFromScriptable();
        // Waypoint settings 
        _animator = GetComponent<Animator>();
        _originalMoveMultiplier = _movementMultiplier;
        _currentWaypointIndex = 0;
        _currentWaypoint = WaypointFather.Instance.GetCurrentWaypoint(_currentWaypointIndex);
        // Set Max Health
        SetMaxHealth();
        // Get Rigidbody from GO
        SetPhysicsSettings();

        lifebar = GetComponentInChildren<Slider>();

        _view = new EnemyView(this, lifebar, vfxOnDie);
        _model = new EnemyModel(this, _rb, _movementMultiplier);
    }
    public override void Die()
    {
        for (int i = 0; i < 2; i++)
        {
            var enemy = Instantiate(enemyPrefab)
                .SetPosition(transform.position.x, transform.position.y, transform.position.z)
                .SetScale(transform.localScale.x, transform.localScale.y, transform.localScale.z)
                .SetHealth(maxHP)
                .SetCurrentWaypoint(_currentWaypointIndex);
            EventManager.Trigger(Constants.SpawnNewEnemyString, enemy);
        }
        _view.OnDieParticles();
        Destroy(gameObject);
        EventManager.Trigger(Constants.DieNewEnemyString, this);
    }

    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);
    }
}
