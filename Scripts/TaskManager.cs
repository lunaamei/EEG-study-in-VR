using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;
using TMPro;

public class TaskManager : MonoBehaviour
{
    // scripts
    public WriteCSV writeCSV;
 
    // Create new stopwatch using System.Diagnostics
    public Stopwatch stopwatch = new();
    [SerializeField] private GameObject startpanel;
    public GameObject panel;
    public GameObject text_1;
    public GameObject text_2;
    [SerializeField] private TextMeshProUGUI instruction;
    
    //data
    private float _rt;
    private string _response;
    private int _rating;
    public string _stimulus;
    public int _cntTrial;
    [SerializeField] private int cntStimulus = 8;
    public bool end;
    
    // to save the data	
    private List<string> _data = new List<string>();
    private string _header = "Stimulus, ReactionTime, Response, Rating";

    
    private void Start()
    {
        _cntTrial = 0;
        end = false;
        
        instruction.text = "Welcome to our experiment. In the following scene you will follow a path with another person, who will make you aware of some objects in the scene. Please stay on the path and press Q if you see a tree and E if you see a house. Press Space to begin.";
        startpanel.SetActive(true);
    }

    private void BeginApp()
    {
        if (!end)
        { 
            if (Input.GetKeyDown("space")) 
            {
                startpanel.SetActive(false); 
            }  
        }

    }

    private void ChangeScene()
    {
        //jump to the next scene in build settings
        SceneManager.LoadScene(1);
        _cntTrial = 0;
        end = false;
        
    }
    private void EndApp()
    {
        Debug.Log("end exp");
        writeCSV.MakeCSV(_data, _header); // save Data

        if (end)
        {
            //display end text
            instruction.text = "Thank you for your participation. Press space to end the application";
            startpanel.SetActive(true);

            if (Input.GetKeyDown("space"))
            {
                startpanel.SetActive(false);
                //Application.Quit(); // end application
                UnityEditor.EditorApplication.isPlaying = false;
            }  
        }
    }

    private void ResetTime()
    {
        // Stop
        stopwatch.Stop();
        // Save
        _rt = stopwatch.ElapsedMilliseconds;
        Debug.Log("reaction time " + _rt);
        // Reset
        stopwatch.Reset();

    }
    
    private void SaveResponses()
    {
        // add all the data to the list
        string currData = (_stimulus + "," + _rt.ToString() + "," + _response + "," + _rating.ToString());
        // add the new list to the list of lists that will become our output csv
        _data.Add(currData);
    }


    void Update()
    {
        BeginApp();
        
        //Stopwatch is started in the Trigger Script

        // processing the response
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
        {
            ResetTime(); // stop and reset stopwatch
            // define the correct response
            if (Input.GetKeyDown(KeyCode.Q)) { 
                _response = "Tree";
            } else if (Input.GetKeyDown(KeyCode.E)) { 
                _response = "House";
            }
            //change panel text
            text_1.SetActive(false);
            text_2.SetActive(true);


        }

        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3) ||
            Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Alpha5))
        {
            //define rating reposnes
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _rating = 1;

            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _rating = 2;

            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                _rating = 3;

            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                _rating = 4;

            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                _rating = 5;

            }
         
            //deactivate rating
            text_2.SetActive(false);
            //disable panel
            panel.SetActive(false);
            SaveResponses();
            
        
            //begin new trial
            _cntTrial++;
            Debug.Log(_cntTrial);
        }
      

 
        //if all trials are completed
        if (_cntTrial >= cntStimulus)
        {
            end = true;
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                //display end text
                instruction.text = "You completed the test trial. Please press space to proceed to the actual experiment.";
                startpanel.SetActive(true);

                if (Input.GetKeyDown("space"))
                {
                    startpanel.SetActive(false);
                    ChangeScene();
                } 
                
            } else if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                EndApp();
            }
        }

    }
}
