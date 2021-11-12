using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class UISound : MonoBehaviour
{
    public AudioClip confirmacion,
    inactivo,
    error,
    maquinaInstalada;
    public List<AudioClip> sonidoPartes;
    public AudioClip crafteo;

    public AudioSource reproductor;

    public AudioMixerGroup sonidosFuertes, sonidosNormales;

    public void PlayConfirmation()
    {
        PlaySound(EventoSonoroUI.Confirmacion);
    }

    public void PlayCerrar()
    {
        PlaySound(EventoSonoroUI.CerrarConsola);
    }

    public void PlayInactive()
    {
        PlaySound(EventoSonoroUI.Inactivo);
    }

    public void PlayError()
    {
        PlaySound(EventoSonoroUI.Error);
    }

    public void PlaySound(EventoSonoroUI evento)
    {
        switch (evento)
        {
            case EventoSonoroUI.Confirmacion:
                //Le asigno un Audio Mixer
                reproductor.outputAudioMixerGroup = sonidosNormales;
                reproductor.clip = confirmacion;
                reproductor.pitch = 0.9f;
                break;
            case EventoSonoroUI.Inactivo:
                //Le asigno un Audio Mixer
                reproductor.outputAudioMixerGroup = sonidosNormales;
                reproductor.clip = inactivo;
                reproductor.pitch = 1;
                break;
            case EventoSonoroUI.Error:
                //Le asigno un Audio Mixer
                reproductor.outputAudioMixerGroup = sonidosNormales;
                reproductor.clip = error;
                reproductor.pitch = 1;
                break;
            case EventoSonoroUI.MaquinaInstalada:
                //Le asigno un Audio Mixer
                reproductor.outputAudioMixerGroup = sonidosFuertes;
                reproductor.clip = maquinaInstalada;
                reproductor.pitch = 1;
                break;
            case EventoSonoroUI.ParteInstalada:
                //Le asigno un Audio Mixer
                reproductor.outputAudioMixerGroup = sonidosFuertes;
                // sonido de instalacion de parte
                int num = Random.Range(0, sonidoPartes.Count);
                reproductor.clip = sonidoPartes[num];
                reproductor.pitch = 1;
                break;
            case EventoSonoroUI.AbrirConsola:
                //Le asigno un Audio Mixer
                reproductor.outputAudioMixerGroup = sonidosNormales;
                reproductor.clip = confirmacion;
                reproductor.pitch = 1.05f;
                break;
            case EventoSonoroUI.CerrarConsola:
                //Le asigno un Audio Mixer
                reproductor.outputAudioMixerGroup = sonidosNormales;
                reproductor.clip = confirmacion;
                reproductor.pitch = 0.85f;
                break;
            case EventoSonoroUI.Crafteo:
                //Le asigno un Audio Mixer
                reproductor.outputAudioMixerGroup = sonidosNormales;
                reproductor.clip = crafteo;
                reproductor.pitch = 1f;
            break;
        }
        reproductor.Play();
    }
}

public enum EventoSonoroUI
{
    Confirmacion,
    Inactivo,
    Error,
    MaquinaInstalada,
    ParteInstalada,
    AbrirConsola,
    CerrarConsola,
    Crafteo
}
