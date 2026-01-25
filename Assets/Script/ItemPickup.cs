using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    // Массив возможных предметов (в Inspector: добавьте Item-ассеты)
    public Item[] items;

    public void PickUp(PlayerInventory inventory)
    {
        if (items.Length > 0)
        {
            // Рандомный выбор
            Item randomItem = items[Random.Range(0, items.Length)];

            // Пробуем добавить в инвентарь
            if (inventory.AddItem(randomItem))
            {
                // Только если успешно — уничтожаем объект
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Не удалось подобрать предмет: инвентарь полон.");
            }
        }
        else
        {
            Debug.LogWarning("ItemPickup: Нет предметов в массиве!");
        }
    }
}