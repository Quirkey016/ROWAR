using UnityEngine;

[RequireComponent(typeof(WwiseFootsteps))]
[RequireComponent(typeof(Rigidbody2D))]
public class FootstepAuto : MonoBehaviour
{
    [Tooltip("Horizontal distance player must travel to trigger a footstep")]
    public float stepDistance = 2f;

    [Tooltip("Minimum horizontal speed to register movement")]
    public float minSpeed = 0.1f;

    private WwiseFootsteps wwiseFootsteps;
    private Rigidbody2D rb;
    private Vector2 lastPosition;
    private float accumulatedDistance;

    void Awake()
    {
        wwiseFootsteps = GetComponent<WwiseFootsteps>();
        rb = GetComponent<Rigidbody2D>();
        lastPosition = transform.position;
    }

    void Update()
    {
        // Horizontal movement only
        Vector2 current = transform.position;
        float moved = Mathf.Abs(current.x - lastPosition.x);
        lastPosition = current;

        // Skip if below min speed
        if (moved < minSpeed * Time.deltaTime)
            return;

        // Skip if airborne — check downward raycast
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            Vector2.down,
            wwiseFootsteps.raycastDistance,
            wwiseFootsteps.groundMask
        );

        if (!hit.collider)
            return;

        accumulatedDistance += moved;
        if (accumulatedDistance >= stepDistance)
        {
            accumulatedDistance = 0f;
            wwiseFootsteps.PlayFootstep();
        }
    }
}