using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    //Animator object that pacman holding
    Animator animator;


    private Vector2 dest = Vector2.zero;
    //Pacman's speed
    private float speed = 0.1f;

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

        //keyboard events
        if((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))){//press up arrow key or w
            dest = (Vector2)transform.position + Vector2.up;//move up
            transform.up = Vector2.up;
        }
        if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)))//press down arrow key or s
        {//press up arrow key or w
            dest = (Vector2)transform.position + Vector2.down;//move down
            transform.up = Vector2.down;
        }
        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)))//press left arrow key or a
        {//press up arrow key or w
            dest = (Vector2)transform.position + Vector2.left;//move left
            transform.up = Vector2.left;
        }
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)))//press right arrow key or d
        {//press up arrow key or w
            dest = (Vector2)transform.position + Vector2.right;//move right
            transform.up = Vector2.right;
        }
    }
}
