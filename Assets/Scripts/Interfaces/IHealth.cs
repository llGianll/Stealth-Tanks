public interface IHealth 
{
    float CurrentHealth { get; set; }

    void DecreaseHealth();
    void Death();
}
