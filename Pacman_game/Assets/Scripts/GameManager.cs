using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    /// <summary>
    /// Finite State Machine
    /// The enum for storing the game states of the game
    /// </summary>
    public enum FSMState
    {
        PLAY, GAME_WON
    }

    /// <summary>
    /// The initial game state when the game started
    /// </summary>
    private FSMState gameState = FSMState.PLAY;

    /// <summary>
    /// The array that stores all of the pill objects in the maze
    /// </summary>
    public List<GameObject> pills;

    /// <summary>
    /// The variable that stores the game object of the game win UI image
    /// </summary>
    public GameObject winImage;

    /// <summary>
    /// The variable for storing the game object of the reset UI Text
    /// </summary>
    public GameObject resetText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        switch (gameState)
        {
            case FSMState.PLAY: UpdatePlayState(); break;
            case FSMState.GAME_WON: UpdateGameWonState(); break;
        }
	}

    /// <summary>
    /// This function defines the actions for the PLAY state of FSMState
    /// This function will check if any of the pill is still active in the maze
    /// If all of the pills are inactive, the game state will change to GAME_WON state
    /// </summary>
    private void UpdatePlayState()
    {
        bool flag = false;
        foreach(GameObject pill in pills)
        {
            if (pill.activeSelf)
            {
                flag = true;
                break;
            }
        }

        if (!flag)
        {
            gameState = FSMState.GAME_WON;
        }
    }

    /// <summary>
    /// This function defines the actions for the GAME_WON state of FSMState
    /// When this function is called, enable the disabled UI image for displaying the game win message to the play,
    /// and enables the disabled UI text for displaying reset message to the player
    /// </summary>
    private void UpdateGameWonState()
    {
        winImage.SetActive(true);//enable game win UI image
        resetText.SetActive(true);//enable reset UI text
        if (Input.GetKeyDown(KeyCode.Return))
        {
            resetGame();
        }
    }

    /// <summary>
    /// This function defines the actions for reseting the game
    /// Resets the game by reload the scene again
    /// </summary>
    public void resetGame()
    {
        SceneManager.LoadScene(0);//loads the first scene in the build settings
    }
}
