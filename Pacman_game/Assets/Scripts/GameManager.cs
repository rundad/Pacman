using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void UpdateGameWonState()
    {
        print("win");
    }
}
