using UnityEngine;
using Photon.Pun;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform itemsParent;
    private Slot[] slots;

    private PlayerInventory playerInventory;

    private void Start()
    {
        // Ќаходим локального игрока
        foreach (var player in FindObjectsOfType<PlayerInventory>())
        {
            if (player.photonView.IsMine)
            {
                playerInventory = player;
                break;
            }
        }

        if (playerInventory == null)
        {
            Debug.LogError("InventoryUI: локальный PlayerInventory не найден!");
            return;
        }

        slots = itemsParent.GetComponentsInChildren<Slot>();

        playerInventory.OnItemAdded.AddListener(UpdateUI);
        playerInventory.OnItemUsed.AddListener(UpdateUI);

        UpdateUI();
    }

    public void UpdateUI()
    {
        if (playerInventory == null)
            return;

        Item[] items = playerInventory.GetItems();

        for (int i = 0; i < slots.Length; i++)
        {
            if (i < items.Length && items[i] != null)
                slots[i].AddItem(items[i]);
            else
                slots[i].ClearSlot();
        }
    }
}