using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGridGenerator : MonoBehaviour {

    public GameObject hexTile;

    int mapWidth = 25;
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
        for(int x = 0; x <= mapWidth; x++)
        {
            for(int z = 0; z <= mapHeight; z++)
            {
                GameObject TileGO = Instantiate(hexTile);
                if(z % 2 == 0)
                {
                    TileGO.transform.position = new Vector3(x * tileXoffset, 0, z * tileZoffset);
                }
                else
                {
                    TileGO.transform.position = new Vector3(x * tileXoffset + tileXoffset / 2, 0, z * tileZoffset);
                }
            }
        }
    }
}
