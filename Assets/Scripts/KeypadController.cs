using System;
using UnityEngine;
using UnityEngine.UI;

public class KeypadController : MonoBehaviour
{
    [SerializeField] GridLayoutGroup keyGrid;
    [SerializeField] GameObject KeyButtonPrefab;
    [SerializeField] Timer timer;

    private string _currentCode = "----";
    [SerializeField] private int unlockCode = 4853;

    public Action<string> CurrentCodeUpdated;
    public Action<bool> UnlockAttempt;
    public Action RestoreFunctionality;

    void Start()
    {
        // instantiate buttons with ascending values (1-9) 
        for (int i = 1; i < 10; i++)
        {
            var keyButton = Instantiate(KeyButtonPrefab, keyGrid.transform);
            keyButton.GetComponent<KeypadNumericalButton>().SetValue(i);
        }
    }

    /// <summary>
    /// Handles the Numerical Key presses
    /// </summary>
    /// <param name="value">The numerical value of the button</param>
    public void HandleKeyButtonPress(int value)
    {
        // validate input
        if (value < 0 || value > 10) return;
        // check if we are already at max digits
        if (!_currentCode.Contains("-")) return;

        // set new code
        string newCode = ShiftDigit(value);
        SetCurrentCode(newCode);
    }

    private string ShiftDigit(int digit)
    {
        Debug.Log("Digit to shift in: " + digit);
        // remove left most value
        string newCode = _currentCode.Substring(1);

        // add new digit
        newCode += digit.ToString();

        return newCode;
    }

    /// <summary>
    /// Set a new current code and notify listeners of change
    /// </summary>
    /// <param name="code"></param>
    private void SetCurrentCode(string code)
    {
        _currentCode = code;
        CurrentCodeUpdated?.Invoke(_currentCode);
    }


    /// <summary>
    /// Handles the enter Key being pressed
    /// </summary>
    public void HandleEnterCodeButton()
    {
        if (String.Equals(_currentCode, unlockCode.ToString()))
        {
            OnSuccesfullAttempt();
        }
        else
        {
            OnFailedAttempt();
        }
    }

    private void OnFailedAttempt()
    {
        Debug.Log("Lost, locking up...");

        // set the time callback to restore functionality after tiumer has elapsed
        timer.TimerElapsed += () => { SetCurrentCode("----"); RestoreFunctionality?.Invoke(); };

        // restart the timer from duration
        timer.RestartTimer();

        // notify listeners to take away functionality
        UnlockAttempt?.Invoke(false);
    }

    private void OnSuccesfullAttempt()
    {
        Debug.Log("Unlocked!");

        SetCurrentCode("----");

        // notify listeners
        UnlockAttempt?.Invoke(true);
    }

    // this is here to not have to close and open the game every time after winning or for resetting midway
    public void OnRestartButtonClicked()
    {
        timer.StopTimer(false);
        SetCurrentCode("----");
        RestoreFunctionality?.Invoke();
    }
}
