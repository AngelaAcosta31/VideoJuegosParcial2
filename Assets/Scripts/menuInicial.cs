using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class menuInicial : MonoBehaviour
{
    //public AudioSource clip;
    public void Jugar(){
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("Juego");

    }

    public void Instrucciones(){
        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("Instrucciones");
    }
    public void Creditos(){
        //SceneManager.UnloadSceneAsync("menuInicial");
        SceneManager.LoadScene("Creditos");
    }

    public void Salir(){
        Application.Quit();
    }

    /*
    public void PlaySoundBtn(AudioClip audio){
        clip.PlayOneShot(audio);
    }
    */
}
