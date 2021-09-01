using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private LineRenderer line;
    void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    void Update()
    {
        // si el segundo punto en la linea no a sido declarado sigue el mouse
        RaycastHit hit;
        Vector3 position = Vector3.zero;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            position = new Vector3(hit.point.x, 0, hit.point.z);
        }
        line.SetPosition(1, position);
    }
}
