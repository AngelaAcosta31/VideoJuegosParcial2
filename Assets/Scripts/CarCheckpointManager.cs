using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCheckpointManager : MonoBehaviour
{
    public int CarID = 0;
    public int cpCrossed = 0;
    
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CheckPoint")){

            if (gameManager.isfinishedLap(CarID, cpCrossed)){
                gameManager.UpdateLaps(CarID);
                cpCrossed = 1;
            } else{
                cpCrossed += 1;
            }
            gameManager.CarCollectedCp(CarID,cpCrossed);
        }


    }
}
