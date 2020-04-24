using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  static class GameConsts
{
    public const string PlayerTag = "Player";
    public const string RockTag = "Rock";

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
                result = PlayerTag;
                break;
            case GameTags.Rock:
                result = RockTag;
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

    public static Color SetAlpha(this Color color, float alphaValue)
    {
        return new Color(color.r, color.g, color.b, alphaValue);
    }
}
