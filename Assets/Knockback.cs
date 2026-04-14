using System.Collections;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    private Rigidbody2D rb;
    public float knockbackForce = 10f;
    public float knockbackDuration = 0.2f;
    
   
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    public void ApplyKnockback(Transform otherTransform)
    {
     Vector2 direction = (transform.position - otherTransform.position).normalized;
            
     StopAllCoroutines(); 
     StartCoroutine(KnockbackCoroutine(direction));
    }
    
    private IEnumerator KnockbackCoroutine(Vector2 direction)
    {
            rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);

            yield return new WaitForSeconds(knockbackDuration);

            rb.linearVelocity = Vector2.zero;
    }
    
    


    
}
