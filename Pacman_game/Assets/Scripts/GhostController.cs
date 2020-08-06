using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour {

    public Vector2 direction = Vector2.up;
    private Rigidbody2D rb;
    private CircleCollider2D cirColl;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        cirColl = GetComponent<CircleCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //This function used to check the passed in direction is valid or not
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

    
}
