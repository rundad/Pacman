using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HexGridManager : MonoBehaviour {

    /// <summary>
    /// The width of the maze
    /// </summary>
    private int mapWidth = 13;

    /// <summary>
    /// The height of the maze
    /// </summary>
    private int mapHeight = 12;

    /// <summary>
    /// The panel that allows the user to enter the size of the maze
    /// </summary>
    public GameObject sizePanel;

    /// <summary>
    /// The select panel for selecting the type of the maze
    /// </summary>
    public GameObject environmentPanel;


    /// <summary>
    /// The input field for the width of the maze
    /// </summary>
    public GameObject WidthInputField;

    /// <summary>
    /// The input field for the heigth of the maze
    /// </summary>
    public GameObject HeightInputField;

    /// <summary>
    /// The instance of HexGridManager
    /// </summary>
    public HexGridManager instance;

    // Use this for initialization
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// The function that responds to the Hexgonal button click event
    /// Checks the width and height of the maze appropriate or not
    /// Loads the scene of the hexagonal-grid maze environment
    /// </summary>
    public void startHexagonalGrid()
    {
        //TODO
        //Load the scene of the hexagonal-grid maze
        //createHexTileMap();
        bool widthValid = mapWidth >= 8 && mapWidth <=13;
        bool heightValid = mapHeight >= 8 && mapHeight <= 13;
        if(!(widthValid && heightValid))
        {
            Debug.LogError("Inappropriate width or height");
            return;
        }
        HexGridGenerator.setInstance(instance);
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

    /// <summary>
    /// The callback function of retriving the value of the WidthInputField
    /// And assign the value to the width property
    /// </summary>
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

    /// <summary>
    /// The callback function of retriving the value of the HeightInputField
    /// And assign the value to the height property
    /// </summary>
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

    /// <summary>
    /// Return the width of the maze
    /// </summary>
    /// <returns></returns>
    public int getWidth()
    {
        return mapWidth;
    }

    /// <summary>
    /// Return the height of the maze
    /// </summary>
    /// <returns></returns>
    public int getHeight()
    {
        return mapHeight;
    }

}
