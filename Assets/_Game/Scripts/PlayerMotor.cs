using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField] private float turnSpeed = 15f;
    [SerializeField] private float moveSpeed = 10f;

    private PlayerInputs playerInput;
    private PlayerStats playerStats;
    private Rigidbody playerRB;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInputs>();
        playerStats = GetComponent<PlayerStats>();
        playerRB = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
#if UNITY_ANDROID || UNITY_EDITOR
        Vector3 pos = playerInput.Position;
        //RB Not working for some reason
        //playerRB.MovePosition(transform.position + pos * Time.fixedDeltaTime);
        
        if(Vector3.Distance(transform.position, pos) > 0.1f)
        {
            transform.position = GetPosition(pos, transform.position);
            transform.LookAt(pos);

            Debug.Log("I AM WORKING");
        }


#elif UNITY_STANDALONE_WIN
        float rotation = playerInput.Rotation;
        float thrust = playerInput.Thrust;

        transform.Rotate(Vector3.up * rotation * Time.deltaTime * turnSpeed);
        transform.position += transform.forward * thrust * Time.deltaTime * moveSpeed;
#endif
    }

    public Vector3 GetPosition(Vector3 _position, Vector3 _initialPos)
    {
        Vector3 resultPos;

        resultPos = Vector3.Lerp(_initialPos, _position, Time.deltaTime);

        return resultPos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.tag);

        if(collision.transform.CompareTag("Torch"))
        {
            Debug.Log("I GOT A TORCH!");
            playerStats.LightCounter++;
            collision.transform.parent.GetComponent<TorchProperties>().TorchLit.SetActive(true);
            collision.transform.parent.GetComponent<TorchProperties>().TorchUnlit.SetActive(false);

            if (playerStats.LightCounter >= GameManager.Instance.Torches.Length)
                GameManager.Instance.WinLevel();
            
        }
    }
}
