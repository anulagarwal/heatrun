using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackHandler : MonoBehaviour
{
    #region Properties
    [Header("Attributes")]
    [SerializeField] private bool spawnObstacles = false;

    [Header("Components Reference")]
    [SerializeField] private GameObject[] obstaclePrefabs = null;
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] private Transform obstacleHolder = null;
    #endregion

    #region MonoBehaviour Functions
    private void Start()
    {
        if (spawnObstacles)
        {
            SpawnObstacles();
        }
    }
    #endregion

    #region Private Core Functions
    private void SpawnObstacles()
    {
        int spawnAmount = Random.Range(4, 12);
        for (int i = 0; i < spawnAmount; i++)
        {
            int index = Random.Range(0, spawnPoints.Count);
            Instantiate(obstaclePrefabs[Random.Range(0, 2)], spawnPoints[index].position, Quaternion.identity, obstacleHolder);
            spawnPoints.RemoveAt(index);
        }
    }
    #endregion
}
