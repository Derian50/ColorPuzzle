using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlashText : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        StartCoroutine(TextFlash());
    }

    private IEnumerator TextFlash()
    {
        while (true)
        {
            _text.enabled = !_text.enabled;
            yield return new WaitForSeconds(.5f);
        }
    }
}
