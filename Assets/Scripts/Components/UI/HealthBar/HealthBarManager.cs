using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    private static HealthBarManager instance;
    private RectTransform currentHealthBarRect; 
    private float maxHealthBarWidth; 
    public Image[] refilIconsEmpty;
    public Image[] refilIconsFull;
    public Image currentHealthBar;

    private float CurrentHealth
    {
        get => PlayerController.instance.PlayerHealthData.current;
    }

    private float MaxHealth
    {
        get => PlayerController.instance.PlayerHealthData.max;
    }

    private float CurrentHealthReserves
    {
        get => PlayerController.instance.PlayerHealthData.currentHealthTanks;
    }

    private float UnlockedHealthReserves
    {
        get => PlayerController.instance.PlayerHealthData.healthTanksUnlocked;
    }

    private float PercentScale
    {
        get => maxHealthBarWidth / MaxHealth;
    }

    private float CurrentHealthBarWidth
    {
        get => currentHealthBarRect.rect.width;
        set => currentHealthBarRect.sizeDelta = new Vector2(value, currentHealthBarRect.sizeDelta.y);
    }


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        currentHealthBarRect = currentHealthBar.rectTransform;
        maxHealthBarWidth = CurrentHealthBarWidth;
    }

    private void Update()
    {
        UpdateHealthbarWidth();
        UpdateHealthReserves();
    }

    public void UpdateHealthbarWidth() => CurrentHealthBarWidth = PercentScale * CurrentHealth;

    public void UpdateHealthReserves()
    {
        for (int i = 0; i < refilIconsEmpty.Length; i++)
        {
            if(i > UnlockedHealthReserves - 1) //Update empty slots. 
                refilIconsEmpty[i].enabled = false;
            else
                refilIconsEmpty[i].enabled = true;

            if(i > CurrentHealthReserves) //Update active tanks. 
                refilIconsFull[i].enabled = false;
            else
                refilIconsFull[i].enabled = true;
        }
    }
}
