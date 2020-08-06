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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "pacman")
        {
            collision.gameObject.GetComponent<PlayerController>().move_position(teleport_to);
        }
        else
        {
            //collision.gameObject.transform.position = teleport_to;
        }
    }
}
