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

    // Update is called once per frame
    void Update()
    {
#if UNITY_ANDROID || UNITY_EDITOR
        /*Debug.Log(Input.touches.Length);
        if (Input.touches.Length > 0)
        {
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            Debug.Log("POS: " + touchPos);
            Position = new Vector3(touchPos.x, transform.position.y, touchPos.z);
        }*/

        if(Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            LayerMask layerMask = LayerMask.GetMask("Player");

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Player"))
                    return;
                Debug.Log("I AM SENDING POSITION");
                Debug.Log("I HIT: " + hit.transform.name);
                Position = new Vector3(hit.point.x, Position.y, hit.point.z);
            }
        }

#elif UNITY_STANDALONE_WIN
        Rotation = Input.GetAxis("Horizontal");
        Thrust = Input.GetAxis("Vertical");
#endif
    }
}
