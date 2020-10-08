using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    /// <summary>
    /// The variable that stores the user input
    /// </summary>
    private Vector2 movementInput;

    /// <summary>
    /// The direction of pacman
    /// </summary>
    private Vector3 direction;

    /// <summary>
    /// The boolean that indicates pacman has moved or not
    /// </summary>
    private bool hasMoved;

    /// <summary>
    /// The animator component of the current object
    /// </summary>
    Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        move();
		if(movementInput.x == 0)
        {
            hasMoved = false;
        }else if(movementInput.x != 0 && !hasMoved)
        {
            hasMoved = true;
            GetMovementDirection();
        }


        
	}

    /// <summary>
    /// The function that defines the action for the movement of the user input
    /// </summary>
    public void GetMovementDirection()
    {
        if(movementInput.x < 0)
        {
            if(movementInput.y > 0)
            {
                direction = new Vector3(-0.9f, 1.565f);
                
            }else if(movementInput.y < 0)
            {
                direction = new Vector3(-0.9f, -1.565f);
            }
            else
            {
                direction = new Vector3(-1.8f, 0, 0);
            }
            transform.position += direction;
        }else if(movementInput.x > 0)
        {
            if(movementInput.y > 0)
            {
                direction = new Vector3(0.9f, 1.565f);
            }else if(movementInput.y < 0)
            {
                direction = new Vector3(0.9f, -1.565f);
            }
            else
            {
                direction = new Vector3(1.8f, 0, 0);
           
            }
            transform.position += direction;
        }
        transform.up = direction;
        Vector3 pos = transform.position;
        pos.x += 0.05f;
        GameObject go = GameObject.Find(pos + "");
        Transform tf = null;
        if (go != null)
        {
             tf = go.transform;

        }

        if (tf)
        {
            if (transform.position == tf.position)
            {
                go.SetActive(false);
            }
        }
        animator.SetBool("moving", (direction.x != 0 || direction.y != 0));
    }

    /// <summary>
    /// The function used to fetch the user movement input
    /// Stores the input to the movementInput variable
    /// </summary>
    public void move()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        movementInput.x = inputX;
        movementInput.y = inputY;
    }

    /// <summary>
    /// The function that detects the collision of the object that the script attached to
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.gameObject.name);
        if(collision.gameObject.tag == "pill")
        {
            collision.gameObject.SetActive(false);
        }
    }
}
