using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Pixelation.Example.Scripts;

public class GameManager : MonoBehaviour
{
    public GameObject [] Torches;

    public GameObject FinalLight;

    public GameObject Camera;

    private static GameManager _gameManager;
    public static GameManager Instance
    {
        get { return _gameManager; }
        set { _gameManager = value; }
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        Torches = GameObject.FindGameObjectsWithTag("Torch");
        print(Torches);
    }

    public void WinLevel()
    {
        for (int i = 0; i < Torches.Length; i++)
            Torches[i].SetActive(false);

        FinalLight.SetActive(true);

        Camera.GetComponent<Assets.Pixelation.Scripts.Pixelation>().BlockCount = 512;
    }
}
