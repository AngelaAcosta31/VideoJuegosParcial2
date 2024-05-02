using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int initialCarCount;
    public GameObject m_CarPrefab;
    public Transform[] spawnPoints;

    public List<GameObject> m_CarPrefabList;
    
    private GameObject CurrentCar;

    public CameraManager cameraManager;


    void Start()
    {
        InitializeCars();
        cameraManager.SetupCameras(GetPlayerTransforms());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && m_CarPrefabList.Count < 4)
        {
            AddCar();
            cameraManager.SetupCameras(GetPlayerTransforms());
        }
    }

    private void InitializeCars()
    {
        for (int i = 0; i < initialCarCount; i++)
        {
            AddCar();
        }
    }

    void AddCar(){
         CurrentCar = Instantiate(m_CarPrefab, spawnPoints[m_CarPrefabList.Count].position,  spawnPoints[m_CarPrefabList.Count].rotation);
         m_CarPrefabList.Add(CurrentCar);
    }

    
    private List<GameObject> GetPlayerTransforms()
        {
            List<GameObject> transforms = new List<GameObject>();
            foreach (var car in m_CarPrefabList)
            {
                transforms.Add(car);
            }
            return transforms;
        }

    

}
