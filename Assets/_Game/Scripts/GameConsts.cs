using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConsts
{
    public const string playerTag = "Player";
    public const string rockTag = "Rock";

    public enum GameTags
    {
        Player, 
        Rock
    }

    public static string GetTagWithEnum(GameTags t)
    {
        string result = "";

        switch (t)
        {
            case GameTags.Player:
                result = playerTag;
                break;
            case GameTags.Rock:
                result = rockTag;
                break;
            default:
                result = "";
                break;
        }

        return result;
    }

    public static void LogColor(string color, string text)
    {
        Debug.Log("<color="+color+">" + text + "</color>");
    }

    public static void LogBold(string text)
    {
        Debug.Log("<b>" + text + "</b>");
    }

    public static void LogBoldColor(string color, string text)
    {
        Debug.Log("<b><color=" + color + ">" + text + "</color></b>");
    }
}
