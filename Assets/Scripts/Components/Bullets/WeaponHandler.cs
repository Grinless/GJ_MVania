using UnityEngine;

[System.Serializable]
public class WeaponHandler
{
    public float _time = 0;

    public float currentTime = 0;

    public bool Elapsed
    {
        get => currentTime >= _time;
    }

    public void SetTime(float cooldown)
    {
        if (Elapsed)
        {
            _time = cooldown;
            currentTime = 0;
        }
    }

    public void UpdateTimer()
    {
        currentTime += Time.deltaTime;
    }
}