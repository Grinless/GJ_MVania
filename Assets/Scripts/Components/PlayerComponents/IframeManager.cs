using UnityEngine;

[System.Serializable]
public class IframeManager
{
    public float iframes = 1f;
    public float currentIframes = 0;

    public bool IframesActive
    {
        get => (currentIframes > 0);
    }

    public void ActivateIframes()
    {
        if (!IframesActive)
        {
            currentIframes += iframes;
        }
    }

    public void UpdateIframes()
    {
        if (IframesActive)
        {
            currentIframes -= Time.deltaTime;
            if (currentIframes < 0)
            {
                currentIframes = 0;
            }
        }
    }
}
