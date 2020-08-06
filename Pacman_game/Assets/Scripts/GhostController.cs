using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour {

    /// <summary>
    /// The movement speed of the ghost
    /// </summary>
    private float speed = 2.0f;

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

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        cirColl = GetComponent<CircleCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        //changeDir();

        movement();
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
        cirColl.Cast(direction, hits, distance, true);
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
}
