using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenuUI : MonoBehaviour
{
    public void onButtonClick()
    {
        YaSDK.ShowFullscreenAdv();
        SceneManager.LoadScene(0);
    }
}
