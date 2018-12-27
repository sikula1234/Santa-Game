using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public GameObject[] mobPrefabs;
    public GameObject[] mobs;
    public Vector2[] spawnCoordinates;

    // Start is called before the first frame update
    void Start()
    {
        spawnCoordinates = new Vector2[10];
        mobs = new GameObject[10]; //makes sure they match length
        for (int i = 0; i < mobs.Length; i++)
        {
            int rand = Random.Range(0, mobPrefabs.Length);
            mobs[i] = Instantiate(mobPrefabs[rand]) as GameObject;

        }

    }

    public void SpawnMobs()
    {
        for (int i = 0; i < mobs.Length; i++)
        {
            int rand = Random.Range(0, mobPrefabs.Length);
            mobs[i] = Instantiate(mobPrefabs[rand]) as GameObject;
            mobs[i].transform.position = spawnCoordinates[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }  
}
