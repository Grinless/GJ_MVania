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

    private float CurrentHealth => PlayerController.instance.PlayerHealthData.current;

    private float MaxHealth => PlayerController.instance.PlayerHealthData.max;

    private float CurrentHealthReserves => PlayerController.instance.PlayerHealthData.currentHealthTanks;

    private float UnlockedHealthReserves => PlayerController.instance.PlayerHealthData.healthTanksUnlocked;

    private float PercentScale => maxHealthBarWidth / MaxHealth;

    private float CurrentHealthBarWidth
    {
        get => currentHealthBarRect.rect.width;
        set => currentHealthBarRect.sizeDelta = new Vector2(value, currentHealthBarRect.sizeDelta.y);
    }


    private void Awake()
    {
        if(instance == null)
            instance = this;

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
            //Update empty slots.
            refilIconsEmpty[i].enabled = i <= UnlockedHealthReserves - 1;
            
            //Update active tanks.
            refilIconsFull[i].enabled = i <= UnlockedHealthReserves - 1;
        }
    }
}
