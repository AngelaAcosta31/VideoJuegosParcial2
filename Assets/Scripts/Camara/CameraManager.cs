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
    private void AdjustOrCreateCameras(List<GameObject> playerTransforms)
    {
        for (int i = Cameras.Count; i < playerTransforms.Count; i++)
        {
            GameObject cameraGameObject = Instantiate(cameraPrefab, new Vector3(0, 5, -6), Quaternion.Euler(20, 0, 0));
            cameraGameObject.name = $"PlayerCamera{i + 1}";

            FollowCamara unityCamera = cameraGameObject.GetComponent<FollowCamara>();

            if (unityCamera == null)
            {
                unityCamera = cameraGameObject.AddComponent<FollowCamara>();
            }

            cameraGameObject.transform.SetParent(playerTransforms[i].transform);

            unityCamera.Target = playerTransforms[i];

            Transform positionCamaraTransform = playerTransforms[i].transform.Find("PositionCamera");
            if (positionCamaraTransform != null)
            {
                unityCamera.T = positionCamaraTransform.gameObject;
            }

            Cameras.Add(cameraGameObject);
            playerCameras.Add(cameraGameObject.GetComponent<Camera>());
        }
    }




    private void AdjustCameraView(int playerCount)
    {
        switch (playerCount)
        {
            case 2:
                playerCameras[0].rect = new Rect(0.5f, 0, 0.5f, 1);
                playerCameras[1].rect = new Rect(0f, 0f, 0.5f, 1);
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

