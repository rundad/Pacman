using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour {

    /// <summary>
    /// The movement speed of the ghost
    /// </summary>
    private float speed = 6.0f;
    /// <summary>
    /// The float value for limiting the frequency of direction changing
    /// </summary>
    private float dirTime;
    /// <summary>
    /// The initial direction of the ghost
    /// </summary>
    public Vector2 direction = Vector2.up;
    /// <summary>
    /// The rigidbody component of the ghost
    /// </summary>
    private Rigidbody2D rb;
    /// <summary>
    /// The circle collider component of the ghost
    /// </summary>
    private CircleCollider2D cirColl;

    /// <summary>
    /// The variable that indicates the ghost is fronzen or not
    /// </summary>
    private bool fronzen = false;

    /// <summary>
    /// 
    /// </summary>
    private Vector2 resPos;

    /// <summary>
    /// 
    /// </summary>
    private int points = 400;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        cirColl = GetComponent<CircleCollider2D>();

        resPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (!fronzen)
        {
            //If the current direction is not valid
            if (!checkDirValid(direction))
            {
                if (canChangeDir())
                {
                    changeDir();
                }
                //current dir is not valid and collided with other game objects
                else if (rb.velocity.magnitude < speed)
                {
                    changeDirRandomly();
                }
            }
            else if (canChangeDir() && Time.time > dirTime)//if the current dir is valid, maybe change dir somewhere on the way
            {
                changeDirRandomly();
            }
            //collide with other game objects when the current dir is valid
            else if (rb.velocity.magnitude < speed)
            {
                changeDirRandomly();
            }

            changeEyesDir();
            movement();
        }
	}

    /// <summary>
    /// This function used to check the passed in direction is valid or not
    /// </summary>
    private bool checkDirValid(Vector2 dir)
    {
        //stores the game objects that hits by the collider shape casted by the circle collider
        RaycastHit2D[] hits = new RaycastHit2D[10];
        //The distance for the cast
        float distance = 1;
        //Cast a collider shape into the scene
        cirColl.Cast(dir, hits, distance, true);
        //If the array has 'wall' objects, the dir is not valid and return false
        foreach(RaycastHit2D hit in hits)
        {
            if(hit && hit.collider.gameObject.tag == "wall")
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// This function gets the perpendicular right and left direction
    /// Uses checkDirValid to check the two direction is valid or not
    /// return true if any of the two are valid
    /// </summary>
    /// <returns>The validation of the two directions</returns>
    private bool canChangeDir()
    {
        Vector2 right = DirectionUtil.GetPerpenRightDir(direction);
        bool canTurnRight = checkDirValid(right);
        Vector2 left = DirectionUtil.GetPerpenLeftDir(direction);
        bool canTurnLeft = checkDirValid(left);
        return canTurnRight || canTurnLeft;

    }

    /// <summary>
    /// This function used to change ghosts direction
    /// Starts from getting and checking the two directions(perpendicular right and left)
    /// If one of the direction is valid, choose the direction randomly
    /// Both directions are not valid, change to the opposite dir of the current dir
    /// </summary>
    private void changeDir()
    {
        dirTime = Time.time + 1;//Ghost cannot change dir for a second
        Vector2 right = DirectionUtil.GetPerpenRightDir(direction);
        bool canTurnRight = checkDirValid(right);
        Vector2 left = DirectionUtil.GetPerpenLeftDir(direction);
        bool canTurnLeft = checkDirValid(left);
        
        if (canTurnRight || canTurnLeft)
        {
            int randomDir = Random.Range(0, 2);
            if(!canTurnLeft || (randomDir == 0 && canTurnRight))
            {
                direction = right;
            }
            else
            {
                direction = left;
            }
        }
        else
        {
            direction = -direction;
        }
    }

    /// <summary>
    /// This function randomly changes the ghost direction
    /// Depends if the randon number is greater than zero
    /// </summary>
    private void changeDirRandomly()
    {
        dirTime = Time.time + 1;//Ghost cannot change dir for a second
        if (Random.Range(0, 2) > 0)
        {
            changeDir();
        }
    }

    /// <summary>
    /// The function that implements ghost's movement
    /// </summary>
    private void movement()
    {
        rb.velocity = direction * speed;
        if(rb.velocity.x == 0)
        {
            transform.position = new Vector2(Mathf.Round(transform.position.x), transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x, Mathf.Round(transform.position.y));
        }
    }

    /// <summary>
    /// This function changes the eyes direction of the ghost
    /// Gets the child transform objects of the current ghost object
    /// If the child transform is not the current objects transform, change the direction
    /// </summary>
    private void changeEyesDir()
    {
        foreach(Transform t in GetComponentInChildren<Transform>())
        {
            if(t != transform)
            {
                t.up = direction;
            }
        }
    }

    /// <summary>
    /// This function is used to detects the game objects that collided with the ghost
    /// If the collided object is pacman, sends the message to game manager
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (GameManager.getInstance().isSuperPacman)
            {
                resetPos();
                collision.gameObject.GetComponent<PlayerController>().addScore(points);
            }
            else
            {
                GameManager.pacmanDied();

            }
        }
    }

    /// <summary>
    /// This function defines the actions for freezing the ghosts
    /// </summary>
    /// <param name="state">Freeze state</param>
    public void freeze(bool state)
    {
        fronzen = state;
        rb.velocity = Vector2.zero;
    }

    /// <summary>
    /// This function used to reset the position of the ghost
    /// Resets the position by setting the transform position of the ghost
    /// </summary>
    public void resetPos()
    {
        transform.position = resPos;
    }
}
