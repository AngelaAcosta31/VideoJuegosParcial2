using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionPlayer : MonoBehaviour
{
    private CameraSelection cameraMaganer;
    void Start()
    {
        cameraMaganer = GameObject.FindGameObjectWithTag("GameController").GetComponent<CameraSelection>();
        cameraMaganer.Players.Add(this.gameObject);
        cameraMaganer.SetupCameras();
    }
}
