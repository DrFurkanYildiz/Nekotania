using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreditsSlip : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private float _startTextSize;
    private bool isSizeUpdate;
    void Start()
    {
        _text=GetComponentInChildren<TextMeshProUGUI>();
        _startTextSize = _text.fontSize;
    }
    private void Update()
    {
        if (isSizeUpdate)
            TextSizeOn();
        else
            TextSizeOff();
    }
    public void IsSizeActive(bool sizeUpdate)
    {
        isSizeUpdate = sizeUpdate;
    }
    private void TextSizeOn()
    {
        _text.gameObject.SetActive(true);
        _text.fontSize = Mathf.Lerp(_text.fontSize, _text.fontSize / 1.5f, Mathf.SmoothStep(0, 1, 1.8f * Time.deltaTime));
    }
    private void TextSizeOff()
    {
        _text.gameObject.SetActive(false);
        _text.fontSize = _startTextSize;
    }
}
