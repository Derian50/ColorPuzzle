using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager GameManagerInstance;
    private void Awake()
    {
        if (GameManagerInstance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            GameManagerInstance = this;
            DontDestroyOnLoad(this);
        }
    }
    private void OnEnable()
    {
        EventBus.onClickButtonLevel += switchScene;
        EventBus.onLevelCompleted += levelCompleted;
    }
    private void OnDisable()
    {
        EventBus.onClickButtonLevel -= switchScene;
        EventBus.onLevelCompleted -= levelCompleted;
    }
    private void switchScene(int indexScene)
    {
        SceneManager.LoadScene(indexScene);
    }
    private void levelCompleted(int indexLevel)
    {
        SaveManager.CurrentState.completedLevels[indexLevel - 1] = true;
        SaveManager.SaveState();
    }
}
