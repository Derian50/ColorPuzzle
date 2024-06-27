using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsButtonManager : MonoBehaviour
{
    private GameObject[] _buttonsArr;
    void Start()
    {
        SortButtonsByCoordinates();
        SetButtonsStates();
        SetButtonsColor();
    }
    private void SetButtonsColor()
    {
        
        for(int i = 0; i < _buttonsArr.Length; i++)
        {
            if (i < 80) _buttonsArr[i].GetComponent<ButtonUI>().SetColorButton(Color.Lerp(Color.green, Color.cyan, (float)(i-60) / 20f));
            if (i < 60) _buttonsArr[i].GetComponent<ButtonUI>().SetColorButton(Color.Lerp(Color.magenta, Color.green, (float)(i-40) / 20f));
            if (i < 40) _buttonsArr[i].GetComponent<ButtonUI>().SetColorButton(Color.Lerp(Color.yellow, Color.magenta, (float)(i-20) / 20f));
            if (i < 20) _buttonsArr[i].GetComponent<ButtonUI>().SetColorButton(Color.Lerp(Color.cyan, Color.yellow, (float)i / 20f));

        }
    }
    private void SetButtonsStates()
    {
        int countOfCompletedLevels = 0;
        foreach(bool levelIsComplete in SaveManager.CurrentState.completedLevels)
        {
            if(levelIsComplete) countOfCompletedLevels++;
        }
        for (int i = 0; i < _buttonsArr.Length; i++)
        {
            _buttonsArr[i].GetComponent<ButtonUI>().setTextLevel(i + 1);
            _buttonsArr[i].GetComponent<ButtonUI>().SetState(ButtonState.Lock);
            if (i/5 < (countOfCompletedLevels + 10)/5) _buttonsArr[i].GetComponent<ButtonUI>().SetState(ButtonState.Open);
            if (SaveManager.CurrentState.completedLevels[i])
            {
                _buttonsArr[i].GetComponent<ButtonUI>().SetState(ButtonState.Win);
            }
            
        }
    }
    private void SortButtonsByCoordinates()
    {
        _buttonsArr = GameObject.FindGameObjectsWithTag("LevelButton");
        System.Array.Sort(_buttonsArr, (b, a) =>
        {
            Vector3 posA = a.transform.position;
            Vector3 posB = b.transform.position;

            if (posA.y != posB.y)
            {
                return posA.y.CompareTo(posB.y);
            }
            else
            {
                return posB.x.CompareTo(posA.x);
            }
        });
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
