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
        PLAY, GAME_WON, PACMAN_KILLED, PACMAN_DYING, GAME_OVER
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
    /// The panel for the user to select the environment of the maze
    /// </summary>
    public GameObject environmentPanel;

    /// <summary>
    /// The start count downs game object
    /// </summary>
    public GameObject startCountDowns;

    /// <summary>
    /// The status of is Pacman in super pacman mode
    /// </summary>
    public bool isSuperPacman = false;

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

        //create a super pill after 10 second of the game starts
        Invoke("createSuperPill", 10f);
    }
	
	// Update is called once per frame
	void Update () {
        switch (gameState)
        {
            case FSMState.PLAY: UpdatePlayState(); break;
            case FSMState.GAME_WON: UpdateGameWonState(); break;
            case FSMState.PACMAN_KILLED: UpdatePacmanKilledState(); break;
            case FSMState.PACMAN_DYING: UpdatePacmanDyingState(); break;
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
        gamePanel.SetActive(false);
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
    private void resetGame()
    {
        SceneManager.LoadScene(0);//loads the first scene in the build settings
    }

    /// <summary>
    /// This function defines the actions for the PACKMAN_KILLED state of FSMState
    /// When the death animation of Pacman has finished playing, it will turned into the Pacman Killed state
    /// Since we have added the life system for Pacman: the player will have 3 lifes in total to complete the challenge,
    /// if the player still has lives left over, the game state will turn back to the normal play state, otherwise, the game state will move on to the game over state
    /// 
    /// If back onto the play state: Update and retrieve player's lives and sets the Pacman back onto active
    /// 
    /// If move onto the game over state: set the game state to game over state
    /// 
    /// reset all of the ghosts position back to their respawn position
    /// un-freeze the ghosts
    /// </summary>
    private void UpdatePacmanKilledState()
    {
        if(Time.time > delayTime)
        {
            gameState = FSMState.PLAY;
            pacman.GetComponent<PlayerController>().setLives();
            if(pacman.GetComponent<PlayerController>().getLives() > 0)
            {
                pacman.SetActive(true);
                pacman.GetComponent<PlayerController>().resetPos();
                pacman.GetComponent<PlayerController>().setState(false);
            }
            else
            {
                gameState = FSMState.GAME_OVER;
            }
            foreach(GameObject ghost in ghosts)
            {
                ghost.GetComponent<GhostController>().resetPos();
                ghost.GetComponent<GhostController>().freeze(false);
            }

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
        instance.gameState = FSMState.PACMAN_DYING;
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
            foreach (GameObject ghost in ghosts)
            {
                ghost.GetComponent<GhostController>().freeze(true);
                ghost.GetComponent<GhostController>().enabled = false;
            }
            gamePanel.SetActive(false);
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
    /// The function that responds to the Hexgonal button click event
    /// Loads the scene of the hexagonal-grid maze environment
    /// </summary>
    public void startHexagonalGrid()
    {
        //TODO
        //Load the scene of the hexagonal-grid maze
    }

    /// <summary>
    /// The function that responds to the Graph button click event
    /// Loadst he scene of the arbitrary graph maze environment
    /// </summary>
    public void startGraph()
    {
        //TODO
        //Load the scene of the arbitrary graph maze
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

    /// <summary>
    /// This function defines the actions of Pacman's dying state
    /// Changes the game state to pacman killed state and sets the delay time by adding 1 to make the actions looks smooth
    /// Disable Pacman in the scene
    /// </summary>
    private void UpdatePacmanDyingState()
    {
        if(Time.time > delayTime)
        {
            gameState = FSMState.PACMAN_KILLED;
            delayTime = Time.time + 1;
            pacman.SetActive(false);
        }
    }

    /// <summary>
    /// The get method of the GameManager instance
    /// </summary>
    /// <returns>The instance of class GameManager</returns>
    public static GameManager getInstance()
    {
        return instance;
    }

    /// <summary>
    /// The function used to remove the eaten pill game object from the list
    /// </summary>
    /// <param name="go">The eaten pill game object</param>
    public void OnEatPill(GameObject go)
    {
        pills.Remove(go);
    }

    /// <summary>
    /// This function defines the actions when a super pill game object has been eaten by Pacman
    /// Change Pacman to super Pacman mode - has the ability of killing ghosts
    /// Freeze the ghosts and un-freeze them after 3 seconds
    /// 
    /// Create another super pill game object after 10 seconds
    /// </summary>
    public void OnEatSuperPill()
    {
        Invoke("createSuperPill", 10f);
        isSuperPacman = true;
        freezeGhosts();
        StartCoroutine(ghostRecovery());
    }

    /// <summary>
    /// This function used to create super pill game object with randomness
    /// Gets a random number and change a normal pill to a super pill
    /// </summary>
    private void createSuperPill()
    {
        if(pills.Count != 0)
        {
            int temp = Random.Range(0, pills.Count);
            pills[temp].transform.localScale = new Vector3(3, 3, 3);
            pills[temp].GetComponent<Pill>().setSuper(true);
        }
    }

    /// <summary>
    /// This function used to freeze the ghosts when Pacman has eaten a super pill
    /// Set ghosts velocity to zero and disabled ghosts script
    /// Repaint ghosts color
    /// </summary>
    private void freezeGhosts()
    {
        foreach(GameObject ghost in ghosts)
        {
            Color color = ghost.GetComponent<SpriteRenderer>().color;
            color.a = color.a - 0.3f;
            ghost.GetComponent<GhostController>().freeze(true);
            ghost.GetComponent<GhostController>().enabled = false;
            ghost.GetComponent<SpriteRenderer>().color = color;

        }
    }

    /// <summary>
    /// This function used to un-freeze the ghosts after 3 seconds of super pacman mode
    /// Enable ghosts script
    /// Repaint ghosts color
    /// </summary>
    private void unfreezeGhosts()
    {
        foreach (GameObject ghost in ghosts)
        {
            Color color = ghost.GetComponent<SpriteRenderer>().color;
            color.a = 1.0f;
            ghost.GetComponent<GhostController>().freeze(false);
            ghost.GetComponent<GhostController>().enabled = true;
            ghost.GetComponent<SpriteRenderer>().color = color;

        }
    }

    /// <summary>
    /// This function used to change super Pacman back to normal Pacman
    /// Once Pacman has eaten a super pill, Pacman will turn into super Pacman mode for 3 second
    /// After 3 seconds of super pacman mode, unfreeze the ghosts
    /// </summary>
    /// <returns></returns>
    IEnumerator ghostRecovery()
    {
        yield return new WaitForSeconds(3.0f);
        unfreezeGhosts();
        isSuperPacman = false;
    }
}
