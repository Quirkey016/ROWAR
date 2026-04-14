using UnityEngine;

namespace Player
{
    public class StaffRotate : MonoBehaviour
    {
        public GameObject staffPivot;
        public Camera cam;
   

        // Update is called once per frame
        //rotates a staff around the player which is where the spells will shoot from
        void Update()
        {
            if (Pause.IsPaused) return;
            var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = staffPivot.transform.position.z;

            var dir = mousePos - staffPivot.transform.position;

            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            staffPivot.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}
