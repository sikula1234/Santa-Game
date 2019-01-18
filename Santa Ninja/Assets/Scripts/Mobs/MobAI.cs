using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobAI : MonoBehaviour
{
	public float destinationRadius;
	public float floatCekani1;
	public float floatCekani2;
	NavMeshAgent navMeshAgent;

	bool mobIsWaiting;

    // Start is called before the first frame update
    void Start()
    {
		navMeshAgent = transform.GetComponent<NavMeshAgent>();
		MoveMob();
    }

    // Update is called once per frame
    void Update()
    {
        if(MobArrived() && !mobIsWaiting)
		{
			StartCoroutine(WaitOnPoint());
		}
    }

	Vector3 FindDestination()
	{
		NavMeshPath path = new NavMeshPath();

		int i = 0;
		while(i < 100)
		{
			float x = Random.Range(-destinationRadius, destinationRadius);
			float z = Random.Range(-destinationRadius, destinationRadius);
			Vector3 targetPosition = new Vector3(x + transform.position.x, 0, z + transform.position.z);

			if (navMeshAgent.CalculatePath(targetPosition, path))
			{
				return targetPosition;
			}
			i++;
		}

		return transform.position;
	}

	void MoveMob()
	{
		navMeshAgent.SetDestination(FindDestination());
		mobIsWaiting = false;
	}

	IEnumerator WaitOnPoint()
	{
		mobIsWaiting = true;
		float randomTime = Random.Range(floatCekani1, floatCekani2);
		yield return new WaitForSecondsRealtime(randomTime);
		MoveMob();
	}

	bool MobArrived()
	{
		if (!navMeshAgent.pathPending)
		{
			if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
			{
				if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
				{
					return true;
				}
			}
		}
		return false;
	}
}
