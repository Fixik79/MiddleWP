using UnityEngine;
using Photon.Pun;

public class FoodItemHandler : MonoBehaviourPun
{
    private PlayerInventory inventory;

    private void Start()
    {
        // 🔒 только для владельца
        if (!photonView.IsMine)
            return;

        inventory = GetComponent<PlayerInventory>();

        if (inventory == null)
            Debug.LogError("FoodItemHandler: PlayerInventory не найден!");
    }

    private void Update()
    {
        if (!photonView.IsMine)
            return;

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            TryUseFirstFood();
        }
    }

    private void TryUseFirstFood()
    {
        Item[] items = inventory.GetItems();

        for (int i = 0; i < items.Length; i++)
        {
            Item item = items[i];

            // тут можно дополнительно проверить тег "Food"
            if (item != null && item is IUsableItem)
            {
                if (inventory.UseItem(i))
                    return;
            }
        }

        Debug.Log("Нет еды для использования");
    }
}
