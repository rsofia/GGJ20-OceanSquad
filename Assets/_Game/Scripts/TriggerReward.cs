using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TriggerReward : MonoBehaviour
{
    [Header("Reward Settings")]
    private bool wasActivated = false;
    public float timeToActivate = 1.0f;
    private string tagToDetect = "";
    public GameConsts.GameTags GameTagToDetect;

    [Header("Reward")]
    public RewardType reward;
    public GameObject objectToActivate;

    //actions for the object to activate
    private string triggerAction = "Open";
    private string triggerActionClose = "Close";

    //actions for self
    private const string triggerPressed = "Pressed";
    private const string triggerReleased = "Release";

    public Color notActivated;
    public Color activated;

    private Animator anim;
    private Light myLight;

    public enum RewardType
    {
        _01_OpenDoor, 
        _02_ShowLight
    }

    private void Awake()
    {
        tagToDetect = GameConsts.GetTagWithEnum(GameTagToDetect);
        anim = GetComponent<Animator>();
        myLight = GetComponentInChildren<Light>();
        myLight.color = notActivated;
    }

    private IEnumerator OnTriggerStay(Collider other)
    {
        GameConsts.LogBoldColor("blue", ("Colliding with " + other.tag));
        if(other.CompareTag(tagToDetect) && !wasActivated)
        {
            anim.SetTrigger(triggerPressed);
            wasActivated = true;
            myLight.color = activated;
            yield return new WaitForSeconds(timeToActivate);
            OnRewardTriggred();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(wasActivated)
        {
            wasActivated = false;
            myLight.color = notActivated;
            anim.SetTrigger(triggerReleased);
            objectToActivate.GetComponent<Animator>().SetTrigger(triggerActionClose);
        }
    }

    private void OnRewardTriggred()
    {
        objectToActivate.GetComponent<Animator>().SetTrigger(triggerAction);
        switch (reward)
        {
            case RewardType._01_OpenDoor:
                break;
            case RewardType._02_ShowLight:
                break;
            default:
                break;
        }
    }
}
