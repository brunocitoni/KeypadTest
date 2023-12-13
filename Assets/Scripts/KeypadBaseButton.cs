using UnityEngine;
using UnityEngine.UI;

public abstract class KeypadBaseButton : MonoBehaviour
{
    protected KeypadController controller;
    [SerializeField] protected Button thisButton;
    protected abstract void OnButtonClick();

    protected virtual void Start()
    {
        controller = GetComponentInParent<KeypadController>();
        controller.UnlockAttempt += OnUnlockAttempt;
        controller.CurrentCodeUpdated += HandleNewCodeReceived;
        controller.RestoreFunctionality += Initialise;

        Initialise();
    }

    protected virtual void OnDestroy()
    {
        controller.UnlockAttempt -= OnUnlockAttempt;
        controller.RestoreFunctionality -= Initialise;
        controller.CurrentCodeUpdated -= HandleNewCodeReceived;
    }

    protected virtual void Initialise()
    {
        Button button = thisButton;

        // assign OnButtonClick callback
        button.interactable = true;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnButtonClick);
    }

    protected virtual void OnUnlockAttempt(bool won)
    {
        // remove functionality regardless of win
        thisButton.interactable = false;
    }

    protected virtual void HandleNewCodeReceived(string code)
    {
        // make button interactable only when we have all 4 digits in place
        thisButton.interactable = code.Contains("-");
    }
}