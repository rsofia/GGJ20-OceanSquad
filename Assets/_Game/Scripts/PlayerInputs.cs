using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    public float Rotation { get; private set; }
    public float Thrust { get; private set; }
    public Vector3 Position { get; private set; }

    // Update is called once per frame
    void Update()
    {
#if UNITY_ANDROID
        Debug.Log(Input.touches.Length);
        if (Input.touches.Length > 0)
        {
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            Debug.Log("POS: " + touchPos);
            Position = new Vector3(touchPos.x, transform.position.y, touchPos.z);
        }
#elif UNITY_STANDALONE_WIN
        Rotation = Input.GetAxis("Horizontal");
        Thrust = Input.GetAxis("Vertical");
#endif
    }
}
