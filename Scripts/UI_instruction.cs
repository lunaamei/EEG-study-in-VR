using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UI_instruction : MonoBehaviour
{
    // TODO panel mit antwortmöglichkeiten nach dem priming
    // TODO mehrere Blöcke
    //scripts
    [SerializeField] private TaskManager taskmanager;
    [SerializeField] private TextMeshProUGUI instruction;

    [SerializeField] private GameObject startpanel;
    
    

    public void DisplayText()
    {
        StartCoroutine(ShowInstruction());
    }
    
    
    IEnumerator ShowInstruction()
    {
        if (taskmanager.end)
        {
            instruction.text = "Thank you for your participation";
        }
        else
        {
            instruction.text = "Welcome to our experiment, in the following scene you will walk through an environment, please just follow the path in one direction";
        }
        startpanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        startpanel.SetActive(false);

    }
}
