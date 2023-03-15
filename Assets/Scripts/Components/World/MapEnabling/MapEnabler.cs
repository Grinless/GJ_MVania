using System.Collections;
using UnityEngine;

public class MapEnabler : MonoBehaviour
{
    public AreaNode a1;
    public AreaNode a2;
    public bool startA1Loaded = true;
    public bool startA2Loaded = false;

    private void Start()
    {
        if (startA1Loaded)
            ChangeToState(true, false); 
        if(startA2Loaded)
            ChangeToState(false, true);
    }

    public void Update()
    {
        if (a1.triggered)
        {
            ChangeToState(true, false);
            a1.triggered = false;
        }
        if (a2.triggered)
        {
            ChangeToState (false, true);
            a2.triggered = false;
        }
    }

    private void ChangeToState(bool a1State, bool a2State)
    {
        if (a1State)
        {
            a1.loadTrigger.enabled = false;
            a1.loadable.SetActive(true);
            a2.loadTrigger.enabled = true;
            a2.loadable.SetActive(false);
        }
        if (a2State)
        {
            a1.loadTrigger.enabled = true;
            a1.loadable.SetActive(false);
            a2.loadTrigger.enabled = false;
            a2.loadable.SetActive(true);
        }
    }
}
