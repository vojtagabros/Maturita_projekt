using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManagement : MonoBehaviour
{
    public Button CallHelpButton;

    private float helpCalled;
    // Start is called before the first frame update
    void Start()
    {
        CallHelpButton.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TaskOnClick()
    {
        helpCalled = Time.time;
        Debug.Log("Help called at: " + helpCalled);
    }
}
