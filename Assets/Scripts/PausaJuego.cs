using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PausaJuego : MonoBehaviour
{
    //public AudioSource clip;
    public void Reiniciar(){
        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("MenuPista");
    }


    public void Salir(){
        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("Seleccion");
    }
}