using DefaultNamespace.Components.Interfaces;
using UnityEngine;
using UnityEngine.AI;

public class ApproachBehaviour : MonoBehaviour, IBehaviour
{
    public Transform player;
    public NavMeshAgent agent;
    public float attackRange = 2f;

    private float DistanceToPlayer => Vector3.Distance(transform.position, player.position);

    public float Evaluate()
    {
        // Если игрок дальше атаки — возвращаем приоритет 1
        return DistanceToPlayer > attackRange ? 1f : 0f;
    }

    public void Behave()
    {
        if (agent != null && player != null)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
    }
}