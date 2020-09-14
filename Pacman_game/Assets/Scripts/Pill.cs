using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pill : MonoBehaviour {

    /// <summary>
    /// The score that the player will earn when collects the pill
    /// </summary>
    private int points = 100;

    /// <summary>
    /// 
    /// </summary>
    private bool isSuperPill = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// When collide with pacman(pacman collects the pill), set current object to inactive(disappear)
    /// If the current pill game object is a super pill game object, sends a message to the game manager to turn Pacman to super Pacman mode
    /// </summary>
    /// <param name="collision">The collision object</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.name == "pacman")
        {
            if (isSuperPill)
            {
                GameManager.getInstance().OnEatSuperPill();
            }
            collision.gameObject.GetComponent<PlayerController>().addScore(points);
            GameManager.getInstance().OnEatPill(gameObject);
            this.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isSuper"></param>
    public void setSuper(bool isSuper)
    {
        isSuperPill = isSuper;
    }

}
