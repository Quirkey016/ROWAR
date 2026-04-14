using UnityEngine;

public class WwiseFootsteps : MonoBehaviour
{
    [Header("Wwise")]
    [SerializeField] private AK.Wwise.Event footstepEvent;
    [SerializeField] private AK.Wwise.Switch haySwitch;
    [SerializeField] private AK.Wwise.Switch grassSwitch;

    [Header("Detection")]
    [SerializeField] private float raycastDistance = 2.0f;

    private string currentSurface = "Grass";

    public void PlayFootstep()
    {
        DetectSurface();
        SetWwiseSwitch();
        footstepEvent.Post(gameObject);
    }

    private void DetectSurface()
    {
        Vector3 rayOrigin = transform.position + Vector3.up * 0.5f;

        // Ignores the player's own layer
        int playerLayer = gameObject.layer;
        int layerMask = ~(1 << playerLayer);

        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, raycastDistance, layerMask);

        if (hit.collider != null)
        {
            Debug.Log("Hit: " + hit.collider.gameObject.name);

            SurfaceIdentifier surface = hit.collider.GetComponent<SurfaceIdentifier>();
            Debug.Log("SurfaceIdentifier found: " + (surface != null ? surface.surfaceType : "NULL - no component found"));

            currentSurface = surface != null ? surface.surfaceType : "Grass";
        }
        else
        {
            Debug.Log("Raycast hit nothing - origin: " + rayOrigin);
        }
    }

    private void SetWwiseSwitch()
    {
        switch (currentSurface)
        {
            case "Hay": haySwitch.SetValue(gameObject); break;
            default: grassSwitch.SetValue(gameObject); break;
        }
    }
}