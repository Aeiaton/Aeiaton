using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    void Start()
    {
        TextAsset text = (TextAsset) Resources.Load("levels");
        string json = text.text;
        LevelData levelData = JsonUtility.FromJson<LevelData>(json);
        foreach (Level level in levelData.levels) {
            GameData.levels.Add(level.name, level);
        }
        SceneManager.LoadScene("map");
    }
}
