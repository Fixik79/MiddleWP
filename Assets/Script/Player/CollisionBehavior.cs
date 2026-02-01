
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CollisionBehavior : MonoBehaviourPun
{
    // Способности при коллизиях (если используешь)
    private ICollisionAbility[] _collisionAbilities;

    // Текущий предмет в зоне подбора
    private ItemPickup currentPickup;
    private bool canPick = false;

    // Инвентарь ЭТОГО игрока
    private PlayerInventory inventory;

    // UI-промпт (ищем в runtime)
    private Text pickUpPrompt;

    private void Start()
    {
        // 🔒 ТОЛЬКО локальный игрок
        if (!photonView.IsMine)
            return;

        // Получаем инвентарь
        inventory = GetComponent<PlayerInventory>();
        if (inventory == null)
        {
            Debug.LogError("CollisionBehavior: PlayerInventory не найден на игроке!");
            enabled = false;
            return;
        }

        _collisionAbilities = GetComponents<ICollisionAbility>();

        // 🔥 ИЩЕМ UI НА CANVAS
        GameObject promptObj = GameObject.Find("PickUpPrompt");
        if (promptObj != null)
        {
            pickUpPrompt = promptObj.GetComponent<Text>();
            pickUpPrompt.enabled = false;
        }
        else
        {
            Debug.LogError("CollisionBehavior: PickUpPrompt не найден на Canvas!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!photonView.IsMine)
            return;

        ItemPickup itemPickup = other.GetComponent<ItemPickup>();
        if (itemPickup == null)
            return;

        currentPickup = itemPickup;
        canPick = true;

        if (pickUpPrompt != null)
            pickUpPrompt.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!photonView.IsMine)
            return;

        ItemPickup itemPickup = other.GetComponent<ItemPickup>();
        if (itemPickup != currentPickup)
            return;

        currentPickup = null;
        canPick = false;

        if (pickUpPrompt != null)
            pickUpPrompt.enabled = false;

        // способности (если реально нужны)
        if (_collisionAbilities != null)
        {
            foreach (var ability in _collisionAbilities)
            {
                ability.UseAbility(other.gameObject);
            }
        }
    }

    private void Update()
    {
        if (!photonView.IsMine)
            return;

        if (!canPick || currentPickup == null)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            currentPickup.PickUp(inventory);

            canPick = false;
            currentPickup = null;

            if (pickUpPrompt != null)
                pickUpPrompt.enabled = false;
        }
    }
}