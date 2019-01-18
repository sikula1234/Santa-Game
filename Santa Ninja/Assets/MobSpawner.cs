using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobSpawner : MonoBehaviour
{
    public GameObject[] mobPrefabs;
    public GameObject[] mobs;
    public List<Vector2> spawnCoordinates = new List<Vector2>();
    public int pocetMobu = 6;
    public LayerMask layer;

	// Is called by LevelGen after level generation is complete
	public void SpawnMobs()
	{
		//GenerateCoordinates();

		mobs = new GameObject[pocetMobu]; //makes sure they match length
		for (int i = 0; i < mobs.Length; i++)
		{
			int rand = Random.Range(0, mobPrefabs.Length);
			mobs[i] = Instantiate(mobPrefabs[rand]) as GameObject;
			mobs[i].transform.position = GetRandomLocation();

		}
		/*
		for (int i = 0; i < mobs.Length; i++)
		{
			mobs[i].transform.position = spawnCoordinates[i];
			//mobs[i].transform.position = new Vector3(spawnCoordinates[i].x, spawnCoordinates[i].y, -2f);
		} */
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
            if ((Mathf.Abs(coordinate.x - usedCoordinates[i].x) > 2.5f && Mathf.Abs(coordinate.y - usedCoordinates[i].y) > 2.5f)
                && !Physics2D.Raycast(coordinate, Vector3.up, 1, layer)
                && !Physics2D.Raycast(coordinate, Vector3.down, 1, layer)
                && !Physics2D.Raycast(coordinate, Vector3.left, 1, layer)
                && !Physics2D.Raycast(coordinate, Vector3.right, 1, layer))
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

	Vector3 GetRandomLocation()
	{
		NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

		int maxIndices = navMeshData.indices.Length - 3;
		// Pick the first indice of a random triangle in the nav mesh
		int firstVertexSelected = Random.Range(0, maxIndices);
		int secondVertexSelected = Random.Range(0, maxIndices);
		//Spawn on Verticies
		Vector3 point = navMeshData.vertices[navMeshData.indices[firstVertexSelected]];

		Vector3 firstVertexPosition = navMeshData.vertices[navMeshData.indices[firstVertexSelected]];
		Vector3 secondVertexPosition = navMeshData.vertices[navMeshData.indices[secondVertexSelected]];
		//Eliminate points that share a similar X or Z position to stop spawining in square grid line formations
		if ((int)firstVertexPosition.x == (int)secondVertexPosition.x ||
			(int)firstVertexPosition.z == (int)secondVertexPosition.z
			)
		{
			point = GetRandomLocation(); //Re-Roll a position - I'm not happy with this recursion it could be better
		}
		else
		{
			// Select a random point on it
			point = Vector3.Lerp(
											firstVertexPosition,
											secondVertexPosition, //[t + 1]],
											Random.Range(0.05f, 0.95f) // Not using Random.value as clumps form around Verticies 
										);
		}
		//Vector3.Lerp(point, navMeshData.vertices[navMeshData.indices[t + 2]], Random.value); //Made Obsolete

		return point;
	}
}
