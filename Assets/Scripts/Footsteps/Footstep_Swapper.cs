using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSwapper : MonoBehaviour
{
    public string footstepEvent = "SurfaceType_Footsteps";
    public string defaultSurface = "Grass";
    public Transform[] footTransforms;
    public float rayDistance = 1.2f;
    public LayerMask groundMask = ~0;

    public void PlayFootstep()
    {
        PlayFootstepInternal(-1);
    }

    public void PlayFootstep(int footIndex)
    {
        PlayFootstepInternal(footIndex);
    }

    private void PlayFootstepInternal(int footIndex)
    {
        string surface = defaultSurface;

        Vector3 origin = transform.position;
        if (footTransforms != null && footTransforms.Length > 0)
        {
            if (footIndex >= 0 && footIndex < footTransforms.Length)
                origin = footTransforms[footIndex].position;
            else
                origin = footTransforms[0].position;
        }

        RaycastHit hit;
        if (Physics.Raycast(origin, Vector3.down, out hit, rayDistance, groundMask))
        {
            var s = hit.transform.GetComponent<SurfaceType>();
            if (s != null && !string.IsNullOrEmpty(s.surfaceType))
                surface = s.surfaceType;
        }

        // Choose the GameObject used as the Wwise emitter:
        // - Use the foot GameObject if available (better spatialization),
        // - otherwise use the player root (this script's GameObject).
        GameObject emitter = gameObject;
        if (footTransforms != null && footTransforms.Length > 0 && footIndex >= 0 && footIndex < footTransforms.Length)
            emitter = footTransforms[footIndex].gameObject;

        // Set switch on the emitter and post the event using the emitter GameObject
        AkUnitySoundEngine.SetSwitch("SurfaceType", surface, emitter);
        AkUnitySoundEngine.PostEvent(footstepEvent, emitter);
    }
}