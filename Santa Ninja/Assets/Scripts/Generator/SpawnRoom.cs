using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : MonoBehaviour
{
	public Vector3 spawnLocation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void SpawnSanta(GameObject santaPrefab)
	{
		GameObject santa = Instantiate(santaPrefab);
		santa.transform.parent = gameObject.transform;
		santa.transform.localPosition = spawnLocation;
		santa.transform.parent = null;
	}

	void OnDrawGizmosSelected()
	{
		// Draw a yellow sphere at the transform's position
		Gizmos.color = Color.blue;
		Gizmos.DrawSphere(spawnLocation, 0.5f);
	}
}
