using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmActivation : MonoBehaviour
{
    public GameObject[] inactiveAlarms; 
    public GameObject[] activeAlarms;

    private void Start()
    {
        foreach (GameObject alarm in inactiveAlarms)
        {
            alarm.SetActive(true);
        }

        foreach (GameObject alarm in activeAlarms)
        {
            alarm.SetActive(false);
        }
    }

    public void EnableAlarms()
    {
        foreach (GameObject alarm in inactiveAlarms)
        {
            alarm.SetActive(false);
        }

        foreach (GameObject alarm in activeAlarms)
        {
            alarm.SetActive(true);
        }
    }
}
