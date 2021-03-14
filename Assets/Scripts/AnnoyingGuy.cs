using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnnoyingGuy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject player;

    public IEnumerator Break()
    {
        yield return new WaitForSeconds(5.0f);
        ChasePlayer();
    }

    public void ChasePlayer()
    {
        agent.SetDestination(player.transform.position);
        StartCoroutine(Break());
    }
}
