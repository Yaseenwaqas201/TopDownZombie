public static class EventManager 
{
    public delegate void GameOverEvent();
    public static event GameOverEvent gameOverEvent;

    public delegate void UpdateGameEconomy();
    public static event UpdateGameEconomy updateGameEconomy;

    public delegate void WeaponUpgrade();
    public static event WeaponUpgrade weaponUpgrade;
    

    public delegate void WaveCompletedEvent();
    public static event WaveCompletedEvent waveCompletedEvent;
    
    
    public delegate void HealthUpgrade();
    public static event HealthUpgrade healthUpgrade;
    
    public delegate void RangeUpgrade();
    public static event RangeUpgrade rangeUpgrade;
    
    public delegate void EnemyKilledEvent();
    public static event EnemyKilledEvent enemyKilledEvent;
    
    public delegate void WarrierBladeUpgrade();
    public static event WarrierBladeUpgrade warrierBladeUpgrade;
    public static void InvokeWarrierBladeUpgrade()
    {
        warrierBladeUpgrade?.Invoke();
    }

    
    public static void InvokeEnemyKilledEvent()
    {
        enemyKilledEvent?.Invoke();
    }

    public static void InvokeRangeUpgrade()
    {
        rangeUpgrade?.Invoke();
    }
    public static void InvokeWaveCompletedEvent()
    {
        waveCompletedEvent?.Invoke();
    }
    
    public static void InvokeHealthUpgrade()
    {
        healthUpgrade?.Invoke();
    }

    public static void InvokeWeaponUpgrade()
    {
        weaponUpgrade?.Invoke();
    }

    
    public static void InvokeUpdateGameEconomy()
    {
        updateGameEconomy?.Invoke();
    }
    
    public static void InvokeGameOverEvent()
    {
        gameOverEvent?.Invoke();
    }
}
