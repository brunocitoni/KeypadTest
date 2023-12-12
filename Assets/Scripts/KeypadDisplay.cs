using TMPro;
using UnityEngine;

public class KeypadDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text displayText;
    [SerializeField] KeypadController controller;

    // Start is called before the first frame update
    void OnEnable()
    {
        controller.CurrentCodeUpdated += UpdateUI;
        controller.UnlockAttempt += OnUnlockAttempt;
    }

    private void OnDestroy()
    {
        controller.CurrentCodeUpdated -= UpdateUI;
        controller.UnlockAttempt -= OnUnlockAttempt;
    }

    void UpdateUI(string value)
    {
        displayText.text = value;
    }

    private void OnUnlockAttempt(bool won)
    {
        // if we won display unlocked message
        if (won) displayText.text = "UNLOCKED";
    }
}
