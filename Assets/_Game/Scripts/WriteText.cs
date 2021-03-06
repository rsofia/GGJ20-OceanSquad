﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WriteText : MonoBehaviour
{
    private TMP_Text m_TextComponent;
    private bool hasTextChanged;

    public float startDelay = 1.0f;
    public float typeSpeed = 0.1f;
    public bool revealCharacters = true;
    public bool doOnce = true;

    private bool done = false;

    void Awake()
    {
        m_TextComponent = gameObject.GetComponent<TMP_Text>();
    }


    void Start()
    {
        done = false;
        if(revealCharacters)
            StartCoroutine(RevealCharacters(m_TextComponent));
        else
            StartCoroutine(RevealWords(m_TextComponent));
    }


    void OnEnable()
    {
        // Subscribe to event fired when text object has been regenerated.
        TMPro_EventManager.TEXT_CHANGED_EVENT.Add(ON_TEXT_CHANGED);
    }

    void OnDisable()
    {
        TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(ON_TEXT_CHANGED);
    }


    // Event received when the text object has changed.
    void ON_TEXT_CHANGED(Object obj)
    {
        if(!doOnce)
         hasTextChanged = true;
    }


    /// <summary>
    /// Method revealing the text one character at a time.
    /// </summary>
    /// <returns></returns>
    IEnumerator RevealCharacters(TMP_Text textComponent)
    {
        textComponent.ForceMeshUpdate();

        TMP_TextInfo textInfo = textComponent.textInfo;

        int totalVisibleCharacters = textInfo.characterCount; // Get # of Visible Character in text object
        int visibleCount = 0;

        while (true)
        {
            if (hasTextChanged)
            {
                totalVisibleCharacters = textInfo.characterCount; // Update visible character count.
                hasTextChanged = false;
            }

            if (visibleCount > totalVisibleCharacters)
            {
                if(doOnce)
                {
                    done = true;
                    break;
                }
                yield return new WaitForSeconds(1.0f);
                visibleCount = 0;
            }

            textComponent.maxVisibleCharacters = visibleCount; // How many characters should TextMeshPro display?

            if(visibleCount == 0)
                yield return new WaitForSeconds(startDelay);
            visibleCount += 1;

            yield return new WaitForSeconds(typeSpeed);
        }
    }


    /// <summary>
    /// Method revealing the text one word at a time.
    /// </summary>
    /// <returns></returns>
    IEnumerator RevealWords(TMP_Text textComponent)
    {
        textComponent.ForceMeshUpdate();

        int totalWordCount = textComponent.textInfo.wordCount;
        int totalVisibleCharacters = textComponent.textInfo.characterCount; // Get # of Visible Character in text object
        int counter = 0;
        int currentWord = 0;
        int visibleCount = 0;

        while (true)
        {
            currentWord = counter % (totalWordCount + 1);

            // Get last character index for the current word.
            if (currentWord == 0) // Display no words.
                visibleCount = 0;
            else if (currentWord < totalWordCount) // Display all other words with the exception of the last one.
                visibleCount = textComponent.textInfo.wordInfo[currentWord - 1].lastCharacterIndex + 1;
            else if (currentWord == totalWordCount) // Display last word and all remaining characters.
                visibleCount = totalVisibleCharacters;

            textComponent.maxVisibleCharacters = visibleCount; // How many characters should TextMeshPro display?

            // Once the last character has been revealed, wait 1.0 second and start over.
            if (visibleCount >= totalVisibleCharacters)
            {
                if(doOnce)
                {
                    done = true;
                    break;
                }
                else
                {
                    yield return new WaitForSeconds(1.0f);
                }
            }

            if(counter == 0)
                yield return new WaitForSeconds(startDelay);
            counter += 1;

            yield return new WaitForSeconds(typeSpeed);
        }
    }

}
