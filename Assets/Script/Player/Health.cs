using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    // Максимальное здоровье
    [SerializeField] private float maxHealth = 100f;
    // Текущее здоровье
    [SerializeField] private float currentHealth;

    private void Awake()
    {
        // Устанавливаем текущее здоровье на максимальное значение
        currentHealth = maxHealth;
    }
    public void SetMaxHealth(float newMaxHealth)
    {
        maxHealth = newMaxHealth;
        currentHealth = maxHealth;
        Debug.Log($"Максимальное здоровье установлено: {maxHealth}");
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth <= 0)
        {
            // Если текущее здоровье уже 0 или меньше, ничего не делаем
            return;
        }
        // Уменьшаем текущее здоровье на величину урона
        currentHealth -= damage;
        Debug.Log($"Текущее здоровье: {currentHealth}");

        // Убеждаемся, что здоровье не опускается ниже 0
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        // Проверка на смерть
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float damage)
    {
        // Увеличиваем  текущее здоровье на величину урона
        currentHealth += damage;
        Debug.Log($"Текущее здоровье: {currentHealth}");
    }

    private void Die()
    {
        Debug.Log("Персонаж мёртв!");
    }
}
