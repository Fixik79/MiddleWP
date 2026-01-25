using UnityEngine;
using UnityEngine.UI; // Для UI-промпта

public class CollisionBehavior : MonoBehaviour
{
    // Массив способностей (через GetComponents)
    private ICollisionAbility[] _collisionAbilities; // Предполагаю, что у вас есть ICollisionAbility; если нет, оставьте пустым

    // Поле для текущего предмета в зоне триггера
    private ItemPickup currentPickup;
    private bool canPick = false;

    // UI-промпт (создайте Text на Canvas и перетащите сюда в Inspector)
    [SerializeField] private Text pickUpPrompt;

    private void Start()
    {
        _collisionAbilities = GetComponents<ICollisionAbility>();
        if (PlayerInventory.Instance == null)
        {
            Debug.LogError("CollisionBehavior: PlayerInventory.Instance не найден!");
        }
        // Скрываем промпт по умолчанию
        if (pickUpPrompt != null) pickUpPrompt.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, если это предмет
        var itemPickup = other.GetComponent<ItemPickup>();
        if (itemPickup != null && PlayerInventory.Instance != null)
        {
            currentPickup = itemPickup;
            canPick = true;
            // Показываем промпт
            if (pickUpPrompt != null) pickUpPrompt.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var itemPickup = other.GetComponent<ItemPickup>();
        if (itemPickup == currentPickup)
        {
            canPick = false;
            currentPickup = null;
            // Скрываем промпт
            if (pickUpPrompt != null) pickUpPrompt.enabled = false;
        }
        // Все еще вызываем способности при выходе, если нужно (оставил как есть)
        if (_collisionAbilities.Length > 0)
        {
            foreach (var ability in _collisionAbilities)
            {
                ability.UseAbility(other.gameObject);
            }
        }
    }

    private void Update()
    {
        // Проверяем нажатие E, если предмет в зоне и можно подобрать
        if (canPick && Input.GetKeyDown(KeyCode.E) && PlayerInventory.Instance != null)
        {
            currentPickup.PickUp(PlayerInventory.Instance);
            canPick = false;
            currentPickup = null;
            // Скрываем промпт (хотя объект уже уничтожен)
            if (pickUpPrompt != null) pickUpPrompt.enabled = false;
        }
    }
}