using UnityEngine;
namespace Player
{
    public class Shield : MonoBehaviour
    {
        public float size;
        public float health;

        [Header("Wwise")]
        [SerializeField] private AK.Wwise.Event shieldSummonEvent;
        [SerializeField] private AK.Wwise.Event shieldLoopEvent;
        [SerializeField] private AK.Wwise.Event shieldLoopStopEvent;
        [SerializeField] private AK.Wwise.Event shieldDespawnEvent;
        [SerializeField] private AK.Wwise.Event shieldImpactEvent;

        private void Awake()
        {
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, size, gameObject.transform.localScale.z);
            shieldSummonEvent.Post(gameObject);
            shieldLoopEvent.Post(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy Spell"))
            {
                health--;
                shieldImpactEvent.Post(gameObject);
            }
        }

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
                shieldLoopStopEvent.Post(gameObject);
                shieldDespawnEvent.Post(gameObject);
                Destroy(gameObject);
            }
            if (health <= 0)
            {
                shieldLoopStopEvent.Post(gameObject);
                shieldDespawnEvent.Post(gameObject);
                Destroy(gameObject);
            }
        }
    }
}