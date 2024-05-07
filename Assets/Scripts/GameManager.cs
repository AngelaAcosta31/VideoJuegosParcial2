using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class GameManager : MonoBehaviour
{

    [Header("Configuración de Camara")]
    public CameraManager cameraManager;

    [Header("Configuración de jugadores")]
    public int totalCars;
    public GameObject m_CarPrefab;
    public Transform[] spawnPoints;
    public  List<GameObject> m_CarsList;
    private GameObject CurrentCar;

    [Header("Configuración de Carrera")]
    public GameObject CheckPoint;
    public GameObject CheckPointContainer;
    public Transform[]  CheckPointPositions;
    public GameObject[] CheckPointForEachCar;
    public Vector3 checkpointScale = new Vector3(2.0f, 2.0f, 2.0f);
    public int[] CurrentLapCar;

    private int totalCheckPoints;


    void Start()
    {
        InitializeCars();
        cameraManager.SetupCameras(GetPlayerTransforms());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && m_CarsList.Count < 4)
        {
            AddCar();
            cameraManager.SetupCameras(GetPlayerTransforms());
        }
    }

    private void InitializeCars()
    {
        for (int i = 0; i < totalCars; i++)
        {
            AddCar();
        }
    }

    void AddCar(){
        CurrentCar = Instantiate(m_CarPrefab, spawnPoints[m_CarsList.Count].position,  spawnPoints[m_CarsList.Count].rotation);
        CurrentCar.name = $"Carro {m_CarsList.Count}";
        CurrentCar.GetComponent<MovimientoCarro>().CarID = m_CarsList.Count;
        PlayerInput playerInput =  CurrentCar.GetComponent<PlayerInput>();
        CurrentCar.layer = 6 + m_CarsList.Count;      
        m_CarsList.Add(CurrentCar);
        Debug.Log($"Player {playerInput.playerIndex} joined with device: {playerInput.devices.Count}");
    }
    

    private List<GameObject> GetPlayerTransforms()
        {
            List<GameObject> transforms = new List<GameObject>();
            foreach (var car in m_CarsList)
            {
                transforms.Add(car);
            }
            return transforms;
        }

     public void setCheckPoints(){
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
}
