using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class DamageScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int DamageAmount = 30;
    public GameObject Player;
    private Animator playerAnimator;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {

            playerAnimator = Player.GetComponent<Animator>();
            if (playerAnimator != null)
            {
                AnimatorStateInfo stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0); 

                if (stateInfo.IsName("StandingMeleeAttack")) 
                {
                    
                    other.GetComponent<EnemyScript>().TakeDamage(DamageAmount); 
                    
                }
                

            }
        }
    }
}
