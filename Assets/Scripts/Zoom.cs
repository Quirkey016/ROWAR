using UnityEngine;

public class Zoom : MonoBehaviour
{
    private Camera cam;
    private float targetZoom;
    private float zoomVelocity = 0f;
    [SerializeField] private float zoomMultiplier = 4f, smoothTime = 0.15f, minZoom = 2f, maxZoom = 10f;

    void Start() { cam = GetComponent<Camera>(); targetZoom = cam.orthographicSize; }

    void Update()
    {
        targetZoom = Mathf.Clamp(targetZoom - Input.GetAxis("Mouse ScrollWheel") * zoomMultiplier, minZoom, maxZoom);
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, targetZoom, ref zoomVelocity, smoothTime);
    }
}