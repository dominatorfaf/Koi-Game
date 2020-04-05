using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiater : MonoBehaviour
{   
    public GameObject cellTemplate;

    float generationInterval = 0.7f;

    int[ , ] cellsArray;

    public int gridHeight;
    private int gridWidth;

    private float cellSize;

    // Start is called before the first frame update
    void Start(){
        gridWidth = Mathf.RoundToInt(gridHeight * Camera.main.aspect);

        cellsArray = new int[gridHeight, gridWidth];
        cellSize = (Camera.main.orthographicSize * 2) / gridHeight;
        print ("Dimensions: " + gridWidth + "X" + gridHeight);
        print("Cell size: " + cellSize);
        
        // fill random alive cells
        for (int i = 0; i < 80; i++){
            cellsArray[Random.Range(1, 14), Random.Range(3, 27)] = 1; 
        }

        InvokeRepeating("NewGenerationUpdate", generationInterval, generationInterval);
    }


    void NewGenerationUpdate(){

        RenderCells();
        ApplyRules();

    }

    void RenderCells(){
        foreach (GameObject cell in GameObject.FindGameObjectsWithTag("Cell")){
            Destroy(cell);
        }

        for (int i = 0; i < gridHeight; i++){
            for (int j = 0; j < gridWidth; j++){
                if (cellsArray[i, j] == 0) continue;
                Vector3 cellPosition = new Vector3(
                    j * cellSize + cellSize/2,
                    (cellSize * gridHeight) - (i * cellSize + cellSize/2),
                    0
                );
                Instantiate(cellTemplate, cellPosition, Quaternion.identity);
            }
        }

    }

    void ApplyRules(){
        int[ , ]nextGenGrid = new int[gridHeight, gridWidth];
        for (int i = 0; i < gridHeight; i++){
            for (int j = 0; j < gridWidth; j++){
                int livingNeighbours = CountLivingNeighbours(i, j);
                if (livingNeighbours == 3) { // reproduction, exactly 3 neighgbours
                    nextGenGrid[i, j] = 1;
                } else if (livingNeighbours == 2 && cellsArray[i, j] == 1) { // exactly 2 neigh, the live cell survives
                    nextGenGrid[i, j] = 1;
                }
                
            }
        }
        cellsArray = nextGenGrid; // GOING TO THE NEXT GEN!

    }

    int CountLivingNeighbours(int i, int j){
        int result = 0;
        for (int iNeigh = i-1; iNeigh < i+2; iNeigh++){ // i-1, i, i+1
            for (int jNeigh = j-1; jNeigh < j+2; jNeigh++){ // j-1, j, j+1
                if (iNeigh == i && jNeigh == j) continue;
                try{
                    result += cellsArray[iNeigh, jNeigh];
                }
                catch{}
            }
        }

        return result;
    }

}
