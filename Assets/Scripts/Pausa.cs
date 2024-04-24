using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Pausa : MonoBehaviour
{
    //public AudioSource clip;
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;
   // [SerializeField] private GameObject botonAudio;
    
    private bool juegoPausado = false;
    

    private void update(){
        if(!juegoPausado && Input.GetKeyDown(KeyCode.Pause)){
            if(juegoPausado){
                Reanudar();
            }else{
                Pausar();
            }
        }
    }
    public void Pausar(){
        juegoPausado = true;
        Time.timeScale = 0f;
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
        DontDestroyOnLoad(gameObject);
        
    }

    public void Reanudar(){
        juegoPausado = false;
        Time.timeScale = 1f;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
        DontDestroyOnLoad(gameObject);

    }

    public void Reiniciar(){
        juegoPausado = false;
        Time.timeScale = 1f;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
        DontDestroyOnLoad(gameObject);
        SceneManager.UnloadSceneAsync("Juego");
        SceneManager.LoadScene("Juego");
    }


    public void Salir(){
        botonPausa.SetActive(false);
        menuPausa.SetActive(false);
        //botonAudio.SetActive(false);
        
        // Descarga la escena del juego actual
        SceneManager.UnloadSceneAsync("Nivel");
        SceneManager.LoadScene("menuInicial");
    }

/*
    public void PauseSoundFondo(){
        clip.mute = !clip.mute;
    }

    public void PlaySoundFondo(){
        clip.Play();
    }

    public void PlaySoundBtn(AudioClip audio){
        clip.PlayOneShot(audio);
    }
    */

}
