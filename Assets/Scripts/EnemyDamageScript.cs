using UnityEngine;

public class EnemyDamageScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int DamageAmount = 20;
    public GameObject Enemy;
    private Animator enemyAnimator;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            enemyAnimator = Enemy.GetComponent<Animator>();
            if (enemyAnimator != null)
            {
                AnimatorStateInfo stateInfo = enemyAnimator.GetCurrentAnimatorStateInfo(0);

                if (stateInfo.IsName("StandingMeleeAttack"))
                {

                    other.GetComponent<EnemyGivenDamageScript>().TakeDamage(DamageAmount);

                }


            }
        }
    }
}
