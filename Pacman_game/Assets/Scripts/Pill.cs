using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pill : MonoBehaviour {

    /// <summary>
    /// The score that the player will earn when collects the pill
    /// </summary>
    private int points = 100;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// When collide with pacman(pacman collects the pill), set current object to inactive(disappear)
    /// </summary>
    /// <param name="collision">The collision object</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.name == "pacman")
        {
            collision.gameObject.GetComponent<PlayerController>().addScore(points);
            this.gameObject.SetActive(false);
        }
    }

}
