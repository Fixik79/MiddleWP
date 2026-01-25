using DefaultNamespace.Components.Interfaces;
using UnityEngine;

public class BehaviourManager : MonoBehaviour
{
    private IBehaviour[] behaviours;
    private IBehaviour activeBehaviour;

    private void Awake()
    {
        behaviours = GetComponents<IBehaviour>();
    }

    private void Update()
    {
        float highestScore = float.MinValue;
        IBehaviour bestBehaviour = null;

        foreach (var behaviour in behaviours)
        {
            float score = behaviour.Evaluate();
            if (score > highestScore)
            {
                highestScore = score;
                bestBehaviour = behaviour;
            }
        }

        if (bestBehaviour != activeBehaviour)
        {
            activeBehaviour = bestBehaviour;
        }

        activeBehaviour?.Behave();
    }
}