using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public static WaypointManager Instance; // Singleton instance

    public List<Transform> waypoints = new List<Transform>(); // List of waypoints

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Duplicate WaypointManager instance found. Destroying the new one.");
            Destroy(gameObject);
        }

        PopulateWaypointsList();
    }

    private void PopulateWaypointsList()
    {
        waypoints.Clear();
        foreach (Transform child in transform)
        {
            waypoints.Add(child);
        }
    }

    public List<Transform> GetWaypoints()
    {
        return waypoints;
    }

    public Transform GetRandomWaypoint()
    {
        if (waypoints.Count == 0)
        {
            Debug.LogError("No waypoints found.");
            return null;
        }

        return waypoints[Random.Range(0, waypoints.Count)];
    }
}
