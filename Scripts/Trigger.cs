using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    //scripts
    [SerializeField] private TaskManager taskmanager;
    
    //stimuli
    public AudioSource playAudio;
    public GameObject stimulus;
    

    private void Start()
    {
        stimulus.SetActive(false);
       
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Delay());
        // get the stimulus type and pass to taskmanager
        taskmanager._stimulus = stimulus.name;
        

    }

    IEnumerator Delay()
    {
        //when player enters the trigger zone, audio is played
        playAudio.Play();
        // delay
        yield return new WaitForSeconds(2f);
        // tree will appear
        stimulus.SetActive(true);
        // start recording the reaction time
        taskmanager.stopwatch.Start();
        // delay
        yield return new WaitForSeconds(0.5f);
        //enable panel for subject response instruction
        taskmanager.panel.SetActive(true);
        taskmanager.text_1.SetActive(true);
        

    }
    
    
 
 
}
