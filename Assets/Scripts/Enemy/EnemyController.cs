using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;

    Transform target;
    NavMeshAgent agent;
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distance = Vector3.Distance(target.position,  transform.position);

        // agregar condicion que haga que tambien vayan a target.position
        // si atacamos al enemy
        if(distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            
            if (distance <= agent.stoppingDistance)

            {
                // Attack the target (if melee)
                FaceTarget();

            }
        }

        if (distance >= lookRadius)
        {
            agent.SetDestination(new Vector3(37.5f, 1f, -0.5f));
        }
    }

    void FaceTarget()

    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    // esto es para ver el rango del lookRadius de los enemigos en la Scene.
    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}


