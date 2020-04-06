using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldInstantiater : MonoBehaviour
{
    public GameObject tileTemplate;

    public int gridHeight;
    private int gridWidth;

    int[ , ] tileArray;
    private float tileSize;

    // Start is called before the first frame update
    void Start()
    {
        gridWidth = Mathf.RoundToInt(gridHeight * Camera.main.aspect);
        tileArray = new int[gridHeight, gridWidth];
        tileSize = (Camera.main.orthographicSize * 2) / gridHeight;
        print ("Grid Dimensions: " + gridWidth + "X" + gridHeight);
        print("Grid tile size: " + tileSize);
        GenerateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateGrid(){
         for (int i = 0; i < gridHeight; i++){
            for (int j = 0; j < gridWidth; j++){
                Vector3 tilePosition = new Vector3(
                    j * tileSize + tileSize/2,
                    (tileSize * gridHeight) - (i * tileSize + tileSize/2),
                    0
                );
                Instantiate(tileTemplate, tilePosition, Quaternion.identity);
            }
        }
    }
}
