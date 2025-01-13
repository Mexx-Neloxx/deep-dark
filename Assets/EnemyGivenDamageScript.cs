using UnityEngine;
using UnityEngine.UI;

public class EnemyGivenDamageScript : MonoBehaviour
{
    private int HP = 100;
    public Animator animator;
    public Slider HealthBar;


    // Update is called once per frame
    void Update()
    {
        HealthBar.value = HP;

    }
    public void TakeDamage(int DamageAmount)
    {
        HP -= DamageAmount;

        if (HP <= 0)
        {
            Destroy(this.gameObject);
            HealthBar.gameObject.SetActive(false);


        }



    }
}
