using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [Header("Level attributes")]
    [SerializeField] List<GameObject> platforms;
    [SerializeField] List<GameObject> spawnedObjects;
    [SerializeField] float platformDistance;
    [SerializeField] float initialOffset;


    // Start is called before the first frame update
    void Start()
    {
        SpawnLevel();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnLevel()
    {
        for (int i = 0; i < platforms.Count; i++)
        {
            Instantiate(platforms[i], new Vector3(0, 0,initialOffset+( platformDistance * i)), Quaternion.identity);
        }
    }
}
