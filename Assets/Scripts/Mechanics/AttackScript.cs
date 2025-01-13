using UnityEngine;

public class AttackScript : MonoBehaviour
{
    Animator animator;
    public AudioSource swordSound;  // Ссылка на компонент AudioSource
    public AudioClip attackSound;   // Звук удара

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("IsAttacking", true);
            if (swordSound != null && attackSound != null)
            {
                swordSound.PlayOneShot(attackSound);  // Воспроизведение звука удара
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("IsAttacking", false);
        }
    }
}
