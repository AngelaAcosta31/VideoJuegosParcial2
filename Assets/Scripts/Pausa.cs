using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro.EditorUtilities;

public class Pausa : MonoBehaviour
{

    public Image vuelta1;
    public Image vuelta2;
    public Image vuelta3;

    public Image posicion1;
    public Image posicion2;
    public Image posicion3;
    public AudioSource clip;
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;

   // [SerializeField] private GameObject botonAudio;
    
    private bool juegoPausado = false;
    
    private void Start() {
        
        vuelta1.gameObject.SetActive(true);
        vuelta2.gameObject.SetActive(false);
        vuelta3.gameObject.SetActive(false);
        // Desactivar todas las imágenes al inicio
        posicion1.gameObject.SetActive(true);
        posicion2.gameObject.SetActive(false);
        posicion3.gameObject.SetActive(false);

        // Suscribirse al evento de cambio de imagen de las vueltas
        //Juego.OnCambioDeImagenVueltas += MostrarImagenVueltas;
        //Juego.OnCambioDeImagenPosicion += MostrarImagenPosicion;


    }

    void OnDestroy()
    {
        // Desuscribirse del evento al destruir el objeto
        Juego.OnCambioDeImagenVueltas -= MostrarImagenVueltas;
        Juego.OnCambioDeImagenPosicion -= MostrarImagenPosicion;
    }
    void MostrarImagenVueltas(int numeroImagen)
    {
        // Ocultar todas las imágenes primero
        vuelta1.gameObject.SetActive(false);
        vuelta2.gameObject.SetActive(false);
        vuelta3.gameObject.SetActive(false);

        // Mostrar la imagen correspondiente según el número recibido
        switch (numeroImagen)
        {
            case 1:
                vuelta1.gameObject.SetActive(true);
                vuelta2.gameObject.SetActive(false);
                vuelta3.gameObject.SetActive(false);
                break;
            case 2:
                vuelta2.gameObject.SetActive(true);
                vuelta1.gameObject.SetActive(false);
                vuelta3.gameObject.SetActive(false);
                break;
            case 3:
                vuelta3.gameObject.SetActive(true);
                vuelta2.gameObject.SetActive(false);
                vuelta1.gameObject.SetActive(false);
                break;
            default:
                Debug.LogWarning("Número de imagen no válido: " + numeroImagen);
                break;
        }
    }

    void MostrarImagenPosicion(int numeroImagen)
    {
        // Ocultar todas las imágenes primero
        posicion1.gameObject.SetActive(false);
        posicion2.gameObject.SetActive(false);
        posicion3.gameObject.SetActive(false);

        // Mostrar la imagen correspondiente según el número recibido
        switch (numeroImagen)
        {
            case 1:
                posicion1.gameObject.SetActive(true);
                posicion2.gameObject.SetActive(false);
                posicion3.gameObject.SetActive(false);
                break;
            case 2:
                posicion2.gameObject.SetActive(true);
                posicion1.gameObject.SetActive(false);
                posicion3.gameObject.SetActive(false);
                break;
            case 3:
                posicion3.gameObject.SetActive(true);
                posicion2.gameObject.SetActive(false);
                posicion1.gameObject.SetActive(false);
                break;
            default:
                Debug.LogWarning("Número de imagen no válido: " + numeroImagen);
                break;
        }
    }
    private void Update(){
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
        clip.Pause();
        Time.timeScale = 0f;
        vuelta1.gameObject.SetActive(false);
        vuelta2.gameObject.SetActive(false);
        vuelta3.gameObject.SetActive(false);
        posicion1.gameObject.SetActive(false);
        posicion2.gameObject.SetActive(false);
        posicion3.gameObject.SetActive(false);
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
        //DontDestroyOnLoad(gameObject);
        
    }

    public void Reanudar(){
        juegoPausado = false;
        clip.Play();
        Time.timeScale = 1f;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
        //Se debe agregar la condicion de mostrar la posiciony la vuelta
        //DontDestroyOnLoad(gameObject);
    }

    public void Reiniciar(){
        juegoPausado = false;
        clip.Play();
        Time.timeScale = 1f;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
        //Se debe agregar la condicion de mostrar la posiciony la vuelta
        //DontDestroyOnLoad(gameObject);
        //SceneManager.UnloadSceneAsync("Juego");
        SceneManager.LoadScene("Juego");
    }


    public void Salir(){
        juegoPausado = false;
        //clip.Pause();
        Time.timeScale = 1f;
        botonPausa.SetActive(false);
        menuPausa.SetActive(false);
        //botonAudio.SetActive(false);

        // Descarga la escena del juego actual
        //SceneManager.UnloadSceneAsync("Juego");
        SceneManager.LoadScene("MenuPista");
    }

    



    

}
