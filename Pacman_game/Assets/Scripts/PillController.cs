using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillController : MonoBehaviour
{

    private Pill pillInstance;

    // Use this for initialization
    void Start () {
        pillInstance = new Pill();
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
            if (pillInstance.getSuper())
            {
                GameManager.getInstance().OnEatSuperPill();
            }
            collision.gameObject.GetComponent<PlayerController>().addScore(pillInstance.getPoints());
            GameManager.getInstance().OnEatPill(gameObject);
            this.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// The setter of the isSuperPill variable
    /// </summary>
    /// <param name="isSuper">A boolean value that indicates the value of isSuperPill</param>
    public void setSuper(bool isSuper)
    {
        pillInstance.setSuper(isSuper);
    }
}
