using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MovimientoCamaraMinas : MonoBehaviour
{
    public CinemachineVirtualCamera theCamera;
    public float speed;
    private bool mobible = false;
    void Start()
    {

    }

    void Update()
    {
        ControlDeMovimiento();
    }

    private void ControlDeMovimiento()
    {
        mobible = theCamera.Priority > 0 ? true : false;
        if(mobible)
        {
            float x = Input.GetAxisRaw("Mouse X");
            float z = Input.GetAxisRaw("Mouse Y");
            if(Input.GetMouseButton(1))
            {
                Vector3 pos = new Vector3(x*-1,0f,z*-1);
                pos = pos * speed * Time.deltaTime;
                transform.position += pos;
            }
        }
    }
}
