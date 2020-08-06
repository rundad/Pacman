using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pill : MonoBehaviour {

    //The score that the player will earn when collects the pill
    private int points = 100;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //When collide with pacman(pacman collects the pill), set current object to inactive(disappear)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.name == "pacman")
        {
            this.gameObject.SetActive(false);
        }
    }

}
