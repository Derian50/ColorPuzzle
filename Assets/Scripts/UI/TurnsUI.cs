using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnsUI : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private int turns = 0;
    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _text.text = turns.ToString();
    }
    private void OnEnable()
    {
        EventBus.onSwapTiles += newTurn;
    }
    private void OnDisable()
    {
        EventBus.onSwapTiles -= newTurn;
    }
    void newTurn()
    {
        turns++;
        _text.text = turns.ToString();
    }
}
