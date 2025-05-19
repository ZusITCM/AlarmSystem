using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private float _speed;

    [SerializeField] private List<Transform> _waypoints;

    private Transform _waypoint;

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
            _waypoint = _waypoints[_currentWaypointIndex];

            transform.position = Vector3.MoveTowards(transform.position, _waypoint.position, _speed * Time.deltaTime);

            if (IsTargetReached())
                _currentWaypointIndex = ++_currentWaypointIndex % _waypoints.Count;

            Debug.Log(IsTargetReached());
        }
    }

    private bool IsTargetReached() => transform.position.IsEnoughClose(_waypoint.transform.position, _closeDistance);
}

public static class Vector3Extensions
{
    public static float SqrDistance(this Vector3 start, Vector3 end)
    {
        Debug.Log((end - start).sqrMagnitude);

        return (end - start).sqrMagnitude;
    }

    public static bool IsEnoughClose(this Vector3 start, Vector3 end, float distance) => start.SqrDistance(end) <= Mathf.Pow(distance, 2);
}