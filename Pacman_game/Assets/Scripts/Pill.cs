using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pill{

    /// <summary>
    /// The score that the player will earn when collects the pill
    /// </summary>
    private int points = 100;

    /// <summary>
    /// The status that indicates the current pill object is a super pill object or not
    /// </summary>
    private bool isSuperPill = false;

	// Use this for initialization
	void Start () {
		
	}

    /// <summary>
    /// The get method of the points property
    /// Returns the value of the points attribute
    /// </summary>
    /// <returns></returns>
    public int getPoints()
    {
        return points;
    }

    /// <summary>
    /// The setter of the isSuperPill variable
    /// </summary>
    /// <param name="isSuper">A boolean value that indicates the value of isSuperPill</param>
    public void setSuper(bool isSuper)
    {
        isSuperPill = isSuper;
    }

    /// <summary>
    /// The get method of the isSuperPill property
    /// Returns the value of the isSuperPill attribute
    /// </summary>
    /// <returns></returns>
    public bool getSuper()
    {
        return isSuperPill;
    }

}
