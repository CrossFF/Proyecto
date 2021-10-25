using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoMovil : MonoBehaviour
{
    //puntos limites
    [SerializeField]private Transform _punto1, _punto2;
    [SerializeField]private Transform _target;// objetivo
    void Awake()
    {
        // desviculo los parents
        _punto1.parent = null;
        _punto2.parent = null;
    }
    void Update()
    {
        // el objeto sigue al objetivo en los limites establecidos
        float x = Mathf.Clamp(_target.position.x, _punto1.position.x, _punto2.position.x);
        float z = Mathf.Clamp(_target.position.z, _punto1.position.z, _punto2.position.z);
        transform.position = new Vector3(x,transform.position.y, z);
    }
}
