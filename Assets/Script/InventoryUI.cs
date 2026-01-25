using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public Slot[] slots;

    private void Start()
    {
        PlayerInventory.Instance.OnItemAdded.AddListener(UpdateUI);
        // Подписались на событие
        PlayerInventory.Instance.OnItemUsed.AddListener(UpdateUI); 
        slots = itemsParent.GetComponentsInChildren<Slot>();
        UpdateUI();
    }

    public void UpdateUI()
    {
        Item[] items = PlayerInventory.Instance.GetItems();

        for (int i = 0; i < slots.Length; i++)
        {
            if (i < items.Length && items[i] != null)
            {
                slots[i].AddItem(items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}