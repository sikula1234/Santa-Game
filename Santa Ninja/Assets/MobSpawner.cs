using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public GameObject[] mobPrefabs;
    public GameObject[] mobs;
    public List<Vector2> spawnCoordinates = new List<Vector2>();
    bool JeCasNaMoby = true;
    public int pocetMobu = 6;

    // Start is called before the first frame update
    void Start()
    {
        GenerateCoordinates();
       /* for (int i = 0; i < spawnCoordinates.Length; i++)
        {
            spawnCoordinates[i] = new Vector2(40f, 40f);

        }*/
        mobs = new GameObject[pocetMobu]; //makes sure they match length
        for (int i = 0; i < mobs.Length; i++)
        {
            int rand = Random.Range(0, mobPrefabs.Length);
            mobs[i] = Instantiate(mobPrefabs[rand]) as GameObject;

        }

    }
  
    void GenerateCoordinates()
    {
        for (int i = 0; i < pocetMobu; i++)
        {
            while (true)
            {
                int a = Random.Range(2, 39);
                int b = Random.Range(2, 39);
                Vector2 coordinate = new Vector2(a, b);
                if(IsSuitable(coordinate, spawnCoordinates))
                {
                    spawnCoordinates.Add(coordinate);
                    break;
                }
            }
            
        }
    }

    bool IsSuitable(Vector2 coordinate, List<Vector2> usedCoordinates)
    {
        for (int i = 0; i < usedCoordinates.Count; i++)
        {
            if ((Mathf.Abs(coordinate.x - usedCoordinates[i].x) > 3 && Mathf.Abs(coordinate.y - usedCoordinates[i].y) > 3)
                && !Physics.Raycast(coordinate, Vector3.up, 2)
                && !Physics.Raycast(coordinate, Vector3.down, 2)
                && !Physics.Raycast(coordinate, Vector3.left, 2)
                && !Physics.Raycast(coordinate, Vector3.right, 2))
            {
                continue;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        if (JeCasNaMoby)
        {
            for (int i = 0; i < mobs.Length; i++)
            {
                mobs[i].transform.position = spawnCoordinates[i];
            }
            JeCasNaMoby = false;
        }
    }  
}
