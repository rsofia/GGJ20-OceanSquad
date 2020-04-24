using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine;

public class UIMenu : MonoBehaviour
{
    public float TimeToQuit = 2.0f;

    [Header("Start")]
    public GameObject StartPanel;

    [Header("Options")]
    public GameObject OptionsPanel;

    [Header("Thank you")]
    public Doozy.Engine.UI.UIView ThankyouView;

    public void OnStart()
    {
        StartPanel.SetActive(true);
    }

    public void OnClickOptions()
    {
        OptionsPanel.SetActive(true);
    }

    public void OnHideOptions()
    {
        OptionsPanel.SetActive(false);
    }

    public void OnExit()
    {
        ThankyouView.Show();
        StartCoroutine(Exit());
    }

    IEnumerator Exit()
    {
        yield return new WaitForSeconds(TimeToQuit);
        GameConsts.LogColor("red", "Bye bye");
        Application.Quit();
    }
}
