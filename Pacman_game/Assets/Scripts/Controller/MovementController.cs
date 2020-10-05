using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    private Vector2 movementInput;

    private Vector3 direction;


    bool hasMoved;

	// Use this for initialization
	void Start () {
		
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
    }

    public void move()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        movementInput.x = inputX;
        movementInput.y = inputY;
    }
}
