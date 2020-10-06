using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HexGridGenerator : MonoBehaviour {

    /// <summary>
    /// The hexagon tile
    /// </summary>
    public GameObject hexTile;

    /// <summary>
    /// The pill objects that are going to place in the maze
    /// </summary>
    public GameObject pill;
    
    /// <summary>
    /// The width of the maze
    /// </summary>
    private int mapWidth;

    /// <summary>
    /// The height of the maze
    /// </summary>
    private int mapHeight;
    
    /// <summary>
    /// The offset/distance of the tiles in the x axis
    /// </summary>
    private float tileXoffset = 1.8f;

    /// <summary>
    /// The offset/distance of the tiles in the y axis
    /// </summary>
    private float tileYoffset = 1.565f;

    /// <summary>
    /// The instance of HexGridManager
    /// </summary>
    private static HexGridManager instance;


    public GameObject pacman;

    // Use this for initialization
    void Start () {

        mapWidth = instance.getWidth();
        mapHeight = instance.getHeight();

        createHexTileMap();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// The function for generating the hexagonal grid maze
    /// </summary>
    private void createHexTileMap()
    {
        bool pacman_spawned = false;
        for (int xaxis = 0; xaxis < mapWidth; xaxis++)
        {
            for(int zaxis = 0; zaxis < mapHeight; zaxis++)
            {
                GameObject TileGO = Instantiate(hexTile);
                GameObject pillGO = Instantiate(pill);
                pillGO.AddComponent<CircleCollider2D>();
                pillGO.GetComponent<CircleCollider2D>().isTrigger = true;
                

                GameObject childGO = TileGO.transform.GetChild(0).gameObject;
                childGO.transform.position = new Vector3(childGO.transform.position.x, -0.8f, 0.8600707f);

                if(zaxis % 2 == 0)
                {
                    pillGO.transform.position = new Vector3(xaxis * tileXoffset + 2.95f, zaxis * tileYoffset - 0.8f, 0);
                    TileGO.transform.position = new Vector3(xaxis * tileXoffset, zaxis * tileYoffset, 0);
                    if (!pacman_spawned)
                    {
                        GameObject pacmanGO = Instantiate(pacman);
                        pacmanGO.transform.position = new Vector3(xaxis * tileXoffset + 2.95f, zaxis * tileYoffset - 0.8f, 0);
                        pacman_spawned = true;
                        pacmanGO.AddComponent<CircleCollider2D>();
                        pacmanGO.AddComponent<MovementController>();
                    }
                    
                }
                else
                {
                    pillGO.transform.position = new Vector3(xaxis * tileXoffset + tileXoffset / 2 + 2.95f, zaxis * tileYoffset - 0.8f, 0);
                    TileGO.transform.position = new Vector3(xaxis * tileXoffset + tileXoffset / 2 , zaxis * tileYoffset, 0);
                }
                pillGO.name = pillGO.transform.position + "";

            }
        }
    }

    /// <summary>
    /// The set method for setting the value of the instance property
    /// </summary>
    /// <param name="hgm"></param>
    public static void setInstance(HexGridManager hgm)
    {
        instance = hgm;
    }

}
