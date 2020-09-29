using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGridGenerator : MonoBehaviour {

    public GameObject hexTile;
    public GameObject pill;

    int mapWidth = 13;
    int mapHeight = 12;

    float tileXoffset = 1.8f;
    float tileZoffset = 1.565f;

    // Use this for initialization
    void Start () {
        createHexTileMap();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

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
                    pillGO.transform.position = new Vector3(xaxis * tileXoffset + 2.95f, zaxis * tileZoffset - 0.8f, 0);
                    TileGO.transform.position = new Vector3(xaxis * tileXoffset, zaxis * tileZoffset, 0);
                    
                }
                else
                {
                    pillGO.transform.position = new Vector3(xaxis * tileXoffset + tileXoffset / 2 + 2.95f, zaxis * tileZoffset - 0.8f, 0);
                    TileGO.transform.position = new Vector3(xaxis * tileXoffset + tileXoffset / 2 , zaxis * tileZoffset, 0);
                }

            }
        }
    }
}
