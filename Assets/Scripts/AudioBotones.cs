using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioBotones : MonoBehaviour
{

    public AudioSource clip;
    public AudioClip hoverFx;
    public AudioClip clickFx;
    public void HoverSound(){
        clip.PlayOneShot(hoverFx);
    }

    public void ClickSound(){
        clip.PlayOneShot(clickFx);
    }
}
