using UnityEngine;
using Photon.Pun;

public class Health : MonoBehaviourPun, IDamageable
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;

    private void Awake()
    {
        if (photonView.IsMine)
            currentHealth = maxHealth;
    }

    public void SetMaxHealth(float newMaxHealth)
    {
        if (!photonView.IsMine) return;

        photonView.RPC(nameof(RPC_SetMaxHealth), RpcTarget.All, newMaxHealth);
    }

    [PunRPC]
    private void RPC_SetMaxHealth(float value)
    {
        maxHealth = value;
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (!photonView.IsMine) return;

        photonView.RPC(nameof(RPC_TakeDamage), RpcTarget.All, damage);
    }

    [PunRPC]
    private void RPC_TakeDamage(float damage)
    {
        if (currentHealth <= 0) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
            Die();
    }

    public void Heal(float value)
    {
        if (!photonView.IsMine) return;

        photonView.RPC(nameof(RPC_Heal), RpcTarget.All, value);
    }

    [PunRPC]
    private void RPC_Heal(float value)
    {
        currentHealth += value;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    private void Die()
    {
        Debug.Log("Персонаж мёртв!");
    }
}
