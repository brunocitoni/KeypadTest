using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeypadButton : MonoBehaviour
{
    public Action<int> ButtonPressed;

    [SerializeField] Button keyButton;
    [SerializeField] TMP_Text keyText;
    KeypadController controller;

    private int _value = 0;

    public void SetValue(int value)
    {
        _value = value;
        keyText.text = _value.ToString();
    }

    private void Start()
    {
        controller = GetComponentInParent<KeypadController>();
        controller.UnlockAttempt += OnUnlockAttempt;
        controller.RestoreFunctionality += Initialise;

        Initialise();
    }
    private void OnDestroy()
    {
        controller.UnlockAttempt -= OnUnlockAttempt;
        controller.RestoreFunctionality -= Initialise;
    }

    void Initialise()
    {
        // assign OnButtonClick callback
        keyButton.interactable = true;
        keyButton.onClick.RemoveAllListeners();
        keyButton.onClick.AddListener(OnButtonClick);

        keyText.text = _value.ToString();
    }

    public void OnButtonClick()
    {
        controller.HandleKeyButtonPress(_value);
    }

    private void OnUnlockAttempt(bool won)
    {
        // remove functionality regardless of win
        keyButton.interactable = false;
    }
}
