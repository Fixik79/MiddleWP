
using System;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Progress;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;

    [SerializeField] private int inventorySize = 10;
    public Item[] inventoryItems;

    public UnityEvent OnItemAdded;
    //Добавлено
    public UnityEvent OnItemUsed;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        inventoryItems = new Item[inventorySize];
    }

    public bool AddItem(Item itemToAdd)
    {
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            if (inventoryItems[i] == null)
            {
                inventoryItems[i] = itemToAdd;

                // Логируем предмет и его тег
                string tagDisplay = string.IsNullOrEmpty(itemToAdd.itemTypeTag) ? "неизвестный тип" : itemToAdd.itemTypeTag;
                Debug.Log($"Предмет \"{itemToAdd.itemName}\" с типом \"{tagDisplay}\" добавлен в слот {i}");

                OnItemAdded?.Invoke();
                return true;
            }
        }

        Debug.Log("Инвентарь полон!");
        return false;
    }

    public bool UseItem(int index)
    {
        if (index < 0 || index >= inventoryItems.Length)
        {
            Debug.LogWarning("Неверный индекс предмета");
            return false;
        }

        Item item = inventoryItems[index];
        if (item == null)
        {
            Debug.Log("Слот пуст");
            return false;
        }

        if (item is IUsableItem usableItem)
        {
            // Передаем игрока как пользователя
            usableItem.Use(gameObject);
            // Удаляем предмет после использования
            inventoryItems[index] = null; 
            OnItemUsed?.Invoke();
            return true;
        }
        else
        {
            Debug.Log($"Предмет {item.itemName} нельзя использовать");
            return false;
        }
    }

    public Item[] GetItems()
    {
        return inventoryItems;
    }
}