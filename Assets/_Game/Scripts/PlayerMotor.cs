using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField] private float turnSpeed = 15f;
    [SerializeField] private float moveSpeed = 10f;

    private PlayerInputs playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInputs>();
    }

    void Update()
    {
#if UNITY_ANDROID
        Vector3 pos = playerInput.Position;
        if (Vector3.Magnitude(transform.position - pos) > 0.1f)
            transform.position = GetPosition(pos, transform.position);
        else
            Debug.Log("STOPPED");
#elif UNITY_STANDALONE_WIN
        float rotation = playerInput.Rotation;
        float thrust = playerInput.Thrust;

        transform.Rotate(Vector3.up * rotation * Time.deltaTime * turnSpeed);
        transform.position += transform.forward * thrust * Time.deltaTime * moveSpeed;
#endif
    }

    public Vector3 GetPosition(Vector3 _position, Vector3 _initialPos)
    {
        Vector3 resultPos = _initialPos;

        resultPos = Vector3.Lerp(_initialPos, _position, Time.deltaTime);

        return resultPos;
    }
}
