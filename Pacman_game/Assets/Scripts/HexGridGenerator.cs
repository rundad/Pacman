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
    private int mapWidth = 13;

    /// <summary>
    /// The height of the maze
    /// </summary>
    private int mapHeight = 12;
    
    /// <summary>
    /// The offset/distance of the tiles in the x axis
    /// </summary>
    private float tileXoffset = 1.8f;

    /// <summary>
    /// The offset/distance of the tiles in the y axis
    /// </summary>
    private float tileYoffset = 1.565f;

    /// <summary>
    /// 
    /// </summary>
    public GameObject sizePanel;

    /// <summary>
    /// 
    /// </summary>
    public GameObject environmentPanel;


    /// <summary>
    /// 
    /// </summary>
    public GameObject WidthInputField;

    /// <summary>
    /// 
    /// </summary>
    public GameObject HeightInputField;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// The function for generating the hexagonal grid maze
    /// </summary>
    private void createHexTileMap()
    {
        for(int xaxis = 0; xaxis <= mapWidth; xaxis++)
        {
            for(int zaxis = 0; zaxis <= mapHeight; zaxis++)
            {
                GameObject TileGO = Instantiate(hexTile);
                GameObject pillGO = Instantiate(pill);

                GameObject childGO = TileGO.transform.GetChild(0).gameObject;
                childGO.transform.position = new Vector3(childGO.transform.position.x, -0.8f, 0.8600707f);

                if(zaxis % 2 == 0)
                {
                    pillGO.transform.position = new Vector3(xaxis * tileXoffset + 2.95f, zaxis * tileYoffset - 0.8f, 0);
                    TileGO.transform.position = new Vector3(xaxis * tileXoffset, zaxis * tileYoffset, 0);
                    
                }
                else
                {
                    pillGO.transform.position = new Vector3(xaxis * tileXoffset + tileXoffset / 2 + 2.95f, zaxis * tileYoffset - 0.8f, 0);
                    TileGO.transform.position = new Vector3(xaxis * tileXoffset + tileXoffset / 2 , zaxis * tileYoffset, 0);
                }

            }
        }
    }

    /// <summary>
    /// The function that responds to the Hexgonal button click event
    /// Loads the scene of the hexagonal-grid maze environment
    /// </summary>
    public void startHexagonalGrid()
    {
        //TODO
        //Load the scene of the hexagonal-grid maze
        //createHexTileMap();
        SceneManager.LoadScene(1);
        
    }

    /// <summary>
    /// The function that responds to the Hexgonal button click event
    /// Loads the scene of the hexagonal-grid maze environment
    /// </summary>
    public void setMazeSize()
    {
        //TODO
        //Load the scene of the hexagonal-grid maze
        //createHexTileMap();
        //SceneManager.LoadScene(1);
        environmentPanel.SetActive(false);
        sizePanel.SetActive(true);
    }

    public void setWidth()
    {
        InputField widthIF = WidthInputField.GetComponent<InputField>();
        string value = widthIF.text;
        print(value);
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogError("Width is missing");
            return;
        }
        mapWidth = int.Parse(value);
    }

    public void setHeight()
    {
        InputField heightIF = HeightInputField.GetComponent<InputField>();
        string value = heightIF.text;
        print(value);
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogError("height is missing");
            return;
        }
        mapHeight = int.Parse(value);
    }
}
