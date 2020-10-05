using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

    //The teleport position
    public Vector2 teleport_to;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Teleport the game objects that collide with this object
    /// </summary>
    /// <param name="collision">The collision object</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If the collide game object is pacman, use the function in pacman for teleporting
        if (collision.gameObject.name == "pacman")
        {
            collision.gameObject.GetComponent<PlayerController>().movePosition(teleport_to);
        }
        //If the game object are other game objects, just set their position to the teleport_to position
        else
        {
            collision.gameObject.transform.position = teleport_to;
        }
    }
}
