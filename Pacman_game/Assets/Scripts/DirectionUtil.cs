using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionUtil : MonoBehaviour {

    /// <summary>
    /// An array stores four directions
    /// </summary>
    public static Vector2[] directions = new Vector2[]
    {
        Vector2.up,
        Vector2.right,
        Vector2.down,
        Vector2.left
    };

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Returns the perpendicular right direction of the passed in direction
    /// </summary>
    /// <param name="d">Direction of the ghost</param>
    /// <returns>new direction</returns>
    public static Vector3 GetPerpenRightDir(Vector2 d)
    {
        for(int i = 0; i < directions.Length; i++)
        {
            Vector2 dir = directions[i];
            if(dir == d)
            {
                int nextDir = i + 1;
                if(nextDir == directions.Length)
                {
                    nextDir = 0;
                }
                return directions[nextDir];
            }
        }
        throw new KeyNotFoundException("Vector " + d + " is not a valid vector");
    }

    /// <summary>
    /// Returns the perpendicular left direction of the passed in direction
    /// </summary>
    /// <param name="d">Direction of the ghost</param>
    /// <returns>new direction</returns>
    public static Vector3 GetPerpenLeftDir(Vector2 d)
    {
        for (int i = 0; i < directions.Length; i++)
        {
            Vector2 dir = directions[i];
            if (dir == d)
            {
                int nextDir = i - 1;
                if (nextDir < 0)
                {
                    nextDir = directions.Length - 1;
                }
                return directions[nextDir];
            }
        }
        throw new KeyNotFoundException("Vector " + d + " is not a valid vector");
    }
}
