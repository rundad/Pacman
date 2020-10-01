using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{

    /// <summary>
    /// Pacman's movement speed
    /// </summary>
    private float speed = 0.2f;

    /// <summary>
    /// The total lives that the player has 
    /// </summary>
    private int lives = 3;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// This function used to update player's lives
    /// This function will be called when the game state has turned into pacman killed state：when pacman is killed, decrease 1 life
    /// And also update the life images
    /// </summary>
    public void setLives()
    {
        lives = lives - 1;
        
    }

    /// <summary>
    /// This function used to return the player's lifes
    /// </summary>
    /// <returns>lives: the number of lives that the player has</returns>
    public int getLives()
    {
        return lives;
    }

    /// <summary>
    /// The get method for the speed property
    /// </summary>
    /// <returns></returns>
    public float getSpeed()
    {
        return speed;
    }

    
}
