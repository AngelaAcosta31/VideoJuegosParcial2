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
    [HideInInspector] public  List<GameObject> m_CarsList;
    private GameObject CurrentCar;

    [Header("Configuración de Carrera")]
    public GameObject CheckPoint;
    public GameObject CheckPointContainer;
    private Transform[]  CheckPointPositions;
    private GameObject[] CheckPointForEachCar;
    public Vector3 checkpointScale = new Vector3(2.0f, 2.0f, 2.0f);
    public int m_nLaps;
    public int[] CurrentLapCar;
    private int totalCheckPoints;
    public int countdownTime = 5;

    void Start()
    {
        InitializeCars();
        cameraManager.SetupCameras(GetPlayerList());
        StartRace();
    }

    void Update()
    {
        int totalDevices = CalculateTotalInputDevices();
        if (Input.GetKeyDown(KeyCode.P) && m_CarsList.Count < 4 && m_CarsList.Count < totalDevices)
        { 
            AddCar();
            cameraManager.SetupCameras(GetPlayerList());
        }
    }
     public void StartRace()
    {
        StartCoroutine(CountdownTimer());
    }

    IEnumerator CountdownTimer()
    {

        while (countdownTime > 0)
        {
            Debug.Log("Tiempo restante: " + countdownTime);
            yield return new WaitForSeconds(1);
            countdownTime--;
        }
        Debug.Log("¡Comienza la carrera!");
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
        CurrentCar.GetComponent<MovimientoCarro>().RacePosition = m_CarsList.Count + 1;
        PlayerInput playerInput =  CurrentCar.GetComponent<PlayerInput>();
        CurrentCar.layer = 6 + m_CarsList.Count;      
        m_CarsList.Add(CurrentCar);
        Debug.Log($"Player {playerInput.playerIndex} joined with device: {playerInput.devices.Count}");
    }
    
    private List<GameObject> GetPlayerList()
        {
            List<GameObject> cars = new List<GameObject>();
            foreach (var car in m_CarsList)
            {
                cars.Add(car);
            }
            return cars;
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
        comparePosition(CarID);
    }

    public bool isfinishedLap(int CarID, int checkPointNumber){
        return checkPointNumber == totalCheckPoints - 1;
    }

    public void finishRaceIfLast(int RacePosition){
        if (RacePosition == totalCars){
            Debug.Log("Carrera Terminada");
        }
    }
    public void UpdateLaps(int CarID){
        CurrentLapCar[CarID] += 1;
    }

    void comparePosition(int CarID){
        
        if (m_CarsList[CarID].GetComponent<MovimientoCarro>().RacePosition > 1){
            GameObject currentCar = m_CarsList[CarID];
            int currentCarPosition = currentCar.GetComponent<MovimientoCarro>().RacePosition;
            int currentCarLap = currentCar.GetComponent<MovimientoCarro>().CurrentLap;
            int currentCarCheckPoint = currentCar.GetComponent<MovimientoCarro>().checkPointsCrossed;

            GameObject carInFront = null;
            int carInFrontPosition = 0;
            int carInFrontLap = 0;
            int carInFrontCheckPoint = 0;

            for(int i = 0; i < totalCars; i++){
                if (m_CarsList[i].GetComponent<MovimientoCarro>().RacePosition == currentCarPosition - 1 ){
                    carInFront = m_CarsList[i];
                    carInFrontPosition = carInFront.GetComponent<MovimientoCarro>().RacePosition;
                    carInFrontLap = carInFront.GetComponent<MovimientoCarro>().CurrentLap;
                    carInFrontCheckPoint = carInFront.GetComponent<MovimientoCarro>().checkPointsCrossed;
                    break;
                }
            }
            if  (currentCarLap == carInFrontLap){
                if (currentCarCheckPoint > carInFrontCheckPoint){
                    currentCar.GetComponent<MovimientoCarro>().RacePosition = currentCarPosition - 1;
                    carInFront.GetComponent <MovimientoCarro>().RacePosition = carInFrontPosition + 1;
                }
            } else if (currentCarLap > carInFrontLap){
                currentCar.GetComponent<MovimientoCarro>().RacePosition = currentCarPosition - 1;
                carInFront.GetComponent <MovimientoCarro>().RacePosition = carInFrontPosition + 1;
            }
        } 
    }
    
    int CalculateTotalInputDevices(){
        int gamepadCount = Gamepad.all.Count;
        int KeyboardMouse = 1;
        return gamepadCount + KeyboardMouse;
    }
}
