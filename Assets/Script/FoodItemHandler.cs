using UnityEngine;

public class FoodItemHandler : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            TryUseFirstFood();
        }
    }

    private void TryUseFirstFood()
    {
        var inventory = PlayerInventory.Instance;
        for (int i = 0; i < inventory.GetItems().Length; i++)
        {
            var item = inventory.GetItems()[i];
            if (item != null && item is IUsableItem)
            {
                if (inventory.UseItem(i))
                    return;
            }
        }
        Debug.Log("Нет еды для использования");
    }
}
