using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTextUI : MonoBehaviour
{
    [SerializeField] private GameObject _win;
    [SerializeField] private GameObject _game;
    void Start()
    {
        _win.SetActive(false);
        _game.SetActive(true);
    }
    private void OnEnable()
    {
        EventBus.onWin += showWinUI;    
    }
    private void OnDisable()
    {
        EventBus.onWin -= showWinUI;
    }
    private void showWinUI()
    {
        _win.SetActive(true);
        _game.SetActive(false);
    }
}
