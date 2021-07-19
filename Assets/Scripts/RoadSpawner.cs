using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> roads;
    [SerializeField] private GameObject backFence;
    [SerializeField] private float offset = 120f;

    private void Start()
    {
        if (roads != null && roads.Count > 0)
        {
            roads = roads.OrderBy(r => r.transform.position.z).ToList();
        }
    }

    public void MoveRoad()
    {
        GameObject moveRoad = roads[0];
        roads.Remove(moveRoad);
        float newX = roads[roads.Count - 1].transform.position.x + offset;
        moveRoad.transform.position = new Vector3(newX, 0, 0);
        roads.Add(moveRoad);

        backFence.transform.position += new Vector3(offset, 0, 0);
    }
}
