using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform player; 
    private NavMeshAgent agent; 
    public Animator animator; 
    public float attackRange = 2f; 
    private bool isAttacking = false; 
    public float attackCooldown = 1.5f;
    

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            
            if (agent != null)
            {
                agent.isStopped = false;
                agent.SetDestination(player.position);
            }

            
            animator.SetBool("IsAttacking", false);
            isAttacking = false; 
        }
        else
        {
            
            if (!isAttacking)
            {
                StartAttack();
            }
        }
    }

    private void StartAttack()
    {
        isAttacking = true; 
        if (agent != null)
        {
            agent.transform.LookAt(player);
            agent.isStopped = true;
            agent.SetDestination(agent.transform.position);
        }

        animator.SetBool("IsAttacking", true); 

       
        Invoke(nameof(EndAttack), attackCooldown);
    }

    private void EndAttack()
    {
        animator.SetBool("IsAttacking", false); 
        isAttacking = false; 
    }
}
