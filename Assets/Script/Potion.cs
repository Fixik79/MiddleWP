using UnityEngine;

public class Potion : MonoBehaviour, ICollisionAbility
{
    [SerializeField] private float _damage = 10;

    public void UseAbility(GameObject target)
   {
        if (target.TryGetComponent<IHealing>(out var healing))
        {
            healing.PlusDamage(_damage);
        }
    }
}
