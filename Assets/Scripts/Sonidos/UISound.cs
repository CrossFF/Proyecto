using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISound : MonoBehaviour
{
    public AudioClip confirmacion,
    inactivo,
    error,
    maquinaInstalada,
    parteInstalada;

    public AudioSource reproductor;

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
                reproductor.clip = confirmacion;
                reproductor.pitch = 0.9f;
                break;
            case EventoSonoroUI.Inactivo:
                reproductor.clip = inactivo;
                reproductor.pitch = 1;
                break;
            case EventoSonoroUI.Error:
                reproductor.clip = error;
                reproductor.pitch = 1;
                break;
            case EventoSonoroUI.MaquinaInstalada:
                reproductor.clip = maquinaInstalada;
                reproductor.pitch = 1;
                break;
            case EventoSonoroUI.ParteInstalada:
                reproductor.clip = parteInstalada;
                reproductor.pitch = 1;
                break;
            case EventoSonoroUI.AbrirConsola:
                reproductor.clip = confirmacion;
                reproductor.pitch = 1.05f;
                break;
            case EventoSonoroUI.CerrarConsola:
                reproductor.clip = confirmacion;
                reproductor.pitch = 0.85f;
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
    CerrarConsola
}
