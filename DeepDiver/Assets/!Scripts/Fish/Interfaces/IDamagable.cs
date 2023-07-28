
public interface IDamagable
{
    int MaxHealth { get; set; }
    int CurrentHealth { get; set; }
    int CatchTime { get; set; }

    void Damage(int damageAmount);

    void Die();
}