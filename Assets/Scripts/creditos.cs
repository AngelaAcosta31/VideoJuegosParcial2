using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class creditos : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource clip;

    void Start()
    {
        
        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        Invoke("Atras",14);
        // Comienza la reproducción de la música cuando la escena inicia.
        
        if (audioSource != null)
        {
            audioSource.Play();
        }
        
    }
    void Update(){
        if(Input.GetKey(KeyCode.Escape)){
            SceneManager.LoadScene("MenuPista");
        }
    }

    public void Atras(){
        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("MenuPista");
    }

    IEnumerator FadeOutAudio()
    {
       // Espera hasta que el audio termine de reproducirse
        while (audioSource.isPlaying)
        {
            yield return null;
        }

        // Cambia a la escena "Menu"
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("MenuPista");
    }
    public void PlaySoundBtn(AudioClip audio){
        clip.PlayOneShot(audio);
    }
    
}
