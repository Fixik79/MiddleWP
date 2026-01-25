using UnityEngine;

[CreateAssetMenu(fileName = "NewFoodItem", menuName = "Items/Food")]
public class FoodItem : Item, IUsableItem
{
    [SerializeField] private int healthRestoreAmount = 25;

    public void Use(GameObject user)
    {
        var health = user.GetComponent<Health>();
        if (health != null)
        {
            health.Heal(healthRestoreAmount);
            Debug.Log($"{itemName} использован. Восстановлено {healthRestoreAmount} HP.");
        }
    }
}