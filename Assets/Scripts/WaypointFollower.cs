using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private float _speed;

    [SerializeField] private List<Transform> _waypoints;

    private readonly float _closeDistance = 0.1f;

    private int _currentWaypointIndex;

    private void Awake()
    {
        _currentWaypointIndex = 0;
    }

    private void Update()
    {
        if (_waypoints.Count != 0)
        {
            Transform waypoint = _waypoints[_currentWaypointIndex];

            transform.position = Vector3.MoveTowards(transform.position, waypoint.position, _speed * Time.deltaTime);

            Vector3 distance = waypoint.position - transform.position;

            if (distance.sqrMagnitude <= Mathf.Pow(_closeDistance, 2))
                _currentWaypointIndex = ++_currentWaypointIndex % _waypoints.Count;
        }
    }
}
