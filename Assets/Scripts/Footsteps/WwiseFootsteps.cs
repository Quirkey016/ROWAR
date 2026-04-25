using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class WwiseFootsteps : MonoBehaviour
{
    [Header("Wwise")]
    public string footstepEvent = "SurfaceType_Footsteps";
    public string defaultSurface = "Grass";

    [Header("Detection")]
    public float raycastDistance = 1.2f;
    public LayerMask groundMask = ~0;

    [Header("Timing")]
    public float minStepInterval = 0.3f;

    private string currentSurface;
    private float lastStepTime;

    public void PlayFootstep()
    {
        if (Time.time - lastStepTime < minStepInterval) return;

        DetectSurface();
        AkUnitySoundEngine.SetSwitch("SurfaceType", currentSurface, gameObject);
        AkUnitySoundEngine.PostEvent(footstepEvent, gameObject);
        lastStepTime = Time.time;
    }

    private void DetectSurface()
    {
        Vector2 rayOrigin = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, raycastDistance, groundMask);

        if (hit.collider != null)
        {
            SurfaceIdentifier surface = hit.collider.GetComponent<SurfaceIdentifier>();
            currentSurface = surface != null ? surface.surfaceType : defaultSurface;
        }
        else
        {
            currentSurface = defaultSurface;
        }
    }
}