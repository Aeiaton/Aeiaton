using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static string selectedNode = null;
    public static Vector3 mapPosition = new Vector3(0f, 0f, -50f);
    public static List<string> revealedNodes = new List<string>();
    public static List<string> completedNodes = new List<string>();
}
