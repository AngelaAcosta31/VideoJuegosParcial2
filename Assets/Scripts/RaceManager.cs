using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public GameObject CheckPoint;
    public GameObject CheckPointContainer;
    public GameObject[] Cars;
    public Transform[]  CheckPointPositions;
    public GameObject[] CheckPointForEachCar;
    public Vector3 checkpointScale = new Vector3(2.0f, 2.0f, 2.0f);

    public int[] CurrentLapCar;

    private int totalCars;
    private int totalCheckPoints;

    public void setCheckPoints(){
        totalCars = Cars.Length;
        totalCheckPoints = CheckPointContainer.transform.childCount;
        CheckPointPositions = new Transform[totalCheckPoints];
        CurrentLapCar = new int[totalCars];
        for (int i = 0; i < totalCheckPoints; i++){
            CheckPointPositions[i] = CheckPointContainer.transform.GetChild(i).transform;
        } 

        CheckPointForEachCar = new GameObject[totalCars];
        for (int i = 0; i < totalCars; i++){
            CheckPointForEachCar[i] = Instantiate(CheckPoint, CheckPointPositions[0].position, CheckPointPositions[0].rotation);
            CheckPointForEachCar[i].transform.localScale = checkpointScale;
            CheckPointForEachCar[i].name= $"Checkpoint Player{i + 1}"; 
            CheckPointForEachCar[i].layer = 6 + i;           
        }
    }

    public void CarCollectedCp(int CarID, int checkPointNumber){
        CheckPointForEachCar[CarID].transform.position = CheckPointPositions[checkPointNumber].position;
        CheckPointForEachCar[CarID].transform.rotation = CheckPointPositions[checkPointNumber].rotation;    
    }

    public bool isfinishedLap(int CarID, int checkPointNumber){
        return checkPointNumber == totalCheckPoints - 1;
    }

    public void UpdateLaps(int CarID){
        CurrentLapCar[CarID] += 1;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M)){
            setCheckPoints();
        }
    }
}
