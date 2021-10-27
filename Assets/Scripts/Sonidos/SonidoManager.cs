using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoManager : MonoBehaviour
{
    [SerializeField]private List<AudioSource> _audioGUI;
    [SerializeField]private List<AudioSource> _audioAmbiente;
    [SerializeField]private List<AudioSource> _audioMusica;
    [SerializeField] private UISound _uiSound;

    public void PlayUISound(EventoSonoroUI evento)
    {
        _uiSound.PlaySound(evento);
    }
}
