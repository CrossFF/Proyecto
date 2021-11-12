using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pasos : MonoBehaviour
{
    public List<AudioClip> pasos;
    public AudioSource reproductor;

    public void Paso()
    {
        int num = Random.Range(0, pasos.Count);
        reproductor.clip = pasos[num];
        reproductor.Play();
    }
}
