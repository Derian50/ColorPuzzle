using UnityEngine;

using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;


[System.Serializable] 
public class SavedData
{
    public bool[] completedLevels;
}

public class SaveManager: MonoBehaviour
{

    //public SavedData SavedData;
    //public static SaveManager Instance;
    public static SaveManager saveManagerInstance;
    private void Awake()
    {
        LoadState(() => { });
        if (saveManagerInstance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            saveManagerInstance = this;
            DontDestroyOnLoad(this);

            /*CurrentState = new SavedData();
            CurrentState.completedLevels = new bool[100];
            CurrentState.completedLevels[4] = true;
            CurrentState.completedLevels[7] = true;
            CurrentState.completedLevels[9] = true;
            CurrentState.completedLevels[11] = true;
            CurrentState.completedLevels[12] = true;
            CurrentState.completedLevels[13] = true;
            CurrentState.completedLevels[14] = true;
            CurrentState.completedLevels[10] = true;*/
        }
        //Instance = this;
        Debug.Log("--------LOAD STATE DATA----------------");
        // Debug.Log(SaveManager.Instance.SavedData.CurrentLevelNumber);
        // Debug.Log(SaveManager.Instance.SavedData.Coins);
        // Debug.Log(SaveManager.Instance.SavedData.currentHeadIndex);
        // Debug.Log(SaveManager.Instance.SavedData.OpenHeadSkin[0]);
        // Debug.Log(SaveManager.Instance.SavedData.OpenHeadSkin[7]);
        Debug.Log("--------STATE DATA LOADED----------------");
    }

    private void Start()
    {
        //Analytics

    }

    public static SavedData CurrentState { get; private set; }
    private void LoadState(Action onLoadCompleted)
    {
        if(Application.isEditor)
        {
            return;
        }
        YaSDK.GetData<SavedData>(data =>
        {
            if (data.completedLevels.Length < 70)
            {
                CurrentState = new SavedData();
                CurrentState.completedLevels = new bool[80];
            }
            else
            {
                CurrentState = data;
            }
            onLoadCompleted();
        });

    }

    public static void SaveState()
    {
        if (Application.isEditor) return;
            Debug.Log("SaveManager: request to save state has been sent");


        // string jsonString = JsonUtility.ToJson(PlayerInfo);
        YaSDK.SetData(CurrentState);
        // YaSDK.SetToLeaderboard(PlayerInfo.score);
    }
}

