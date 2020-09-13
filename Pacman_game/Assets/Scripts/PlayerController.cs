using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {


    /// <summary>
    /// Animator object that pacman holding
    /// </summary>
    Animator animator;

    /// <summary>
    /// The initial position of pacman
    /// </summary>
    private Vector2 dest = Vector2.zero;

    /// <summary>
    /// Pacman's movement speed
    /// </summary>
    private float speed = 0.2f;

    /// <summary>
    /// The total score that the player have earned during the game
    /// </summary>
    private int score;

    /// <summary>
    /// The state that indicates that pacman is still alive or not
    /// </summary>
    private bool pacman_alive;

    /// <summary>
    /// The score text UI on the canvas
    /// </summary>
    public Text scoreText;

    /// <summary>
    /// The total lives that the player has 
    /// </summary>
    private int lives = 3;

    /// <summary>
    /// The first life image of Pacman
    /// </summary>
    public Image life1;

    /// <summary>
    /// The second life image of Pacman
    /// </summary>
    public Image life2;

    /// <summary>
    /// The third life image of Pacman
    /// </summary>
    public Image life3;

    private Vector2 resPos;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();

        //stay still when the game starts
        dest = transform.position;
        resPos = transform.position;

        scoreText.text = "Score:\n\n" + 0;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //The temp vector which holds the next point position that towards to the destination
        Vector2 temp = Vector2.MoveTowards(transform.position, dest, speed);
        //Sets the position to the temp position using rigidbody
        GetComponent<Rigidbody2D>().MovePosition(temp);
        //When pacman reaches to the destination position, then able to do the next move
        if((Vector2)transform.position == dest)
        {
            //keyboard events
            if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && dirValid(Vector2.up))
            {//press up arrow key or w
                dest = (Vector2)transform.position + Vector2.up;//move up
                transform.up = Vector2.up;
            }
            if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && dirValid(Vector2.down))//press down arrow key or s
            {//press up arrow key or w
                dest = (Vector2)transform.position + Vector2.down;//move down
                transform.up = Vector2.down;
            }
            if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && dirValid(Vector2.left))//press left arrow key or a
            {//press up arrow key or w
                dest = (Vector2)transform.position + Vector2.left;//move left
                transform.up = Vector2.left;
            }
            if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && dirValid(Vector2.right))//press right arrow key or d
            {//press up arrow key or w
                dest = (Vector2)transform.position + Vector2.right;//move right
                transform.up = Vector2.right;
            }
        }
        //if pacman has been moved, the vector will not be 0.0, 0.0
        Vector2 dir = dest - (Vector2)transform.position;
        animator.SetBool("moving", (dir.x != 0 || dir.y != 0));
        
    }

    /// <summary>
    /// Check pacman's next destination is valid or not
    /// </summary>
    /// <param name="dir">The next destination point that pacman is going</param>
    /// <returns>Validation of the passed in parameter direction</returns>
    private bool dirValid(Vector2 dir)
    {
        Vector2 pos = transform.position;//object current position
        RaycastHit2D hitObj = Physics2D.Linecast(pos + dir, pos);//return the object that are in the way that pacman is going
        return (hitObj.collider == GetComponent<Collider2D>() || hitObj.collider.gameObject.tag == "pill");
    }

    /// <summary>
    /// Moves pacman's position instantly and update the destination value
    /// </summary>
    /// <param name="pos">The postion that pacman is going to move to</param>
    public void movePosition(Vector2 pos)
    {
        transform.position = pos;
        dest = pos;
    }

    /// <summary>
    /// Adds the score from collecting the pill to the total score
    /// </summary>
    /// <param name="points">The score that earns from collecting a pill</param>
    public void addScore(int points)
    {
        score += points;
        scoreText.text = "Score:\n\n" + score;
    }

    /// <summary>
    /// The function that sets the state for the pacman animation for changing animation to pacman died animation
    /// </summary>
    /// <param name="aliveState">The boolean value that states pacman is alive or not</param>
    public void setState(bool aliveState)
    {
        pacman_alive = aliveState;
        animator.SetBool("died", pacman_alive);
    }

    public void setLives()
    {
        lives = lives - 1;
        life1.enabled = lives > 0;
        life2.enabled = lives > 1;
        life3.enabled = lives > 2;
    }

    public int getLives()
    {
        return lives;
    }

    public void resetPos()
    {
        transform.position = resPos;
        dest = resPos;
    }
}
