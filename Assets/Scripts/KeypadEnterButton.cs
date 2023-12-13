using UnityEngine;

public class KeypadEnterButton : KeypadBaseButton
{
    protected override void OnButtonClick()
    {
        Debug.Log("Clicked enter button");
        controller.HandleEnterCodeButton();
    }

    protected override void HandleNewCodeReceived(string code)
    {
        // make button interactable only when we have all 4 digits in place
        thisButton.interactable = !code.Contains("-");
    }
}
