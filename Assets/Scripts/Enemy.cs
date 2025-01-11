using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int HP = 100;
    public NavMeshAgent NavMeshAgent;
    private MovementController Player;

    public void GiveDamage(int Damage)
    {
        HP -= Damage;
        if (HP <= 0)
            Destroy(this.gameObject);
    }

    void Start()
    {
        Player = FindAnyObjectByType<MovementController>();
    }

    void Update()
    {
        NavMeshAgent.SetDestination(Player.transform.position);
    }
}
