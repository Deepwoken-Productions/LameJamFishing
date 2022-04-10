using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour
{
    public static CloudManager instance;

    [SerializeField] private int maxClouds;
    [SerializeField] private float cloudHeight;
    [SerializeField] private Transform cloudEnd;
    [SerializeField] private Transform cloudContainer;
    [SerializeField] private GameObject[] cloudObjects;

    private List<Cloud> clouds = new List<Cloud>();

    private void Awake()
    {
        instance = this;

        SpawnClouds();
    }

    public void SpawnClouds()
    {
        for (int i = 0; i < maxClouds; i++)
        {
            GameObject randomCloud = cloudObjects[Random.Range(0, cloudObjects.Length)];

            Cloud cloud = Instantiate(randomCloud, cloudContainer).GetComponent<Cloud>();

            cloud.Initialize(cloudEnd.position.x, cloudHeight);
            clouds.Add(cloud);
        }
    }

    public void DeleteClouds(int amt)
    {
        for (int i = 0; i < amt; i++)
        {
            Destroy(clouds[i].gameObject);            
        }
    }
}
