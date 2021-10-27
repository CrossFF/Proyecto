using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISound : MonoBehaviour
{
    public AudioClip confirmacion, inactivo, error, maquinaInstalada, parteInstalada;
    public AudioSource reproductor;

    public void PlayConfirmation()
    {
        PlaySound(EventoSonoroUI.Confirmacion);
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
                break;
            case EventoSonoroUI.Inactivo:
                reproductor.clip = inactivo;
                break;
            case EventoSonoroUI.Error:
                reproductor.clip = error;
                break;
            case EventoSonoroUI.MaquinaInstalada:
                reproductor.clip = maquinaInstalada;
                break;
            case EventoSonoroUI.ParteInstalada:
                reproductor.clip = parteInstalada;
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
    ParteInstalada
}
