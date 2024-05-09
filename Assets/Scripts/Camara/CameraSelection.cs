using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CameraSelection : MonoBehaviour
{
    public GameObject cameraPrefab;
    public PlayerInputManager PlayerManager;
    public List<GameObject> Players = new List<GameObject>();
    public List<Camera> playerCameras =  new List<Camera>();
    public List<GameObject> Spawn; 
    int countdownTime = 5;


    bool start = false;

    void Update()
    {
        startGame();
    }
    public void SetupCameras()
    {
        AdjustOrCreateCameras();
        AdjustCameraView();
    }
    private void AdjustOrCreateCameras()
    {
        for (int i = playerCameras.Count; i < PlayerManager.playerCount; i++)
        {   
            Camera cameraGameObject = Players[i].GetComponent<CharacterSelection>().camara;
            Players[i].transform.position = Spawn[i].transform.position;
            playerCameras.Add(cameraGameObject);
        }
    }

    private void AdjustCameraView()
    {
        switch (PlayerManager.playerCount)
        {
            case 2:
                playerCameras[0].rect = new Rect(0f, 0f, 0.5f, 1);
                playerCameras[1].rect = new Rect(0.5f, 0, 0.5f, 1);
                break;
                
            case 3:
                playerCameras[0].rect = new Rect(0f, 0f, 0.33f, 1f); // Arriba izquierda
                playerCameras[1].rect = new Rect(0.33f, 0f, 0.33f, 1f); // Arriba derecha
                playerCameras[2].rect = new Rect(0.66f, 0f, 0.33f, 1f); // Abajo a lo largo
                break;
            case 4:
                playerCameras[0].rect = new Rect(0f, 0f, 0.25f, 1f); // Arriba izquierda
                playerCameras[1].rect = new Rect(0.25f, 0f, 0.25f, 1f); // Arriba derecha
                playerCameras[2].rect = new Rect(0.5f, 0f, 0.25f, 1f); // Abajo izquierda
                playerCameras[3].rect = new Rect(0.75f, 0f, 0.25f, 1f); // Abajo derecha

                playerCameras[0].fieldOfView =25;
                playerCameras[1].fieldOfView =25;
                playerCameras[2].fieldOfView =25;
                playerCameras[3].fieldOfView =25;
                break;
        }
    }

    void startGame(){

        foreach( GameObject Player in Players){
            if (!Player.GetComponent<CharacterSelection>().Ready){
                start = false;
                break;
            } else{
                start = true;
            }
        }

        if (start){
            StartCoroutine(CountdownTimer());
            if (countdownTime == 0){
                 SceneManager.LoadScene("pista test");
            }
        }
    }

    IEnumerator CountdownTimer()
    {
        while (countdownTime > 0)
        {
            Debug.Log("Tiempo restante: " + countdownTime);
            yield return new WaitForSeconds(1);
            countdownTime--;
            if (!start){
                countdownTime = 5;
                break;
            }
        }



    }
}
