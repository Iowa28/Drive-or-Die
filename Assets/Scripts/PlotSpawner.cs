using System.Collections.Generic;
using UnityEngine;

public class PlotSpawner : MonoBehaviour
{
    [SerializeField] private float plotSize = 119f;
    [SerializeField] private float lastPosZ = -8f;
    
    [SerializeField] private List<GameObject> plots;
    
    private int initAmount = 5;
    private float xRightOffset = 360f;

    private GameObject plotLeft;
    private GameObject plotRight;
    
    void Start()
    {
        for (int i = 0; i < initAmount; i++)
        {
            SpawnPlot();
        }
    }

    public void SpawnPlot()
    {
        plotLeft = plots[Random.Range(0, plots.Count)];
        plotRight = plots[Random.Range(0, plots.Count)];

        float xPos = lastPosZ + plotSize;

        Instantiate(plotLeft, new Vector3(xPos, 0, 0), plotLeft.transform.rotation);
        Instantiate(plotRight, new Vector3(xPos + xRightOffset, 0, 0), new Quaternion(0, 180, 0, 0));

        lastPosZ += plotSize;
    }

}
