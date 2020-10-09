using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GraphManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// The function that responds to the Graph button click event
    /// Loadst he scene of the arbitrary graph maze environment
    /// </summary>
    public void startGraph()
    {
        SceneManager.LoadScene(2);
    }
}
