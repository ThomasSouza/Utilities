using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyModel
{
    public EnemyModel(Enemy enemy, Rigidbody rb, float movementMultiplier)
    {
        _enemy = enemy;
        _rb = rb;
        _movementMultiplier = movementMultiplier;

        _currentWaypointIndex = 0;
        _currentWaypoint = WaypointFather.Instance.GetCurrentWaypoint(_currentWaypointIndex);

        _enemy.OnUpdate += MoveEnemy;
        _enemy.OnMoveScaleChange += OnMoveScaleChange;
    }
    Enemy _enemy;
    Rigidbody _rb;

    [Header("Waypoints")]
    protected Transform _currentWaypoint;
    protected int _currentWaypointIndex;

    Vector3 _desired = new Vector3();
    Vector3 _steering = new Vector3();
    Vector3 _velocity = new Vector3();
    float _maxForce = 0.2f;
    float _arriveDistance = 0.5f;
    float _movementMultiplier;

    private void MoveEnemy()
    {
        if ((_currentWaypoint.position - _enemy.transform.position).magnitude <= _arriveDistance)
            NextWaypoint();

        _desired = (_currentWaypoint.position - _enemy.transform.position).normalized * (Constants.EnemyNormalSpeed * _movementMultiplier);
        _steering = _desired - _velocity;
        _steering = Vector3.ClampMagnitude(_steering, _maxForce);
        _velocity = Vector3.ClampMagnitude(_velocity + _steering, Constants.EnemyNormalSpeed * _movementMultiplier);
        _velocity.y = _rb.velocity.y;
        _rb.velocity = _velocity;
        _enemy.transform.forward = _velocity.normalized;
    }

    private void NextWaypoint()
    {
        _currentWaypointIndex++;
        if (_currentWaypointIndex == WaypointFather.Instance.GetWaypoints().Count)
            _enemy.Die();
        else
            _currentWaypoint = WaypointFather.Instance.GetCurrentWaypoint(_currentWaypointIndex);
    }

    public void OnMoveScaleChange(float newMove)
    {
        _movementMultiplier = newMove;
    }
}
