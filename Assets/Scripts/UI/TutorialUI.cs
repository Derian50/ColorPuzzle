using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] GameObject _tutorial;

    public void onClickButtonCloseTutorial()
    {
        _tutorial.SetActive(false);
    }
}
