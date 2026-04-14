using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyMovement : MonoBehaviour
{
    public enum State { Roaming, Chasing, Dashing, Kiting }
    public State currentState = State.Roaming;
    public float moveSpeed = 3f;
    public float dashSpeed = 12f;
    public float sightRange = 10f;
    public float attackRange = 3f;
    public float personalBubble = 2f;
    public float dashCooldown = 3f;
    public float detectionRadius = 1.5f;
    public LayerMask hazardLayer;
    public bool blocked;
    private Rigidbody2D rb;
    private Transform player;
    private Vector2 roamTarget;
    private bool isDashing = false;
    private float nextDashTime;
    private SpriteRenderer sR;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sR = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        roamTarget = GetNewRoamPoint();
    }

    void Update()
    {
        if (player == null) return;

        float distToPlayer = Vector2.Distance(transform.position, player.position);

        // State Switching Logic
        if (distToPlayer > sightRange) currentState = State.Roaming;
        else if (distToPlayer <= attackRange && !isDashing) currentState = State.Kiting;
        else if (!isDashing) currentState = State.Chasing;

        HandleAvoidance();
        FlipSprite();
    }

    public void OnCollisionStay2D(Collision2D other)
    {
        blocked = other.gameObject.tag == "Shield";
    }

    public void GravityConst()
    {
        rb.AddForceY(9.63f);
    }
    
    void FixedUpdate()
    {
        GravityConst();
        if (blocked) return;
        switch (currentState)
        {
            case State.Roaming: Roam(); break;
            case State.Chasing: Chase(); break;
            case State.Kiting: Kite(); break;
        }
    }


    void Roam()
    {
        if (Vector2.Distance(transform.position, roamTarget) < 0.5f)
            roamTarget = GetNewRoamPoint();

        MoveTowards(roamTarget, moveSpeed);
    }

    void Chase()
    {
        MoveTowards(player.position, moveSpeed);

        // Dash logic: Dash if in range and cooldown is over
        if (Time.time >= nextDashTime && Vector2.Distance(transform.position, player.position) < attackRange * 1.5f)
        {
            StartCoroutine(DashAttack());
        }
    }

    void Kite()
    {
        // Moves away if too close, or shuffles side to side
        Vector2 directionAway = (transform.position - player.position).normalized;
        Vector2 kitePoint = (Vector2)transform.position + directionAway * personalBubble;
        
        // Add a bit of "oscillation" so they don't just walk in a straight line back
        kitePoint += new Vector2(Mathf.Sin(Time.time * 2f), Mathf.Cos(Time.time * 2f)) * 0.5f;

        MoveTowards(kitePoint, moveSpeed * 0.8f);
    }

    IEnumerator DashAttack()
    {
        isDashing = true;
        currentState = State.Dashing;
        Vector2 dashDir = (player.position - transform.position).normalized;
        
        float startTime = Time.time;
        while (Time.time < startTime + 0.2f) // Dash duration
        {
            rb.linearVelocity = dashDir * dashSpeed;
            yield return null;
        }

        rb.linearVelocity = Vector2.zero;
        nextDashTime = Time.time + dashCooldown;
        isDashing = false;
    }


    void HandleAvoidance()
    {
        // Simple Danger Avoidance: Check for objects in Hazard layer
        Collider2D danger = Physics2D.OverlapCircle(transform.position, detectionRadius, hazardLayer);
        if (danger != null)
        {
            Vector2 avoidDir = (transform.position - danger.transform.position).normalized;
            rb.AddForce(avoidDir * moveSpeed * 2f);
        }
    }

    void MoveTowards(Vector2 target, float speed)
    {
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
    }

    Vector2 GetNewRoamPoint()
    {
        return (Vector2)transform.position + Random.insideUnitCircle * 5f;
    }

    void FlipSprite()
    {
        sR.flipX = player.position.x - transform.position.x > 0;
        }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, personalBubble);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
