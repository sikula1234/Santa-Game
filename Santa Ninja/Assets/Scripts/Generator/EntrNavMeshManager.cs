using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntrNavMeshManager : MonoBehaviour
{
	public Transform[] entranceNavMeshes;
	public Entrance[] entrances;
	// Start is called before the first frame update
	void Start()
    {

	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void CloseNavMeshEntrances()
	{
		for (int i = 0; i < entranceNavMeshes.Length; i++)
		{
			if(entrances[i].isOpen == false)
			{
				entranceNavMeshes[i].gameObject.SetActive(false);
			}
		}
	}
}
