using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarManager : MonoBehaviour
{
    private static HealthBarManager instance;
    public Sprite[] refilIcons;
    public Sprite currentHealthBar;

    private float spriteWidth; 
    private float currentSpriteWidth;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        
    }

    public void UpdateHealthbarWidth(float spriteWidth)
    {

    }


}
