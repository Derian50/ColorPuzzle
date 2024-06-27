using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ButtonState
{
    Win,
    Lock,
    Open
}
public class ButtonUI : MonoBehaviour
{
    [SerializeField] private GameObject _winImage;
    [SerializeField] private GameObject _lockImage;
    [SerializeField] private GameObject _openImage;
    [SerializeField] private TextMeshProUGUI _text;

    private ButtonState _state;

    private void Start()
    {
        //SetState(ButtonState.Lock);
    }
    public void ClickButtonLevel()
    {
        if (_state == ButtonState.Win || _state == ButtonState.Open)
        {
            EventBus.onClickButtonLevel?.Invoke(int.Parse(_text.text));
        }
    }
    public void SetColorButton(Color color)
    {
        _winImage.transform.GetChild(0).gameObject.GetComponent<Image>().color = color;
    }
    public void setTextLevel(int level)
    {
        _text.text = level.ToString();
    }
    public void SetState(ButtonState state)
    {
        _state = state;
        _winImage.SetActive(state == ButtonState.Win);
        _lockImage.SetActive(state == ButtonState.Lock);
        _openImage.SetActive(state == ButtonState.Open);
        _text.enabled = (state == ButtonState.Win || state == ButtonState.Open);
    }
}
