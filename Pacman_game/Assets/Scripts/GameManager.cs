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
        PLAY, GAME_WON, PACMAN_KILLED, GAME_OVER
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
    /// The variable that stores the game object of the game over UI image
    /// </summary>
    public GameObject gameOverImage;

    /// <summary>
    /// The variable for storing the game object of the reset UI Text
    /// </summary>
    public GameObject resetText;

    /// <summary>
    /// A static instance of GameManager
    /// Initialised at the start of the game
    /// </summary>
    private static GameManager instance;

    /// <summary>
    /// The pacman game object in the maze
    /// </summary>
    public GameObject pacman;

    /// <summary>
    /// The pacman died animation clip
    /// </summary>
    public AnimationClip pacman_died_anim;

    /// <summary>
    /// The variable that used to delay the executing time of some logic
    /// </summary>
    private float delayTime;

    /// <summary>
    /// The array that stores the ghosts in the mze
    /// </summary>
    public List<GameObject> ghosts;

    /// <summary>
    /// The game start UI panel game object
    /// </summary>
    public GameObject startPanel;

    /// <summary>
    /// The game panel UI panel game object
    /// </summary>
    public GameObject gamePanel;

    /// <summary>
    /// The start count downs game object
    /// </summary>
    public GameObject startCountDowns;

	// Use this for initialization
	void Start () {
		if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        setGameState(false);
	}
	
	// Update is called once per frame
	void Update () {
        switch (gameState)
        {
            case FSMState.PLAY: UpdatePlayState(); break;
            case FSMState.GAME_WON: UpdateGameWonState(); break;
            case FSMState.PACMAN_KILLED: UpdatePacmanKilledState(); break;
            case FSMState.GAME_OVER: UpdateGameOverState(); break;
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
    /// When this function is called, activate the disabled UI image for displaying the game win message to the play,
    /// and activate the disabled UI text for displaying reset message to the player
    /// </summary>
    private void UpdateGameWonState()
    {
        winImage.SetActive(true);//activate game win UI image
        resetText.SetActive(true);//activate reset UI text
        setGameState(false);
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

    /// <summary>
    /// This function defines the actions for the PACKMAN_KILLED state of FSMState
    /// When pacman collided with ghosts, game state will changed to PACMAN_KILLED state and sets pacman inactive
    /// </summary>
    private void UpdatePacmanKilledState()
    {
        if(Time.time > delayTime)
        {
            pacman.SetActive(false);
            delayTime = Time.time + 1;
            gameState = FSMState.GAME_OVER;

        }
    }

    /// <summary>
    /// This function is for the communication between the game manager and the ghosts
    /// Ghosts tells the game manager that pacman has died
    /// Make transition to pacman's animation, change to die animation
    /// Change game state to PACMAN_KILLED state
    /// Setting delay time for executing afterwards actions after finished playing the animation
    /// </summary>
    public static void pacmanDied()
    {
        instance.pacman.GetComponent<PlayerController>().setState(true);
        instance.gameState = FSMState.PACMAN_KILLED;
        instance.delayTime = Time.time + instance.pacman_died_anim.length;
        foreach(GameObject ghost in instance.ghosts)
        {
            ghost.GetComponent<GhostController>().freeze(true);
        }
    }

    /// <summary>
    /// This function defines the actions for the GAME_OVER state of FSMState
    /// When the game state turned into game over state,
    /// activate the disabled reset text UI and game over image UI.
    /// Allows the player to reset the game by pressing down the enter/return key on the keyword in the game over state
    /// </summary>
    private void UpdateGameOverState()
    {
        if(Time.time > delayTime)
        {
            winImage.SetActive(false);
            gameOverImage.SetActive(true);
            resetText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                resetGame();
            }
        }
    }

    /// <summary>
    /// This function is used to freeze all moveable game objects in the maze.
    /// Disable all moveable game objects movement by disable game objects controller
    /// </summary>
    /// <param name="state"></param>
    private void setGameState(bool state)
    {
        pacman.GetComponent<PlayerController>().enabled = state;
        foreach(GameObject ghost in ghosts)
        {
            ghost.GetComponent<GhostController>().enabled = state;
        }
    }

    /// <summary>
    /// This function plays the start count downs animation
    /// Starts a coroutine to perform the start count downs
    /// Disable the start panel
    /// </summary>
    public void onStartButton()
    {
        StartCoroutine(startCountDown());
        startPanel.SetActive(false);
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
        GameObject go = Instantiate(startCountDowns);
        yield return new WaitForSeconds(4f);
        Destroy(go);
        setGameState(true);
        gamePanel.SetActive(true);
    }
}
