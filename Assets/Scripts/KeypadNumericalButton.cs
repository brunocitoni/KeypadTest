using System;
using TMPro;
using UnityEngine;

public class KeypadNumericalButton : KeypadBaseButton
{
    public Action<int> ButtonPressed;

    [SerializeField] TMP_Text keyText;

    private int _value = 0;

    public void SetValue(int value)
    {
        _value = value;
        keyText.text = _value.ToString();
    }

    protected override void Initialise()
    {
        base.Initialise();

        keyText.text = _value.ToString();
    }

    protected override void OnButtonClick()
    {
        controller.HandleKeyButtonPress(_value);
    }
}
