using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CameraSelection : MonoBehaviour
{
    public GameObject cameraPrefab;
    public GameObject Player;
    public PlayerInputManager PlayerManager;
    public List<GameObject> Players = new List<GameObject>();
    public List<Camera> playerCameras =  new List<Camera>();
    public List<GameObject> Spawn; 
    public TextMeshProUGUI startText;
    public int Devices = 0;

    int countdownTime = 5;
     public List<Color> colors = new List<Color> { 
        new Color(40f/255f, 150f/255f, 241f/255f), 
        new Color(233f/255f, 43f/255f, 54f/255f), 
        new Color(114f/255f, 237f/255f, 40f/255f), 
        new Color(245f/255f, 242f/255f, 72f/255f)};
    bool start = false;

    void Update()
    {
        Devices = CountDevices();
        if (PlayerManager.playerCount < Devices  ){
            addPlayer();
        }

        if (PlayerManager.playerCount > Devices){
            removePlayer();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene("MenuPista");
        }

        startGame();
    }

    void addPlayer(){
        GameObject newPlayer = Instantiate(Player, transform.position, transform.rotation);
        PlayerInput newPlayerInput = newPlayer.GetComponent<PlayerInput>();
        newPlayerInput.SwitchCurrentControlScheme("Gamepad", Gamepad.all[PlayerManager.playerCount-1]);

        startText.enabled = false;
    }

    void removePlayer(){
        Destroy(Players[PlayerManager.playerCount - 1]);
        Players.RemoveAt(Players.Count - 1);
        playerCameras.RemoveAt(playerCameras.Count - 1);
        AdjustCameraView();
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
            cameraGameObject.clearFlags = CameraClearFlags.SolidColor;
            cameraGameObject.backgroundColor = colors[i];
            Debug.Log("" + cameraGameObject.backgroundColor);
        }
    }

    
    private void AdjustCameraView()
    {
        switch (playerCameras.Count)
        {
            case 1:
                playerCameras[0].rect = new Rect(0f, 0f, 1f, 1f);
                break;
            case 2:
                playerCameras[0].rect = new Rect(0f, 0f, 0.5f, 1);
                playerCameras[1].rect = new Rect(0.5f, 0, 0.5f, 1);
                break;
                
            case 3:
                playerCameras[0].rect = new Rect(0f, 0f, 0.33f, 1f); 
                playerCameras[1].rect = new Rect(0.33f, 0f, 0.33f, 1f); 
                playerCameras[2].rect = new Rect(0.66f, 0f, 0.33f, 1f); 
                break;
            case 4:
                playerCameras[0].rect = new Rect(0f, 0f, 0.25f, 1f);
                playerCameras[1].rect = new Rect(0.25f, 0f, 0.25f, 1f); 
                playerCameras[2].rect = new Rect(0.5f, 0f, 0.25f, 1f);
                playerCameras[3].rect = new Rect(0.75f, 0f, 0.25f, 1f); 

                playerCameras[0].fieldOfView =25;
                playerCameras[1].fieldOfView =25;
                playerCameras[2].fieldOfView =25;
                playerCameras[3].fieldOfView =25;
                break;
        }
    }

    int CountDevices(){
        InputDevice[] devices = InputSystem.devices.ToArray();

        int CurrentDevice = 0;

        foreach (InputDevice device in devices)
        {
            if (device is Gamepad)
            {
                CurrentDevice++;
            }
        }

        return CurrentDevice;
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

            if (countdownTime < 1){
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
            if (!start)
            {
                break; 
            }
            countdownTime--;
        }
        
        if (!start)
        {
            countdownTime = 5; 
        }
    }
}
