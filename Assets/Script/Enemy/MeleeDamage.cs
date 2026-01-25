using UnityEngine;

public class MeleeDamage : MonoBehaviour, ICollisionAbility
{
    [SerializeField] private float _damage = 1;

    public void UseAbility(GameObject target)
    {
        if (target.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TakeDamage(_damage);
        }
    }
}


