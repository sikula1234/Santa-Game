using UnityEngine.SceneManagement;
using UnityEngine;

public class Santa : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Die()
	{
		Debug.Log("Dead :D");
		SceneManager.LoadScene("TestLevel", LoadSceneMode.Single);
	}
}
