using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine;

public class UIMenu : MonoBehaviour
{
    public float timeToQuit = 2.0f;

    [Header("Start")]
    public GameObject startPanel;

    [Header("Options")]
    public GameObject optionsPanel;

    [Header("Thank you")]
    public Doozy.Engine.UI.UIView thankyouView;

    public void OnStart()
    {
        startPanel.SetActive(true);
    }

    public void OnClickOptions()
    {
        optionsPanel.SetActive(true);
    }

    public void OnHideOptions()
    {
        optionsPanel.SetActive(false);
    }

    public void OnExit()
    {
        thankyouView.Show();
        StartCoroutine(Exit());

    }

    IEnumerator Exit()
    {
        yield return new WaitForSeconds(timeToQuit);
        Application.Quit();
    }
}
