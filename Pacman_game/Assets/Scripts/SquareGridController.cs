using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareGridController : MonoBehaviour {

    /// <summary>
    /// The game start UI panel game object
    /// </summary>
    public GameObject startPanel;

    /// <summary>
    /// The game panel UI panel game object
    /// </summary>
    public GameObject gamePanel;

    /// <summary>
    /// The panel for the user to select the environment of the maze
    /// </summary>
    public GameObject environmentPanel;

    /// <summary>
    /// The start count downs game object
    /// </summary>
    public GameObject startCountDowns;

    /// <summary>
    /// A static instance of GameManager
    /// Initialised at the start of the game
    /// </summary>
    private static GameManager instance;

    // Use this for initialization
    void Start () {
        instance = GameManager.getInstance();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// This function plays the start count downs animation
    /// Starts a coroutine to perform the start count downs
    /// Disable the start panel
    /// </summary>
    public void onStartButton()
    {
        //StartCoroutine(startCountDown());
        startPanel.SetActive(false);
        environmentPanel.SetActive(true);
    }

    /// <summary>
    /// The function that defines the action of the Square grid environment has been selected by the player
    /// </summary>
    public void startSquareGrid()
    {
        StartCoroutine(startCountDown());
        environmentPanel.SetActive(false);
    }

    /// <summary>
    /// This function plays the start count downs.
    /// After playing the start count downs(about 4 second), destory the start count downs game object
    /// Enable all moveable game objects's movement
    /// Starts the game panel
    /// </summary>
    /// <returns></returns>
    IEnumerator startCountDown()
    {
        if(instance == null)
        {
            instance = GameManager.getInstance();
        }
        GameObject go = Instantiate(startCountDowns);
        yield return new WaitForSeconds(4f);
        Destroy(go);
        instance.setGameState(true);
        gamePanel.SetActive(true);
    }
}
