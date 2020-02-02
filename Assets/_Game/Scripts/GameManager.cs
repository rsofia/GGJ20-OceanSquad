using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Pixelation.Example.Scripts;

public class GameManager : MonoBehaviour
{
    public GameObject [] Torches;

    public GameObject FinalLight;

    public GameObject Camera;

    public GameObject Player;

    public GameObject gameOverPanel;

    public bool gameOver = false;

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
        /*for (int i = 0; i < Torches.Length; i++)
            Torches[i].transform.parent.gameObject.SetActive(false);*/
        gameOver = true;
        gameOverPanel.SetActive(true);
        Player.GetComponentInChildren<Animator>().SetTrigger("Win");
        Camera.GetComponent<Assets.Pixelation.Scripts.Pixelation>().enabled = false;
    }

    public void TurnLightUp()
    {
        StartCoroutine(GradualIncrease());
    }

    IEnumerator GradualIncrease()
    {
        float initialIntensity = FinalLight.GetComponent<Light>().intensity;

        while(FinalLight.GetComponent<Light>().intensity < (initialIntensity+ 0.15f))
        {
            FinalLight.GetComponent<Light>().intensity += 0.15f * Time.deltaTime;
            yield return null;
        }
    }

    public void GoToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
