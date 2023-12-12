using System;
using UnityEngine;
using UnityEngine.UI;

public class KeypadEnterButton : MonoBehaviour
{
    [SerializeField] Button enterButton;
    [SerializeField] KeypadController controller;

    void Start()
    {

        controller.CurrentCodeUpdated += HandleNewCodeReceived;
        controller.UnlockAttempt += OnUnlockAttempt;
        controller.RestoreFunctionality += Initialise;

        Initialise();
    }

    void Initialise()
    {
        enterButton.onClick.RemoveAllListeners();
        enterButton.onClick.AddListener(OnClickEnterButton);
        enterButton.interactable = false;
    }

    void OnDestroy()
    {
        controller.CurrentCodeUpdated -= HandleNewCodeReceived;
        controller.UnlockAttempt -= OnUnlockAttempt;
        controller.RestoreFunctionality -= Initialise;
    }

    private void OnClickEnterButton()
    {
        Debug.Log("Clicked enter button");
        controller.HandleEnterCodeButton();
    }

    private void HandleNewCodeReceived(string code)
    {
        // make button interactable only when we have all 4 digits in place
        enterButton.interactable = !code.Contains("-");
    }

    private void OnUnlockAttempt(bool won)
    {
        // remove functionality regardless of win
        enterButton.interactable = false;
    }
}
