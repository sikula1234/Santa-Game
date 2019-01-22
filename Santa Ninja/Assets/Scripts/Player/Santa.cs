using UnityEngine;

public class Santa : MonoBehaviour {

	SceneTransitions sceneTransitions;

	// Use this for initialization
	void Start () {
		sceneTransitions = FindObjectOfType<SceneTransitions>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Die()
	{
		sceneTransitions.LoadScene("TestLevel", 0);
        Debug.Log("Nazdar");
	}
}
