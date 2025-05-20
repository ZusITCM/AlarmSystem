using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private float _speed;

    [SerializeField] private List<Transform> _waypoints;

    private Transform _waypoint;

    private readonly float _closeDistance = 0.8f;

    private int _currentWaypointIndex;

    private void Awake()
    {
        _currentWaypointIndex = 0;

        _waypoint = _waypoints[_currentWaypointIndex];
    }

    private void Update()
    {
        if (_waypoints.Count != 0)
        {
            UpdateTarget();

            Move();
        }
    }

    private void UpdateTarget()
    {
        if (IsTargetReached())
            _currentWaypointIndex = ++_currentWaypointIndex % _waypoints.Count;
    }

    private void Move()
    {
        _waypoint = _waypoints[_currentWaypointIndex];

        transform.position = Vector3.MoveTowards(transform.position, _waypoint.position, _speed * Time.deltaTime);
    }

    private bool IsTargetReached() => transform.position.IsEnoughClose(_waypoint.transform.position, _closeDistance);
}