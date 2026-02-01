
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;

public class PlayerInventory : MonoBehaviourPun
{
    [SerializeField] private int inventorySize = 10;
    public Item[] inventoryItems;

    public UnityEvent OnItemAdded;
    public UnityEvent OnItemUsed;
    internal static object Instance;

    private void Awake()
    {
        inventoryItems = new Item[inventorySize];
    }

    // ======================
    // Добавление предмета
    // ======================
    public bool AddItem(Item itemToAdd)
    {
        if (!photonView.IsMine)
            return false;

        for (int i = 0; i < inventoryItems.Length; i++)
        {
            if (inventoryItems[i] == null)
            {
                inventoryItems[i] = itemToAdd;

                string tagDisplay = string.IsNullOrEmpty(itemToAdd.itemTypeTag)
                    ? "неизвестный тип"
                    : itemToAdd.itemTypeTag;

                Debug.Log($"[{photonView.ViewID}] Добавлен предмет \"{itemToAdd.itemName}\" в слот {i}");

                OnItemAdded?.Invoke();
                return true;
            }
        }

        Debug.Log("Инвентарь полон!");
        return false;
    }

    // ======================
    // Использование предмета
    // ======================
    public bool UseItem(int index)
    {
        if (!photonView.IsMine)
            return false;

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
            usableItem.Use(gameObject); // gameObject = владелец

            inventoryItems[index] = null;
            OnItemUsed?.Invoke();
            return true;
        }

        Debug.Log($"Предмет {item.itemName} нельзя использовать");
        return false;
    }

    public Item[] GetItems()
    {
        return inventoryItems;
    }
}
