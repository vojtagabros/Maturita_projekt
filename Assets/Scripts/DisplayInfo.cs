using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayInfo : MonoBehaviour
{
    public GameObject FailCanvas;
    public GameObject SuccesCanvas;
    
    public TextMeshProUGUI myText; 

    void Start()
    {
        FailCanvas.SetActive(false);
        SuccesCanvas.SetActive(true);
        
        if (GameData.Escaped == true)
        {
            if (GameData.HelpCalled == 0)
            {
                myText.text = "Help was not called";
            }
            else
            {
                myText.text = "Help called at: " + GameData.HelpCalled.ToString("F1") + "s";
            }
        }
        else
        {
            FailCanvas.SetActive(true);
            SuccesCanvas.SetActive(false);
        }

            
    }
}
