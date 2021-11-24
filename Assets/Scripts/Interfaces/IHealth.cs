using System;

public interface IHealth 
{
    float CurrentHealth { get; set; }
    void DecreaseHealth();
    public Action<float, float> OnHealthUpdate { get; set; }
    void Death();
}

public interface IDeath
{
    bool IsDead { get; set; }
    string ID { get; set; }
    public Action<string> OnDeath { get; set; } 
    void Death();
}
