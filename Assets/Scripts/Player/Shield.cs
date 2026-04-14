using UnityEngine;

namespace Player
{
    public class Shield : MonoBehaviour
    {
        public float size;
        public float health;


        private void Awake()
        {
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, size, gameObject.transform.localScale.z);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Damaging"))
            {
                health--;
            }
        }

        // Update is called once per frame
        void Update()
        {
            DestroyShield();

            var followPoint = GameObject.FindGameObjectWithTag("FollowPoint");
            gameObject.transform.position = followPoint.transform.position;
            gameObject.transform.rotation = followPoint.transform.rotation;
        }

        private void DestroyShield()
        {
            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                Destroy(gameObject);
            }

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
