using UnityEngine;

/// <summary>
/// Class responsible for managing IFrames. 
/// </summary>
[System.Serializable]
public class IframeManager
{
    /// <summary>
    /// The amount of IFrames to add per tick. 
    /// </summary>
    public float iframes = 1f;

    /// <summary>
    /// The remaining number of IFrames. 
    /// </summary>
    public float currentIframes = 0;

    /// <summary>
    /// Are IFrames active. 
    /// </summary>
    public bool IframesActive
    {
        get => (currentIframes > 0);
    }

    /// <summary>
    /// Activate a new IFrame Timer. 
    /// </summary>
    public void ActivateIframes()
    {
        if (!IframesActive)
            currentIframes += iframes;
    }

    /// <summary>
    /// Update the IFrame Manager. 
    /// </summary>
    public void UpdateIframes()
    {
        if (!IframesActive)
            return;

        currentIframes -= Time.deltaTime;
        if (currentIframes < 0)
            currentIframes = 0;
    }
}
