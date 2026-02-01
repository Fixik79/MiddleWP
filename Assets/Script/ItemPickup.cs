using UnityEngine;
using Photon.Pun;

public class ItemPickup : MonoBehaviourPun
{
    [SerializeField] private Item[] items;
    private bool isPickedUp = false;

    public void PickUp(PlayerInventory inventory)
    {
        if (inventory == null || isPickedUp)
            return;

        if (!inventory.photonView.IsMine)
            return;

        if (items == null || items.Length == 0)
            return;

        Item randomItem = items[Random.Range(0, items.Length)];

        if (inventory.AddItem(randomItem))
        {
            isPickedUp = true;

            if (PhotonNetwork.IsMasterClient)
                PhotonNetwork.Destroy(gameObject);
            else
                photonView.RPC(nameof(RequestDestroy), RpcTarget.MasterClient);
        }
    }

    [PunRPC]
    private void RequestDestroy()
    {
        PhotonNetwork.Destroy(gameObject);
    }
}