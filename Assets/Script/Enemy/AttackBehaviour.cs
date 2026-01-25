using DefaultNamespace.Components.Interfaces;
using UnityEngine;
using UnityEngine.AI;

public class AttackBehaviour : MonoBehaviour, IBehaviour
{
    public Transform player;
    public NavMeshAgent agent;
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;
    private float lastAttackTime;

    private float DistanceToPlayer => Vector3.Distance(transform.position, player.position);

    public float Evaluate()
    {
        // Если игрок в зоне атаки — высокий приоритет (2)
        return DistanceToPlayer <= attackRange ? 2f : 0f;
    }

    public void Behave()
    {
        if (agent != null)
            agent.isStopped = true;

        if (Time.time - lastAttackTime > attackCooldown)
        {
            Debug.Log("Атака игрока!");
            lastAttackTime = Time.time;
            // Здесь можно вызвать метод нанесения урона
        }
    }
}