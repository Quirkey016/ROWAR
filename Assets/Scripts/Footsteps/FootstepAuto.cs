using UnityEngine;

[RequireComponent(typeof(FootstepSwapper))]
public class FootstepAuto : MonoBehaviour
{
    [Tooltip("Horizontal distance player must travel to trigger the next footstep")]
    public float stepDistance = 2f;

    [Tooltip("Minimum horizontal speed to consider player 'walking'")]
    public float minSpeed = 0.1f;

    // If you want per-foot detection you can leave this empty and the Footstep_Swapper's footTransforms will be used.
    public Transform[] footTransforms;

    FootstepSwapper swapper;
    Vector3 lastPosition;
    float accumulatedDistance;
    int nextFoot = 0;

    void Awake()
    {
        swapper = GetComponent<FootstepSwapper>();
        lastPosition = transform.position;
        if (swapper == null)
            Debug.LogError("Footstep_Swapper required by FootstepAuto.", this);
    }

    void Update()
    {
        // Horizontal movement delta
        Vector3 delta = transform.position - lastPosition;
        delta.y = 0f;
        float moved = delta.magnitude;
        lastPosition = transform.position;

        // Skip if not moving enough
        if (moved < minSpeed * Time.deltaTime)
            return;

        // Check grounded: prefer CharacterController, otherwise raycast down using swapper's groundMask and rayDistance
        bool grounded = true;
        var cc = GetComponent<CharacterController>();
        if (cc != null)
        {
            grounded = cc.isGrounded;
        }
        else
        {
            RaycastHit hit;
            grounded = Physics.Raycast(transform.position, Vector3.down, out hit, swapper.rayDistance, swapper.groundMask);
        }

        if (!grounded)
            return;

        accumulatedDistance += moved;
        if (accumulatedDistance >= stepDistance)
        {
            accumulatedDistance = 0f;

            // If footTransforms provided here use them, otherwise fall back to swapper's footTransforms or no-index call
            if ((footTransforms != null && footTransforms.Length > 0) || (swapper.footTransforms != null && swapper.footTransforms.Length > 0))
            {
                int footCount = (footTransforms != null && footTransforms.Length > 0) ? footTransforms.Length : swapper.footTransforms.Length;
                swapper.PlayFootstep(nextFoot % footCount);
                nextFoot++;
            }
            else
            {
                swapper.PlayFootstep();
            }
        }
    }
}