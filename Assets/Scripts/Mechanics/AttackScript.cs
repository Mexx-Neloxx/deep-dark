using UnityEngine;

public class AttackScript : MonoBehaviour
{
    Animator animator;
    void Start()
    {
      animator = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) animator.SetBool("IsAttacking", true);
        else if (Input.GetMouseButtonUp(0)) animator.SetBool("IsAttacking", false);
    }
}
