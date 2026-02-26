using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MultiSceneLoader : MonoBehaviour
{
    [Header("Список дополнительных сцен для загрузки")]
    public List<string> scenesToLoad;

    void Start()
    {
        LoadRooms();
    }

    void LoadRooms()
    {
        foreach (string sceneName in scenesToLoad)
        {
            if (!SceneManager.GetSceneByName(sceneName).isLoaded)
            {
                SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            }
        }
    }
}