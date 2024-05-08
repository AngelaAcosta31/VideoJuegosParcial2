using System.Collections.Generic;
using UnityEngine;


public class CameraManager : MonoBehaviour
{
    public GameObject cameraPrefab;
    public List<GameObject> Cameras = new List<GameObject>();
    private List<Camera> playerCameras = new List<Camera>();
 
    public void SetupCameras(List<GameObject> playerTransforms)
    {
        AdjustOrCreateCameras(playerTransforms);
        AdjustCameraView(playerTransforms.Count);
    }
    private void AdjustOrCreateCameras(List<GameObject> playerList)
    {
        for (int i = Cameras.Count; i < playerList.Count; i++)
        {   
            GameObject cameraGameObject = playerList[i].transform.Find("Camara").gameObject;
            cameraGameObject.name = $"PlayerCamera{i + 1}";

            FollowCamara unityCamera = cameraGameObject.GetComponent<FollowCamara>();

            Cameras.Add(cameraGameObject);
            playerCameras.Add(cameraGameObject.GetComponent<Camera>());
        }
    }

    private void AdjustCameraView(int playerCount)
    {
        switch (playerCount)
        {
            case 2:
                playerCameras[0].rect = new Rect(0f, 0f, 0.5f, 1);
                playerCameras[1].rect = new Rect(0.5f, 0, 0.5f, 1);
                break;
                
            case 3:
                playerCameras[0].rect = new Rect(0f, 0.5f, 0.5f, 0.5f); // Arriba izquierda
                playerCameras[1].rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f); // Arriba derecha
                playerCameras[2].rect = new Rect(0f, 0f, 1f, 0.5f); // Abajo a lo largo
                break;
            case 4:
                playerCameras[0].rect = new Rect(0f, 0.5f, 0.5f, 0.5f); // Arriba izquierda
                playerCameras[1].rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f); // Arriba derecha
                playerCameras[2].rect = new Rect(0f, 0f, 0.5f, 0.5f); // Abajo izquierda
                playerCameras[3].rect = new Rect(0.5f, 0f, 0.5f, 0.5f); // Abajo derecha
                break;
        }
    }
}

