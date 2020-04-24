using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    public float Rotation { get; private set; }
    public float Thrust { get; private set; }
    public Vector3 Position { get; private set; }

    private void Awake()
    {
        Position = transform.position;
    }

  
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            LayerMask layerMask = LayerMask.GetMask("Player");

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Player"))
                    return;
                Position = new Vector3(hit.point.x, Position.y, hit.point.z);
            }
        }
    }
}
