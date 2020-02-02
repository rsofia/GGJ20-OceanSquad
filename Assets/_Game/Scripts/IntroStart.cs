using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class IntroStart : MonoBehaviour
{
    public AnimUI[] txtOrder;
    public AnimationCurve curve;

    int current = 0;

    public float timeToLoadScene = 1.0f;

    public void Start()
    {
        current = 0;
        for(int i = 0; i < txtOrder.Length; i++)
        {
            if (i == current)
                txtOrder[i].txt.gameObject.SetActive(true);
            else
                txtOrder[i].txt.gameObject.SetActive(false);
        }
    }

    public void Next()
    {
        StartCoroutine(LerpSize(txtOrder[current], curve));
        current++;
        if (current >= txtOrder.Length)
            StartCoroutine(LoadScene(1));
        else
            txtOrder[current].txt.gameObject.SetActive(true);
    }

    [System.Serializable]
    public class AnimUI
    {
        public TextMeshProUGUI txt;
        //public AnimationCurve curve;
        [HideInInspector]
        public bool isOnLerp;
    }

    IEnumerator LoadScene(int index)
    {
        yield return new WaitForSeconds(timeToLoadScene);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(index);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    /// <summary>
    /// Lerps the color of a Text with a given Animation Curve.
    /// </summary>
    /// <param name="curve"></param>
    /// <param name="rect"></param>
    /// <param name="deltaTime"></param>
    /// <returns></returns>
    IEnumerator LerpSize(AnimUI anim, AnimationCurve curve, float deltaTime = 0.01f)
    {
        anim.txt.color = anim.txt.color.SetAlpha(1);
        Color startColor = anim.txt.color;
        anim.isOnLerp = true;
        float t = 0;
        do
        {
            float size = curve.Evaluate(t);
            anim.txt.color = startColor.SetAlpha(size);
            yield return new WaitForSeconds(deltaTime);
            t += deltaTime; //add the time

        } while (t < curve[curve.length - 1].time); //run until the time of the last frame

        anim.isOnLerp = false;
    }
}
