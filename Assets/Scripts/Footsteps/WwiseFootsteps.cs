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

    private string currentSurface = "Grass";

    public void PlayFootstep()
    {
        DetectSurface();
        AkUnitySoundEngine.SetSwitch("SurfaceType", currentSurface, gameObject);
        AkUnitySoundEngine.PostEvent(footstepEvent, gameObject);
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