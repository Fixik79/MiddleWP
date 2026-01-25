using UnityEngine;

public class BasicRigidBodyPush : MonoBehaviour
{
    // Слой объектов, которые можно толкать
    public LayerMask pushLayers;
    // По умолчанию разрешаем толкание
    public bool canPush = true;
    // Сила толчка
    [Range(0.5f, 5f)] public float strength = 1.1f; 

    void Start()
    {
        pushLayers = LayerMask.GetMask("Pushable");
        canPush = true;
    }

    public void Push(GameObject source)
    {
        if (canPush && TryGetComponent<Rigidbody>(out var body))
        {
            // Проверяем, находится ли объект на нужном слое
            if ((pushLayers & (1 << source.layer)) == 0)
            {
                // Выходим, если объект не на нужном слое
                return; 
            }

            // Направление отталкивания
            Vector3 pushDir = (transform.position - source.transform.position).normalized; 
            body.AddForce(pushDir * strength, ForceMode.Impulse);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (canPush)
        {
            PushRigidBodies(hit);
        }
        CheckForDamage(hit);
    }

    private void PushRigidBodies(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        if (body == null || body.isKinematic)
        {
            // Игнорируем, если нет Rigidbody или он кинематический
            return; 
        }

        if ((pushLayers & (1 << body.gameObject.layer)) == 0)
        {
            // Выходим, если объект не на нужном слое
            return;
        }

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0.0f, hit.moveDirection.z);
        body.AddForce(pushDir * strength, ForceMode.Impulse);
    }

    private void CheckForDamage(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Enemy") || hit.collider.CompareTag("Hazard"))
        {
            if (TryGetComponent<Health>(out var health))
            {
                float damageAmount = 10f; 
                health.TakeDamage(damageAmount);
            }
        }
        else if (hit.collider.CompareTag("Potion"))
        {
            if (TryGetComponent<Health>(out var health))
            {// Количество здоровья, которое прибавляется
                float healingAmount = 10f;
                // Прибавляем здоровье
                health.Heal(healingAmount); 

                // Удаляем объект "Potion"
                Destroy(hit.collider.gameObject);
            }
        }
    }
}
