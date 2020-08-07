using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    //Animator object that pacman holding
    Animator animator;


    private Vector2 dest = Vector2.zero;
    //Pacman's speed
    private float speed = 0.2f;

    //Player's score
    private int score;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();

        //stay still when the game starts
        dest = transform.position;
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

    //Check pacman's next destination is valid or not
    private bool dirValid(Vector2 dir)
    {
        Vector2 pos = transform.position;//object current position
        RaycastHit2D hitObj = Physics2D.Linecast(pos + dir, pos);//return the object that are in the way that pacman is going
        return (hitObj.collider == GetComponent<Collider2D>() || hitObj.collider.gameObject.tag == "pill");
    }

    //Moves pacman's position instantly and update the destination value
    public void movePosition(Vector2 pos)
    {
        transform.position = pos;
        dest = pos;
    }

    public void addScore(int points)
    {
        score += points;
        print(score);
    }
}
