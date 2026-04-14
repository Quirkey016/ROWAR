using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float enemyHealth = 30f;
    public bool isBoss = false;
    public Pause pause;

    // Update is called once per frame
    void Update()
    {
        if(enemyHealth <= 0  && !isBoss)
        {
            Destroy(gameObject);
        }
        if(enemyHealth <= 0  && isBoss)
        {
            pause.Win();
            Destroy(gameObject);
        }
    }

    public void EnemyTakeDamage(float damage)
    {
        if(damage > 0)
        {
            enemyHealth -= damage;
        } 
        
    }
}
