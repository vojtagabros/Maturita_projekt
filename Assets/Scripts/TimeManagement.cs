using UnityEngine;
using UnityEngine.UI;

public class TimeManagement : MonoBehaviour
{
    public Button CallHelpButton;

    void Start()
    {
        CallHelpButton.onClick.AddListener(OnCallHelp);
    }

    void OnCallHelp()
    {
        if (GameData.HelpCalled != 0)
            return;

        GameData.HelpCalled = Time.time - GameData.GameStartTime;
        CallHelpButton.interactable = false;
    }
}
