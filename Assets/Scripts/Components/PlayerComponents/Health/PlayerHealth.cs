[System.Serializable]
public struct PlayerHealth
{
    public float current;
    public float min;
    public float max;
    public float healthTanksMax;
    public float healthTanksUnlocked;
    public float currentHealthTanks;
}

public interface IPlayerDamage
{
    void ApplyDamage(int value); 
}

public interface IPlayerHeal
{
    void ApplyHealth(int value);

    void ApplyFullHealth();
}
