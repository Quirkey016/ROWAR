using System.Collections;
using Player;
using UnityEngine;

namespace Enemies
{
    
    enum EnemyType
    {
    Melee = 0,
    Ranged = 1,
    }
    public class EnemyAttacks : MonoBehaviour
    {
        private EnemyType _enemyType;
        public int damage = 15;
        public GameObject playerRef;
        public float sight = 5f;
        public GameObject gun;
        public GameObject projectile;
        private bool _canFire = true;
        public float attackSpeed = 2.5f;
        [SerializeField] private Animator animator;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Debug.Log("Player here");
                animator.SetBool("Punch", true);
                PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

                if (playerHealth != null)
                {
                    Debug.Log("damage");

                    playerHealth.currentHealth -= damage;
                }
            }
        }

       

        private void Start()
        {
            playerRef = GameObject.FindGameObjectWithTag("Player");
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (gun && !Pause.IsPaused)
            {
                ShootPlayer();
            }
        }
        
        private void ShootPlayer()
        {
            var posThis = transform.position;
            var posPlayer = playerRef.transform.position;


            
            var distance = Vector2.Distance(posThis, posPlayer);
            Vector3 direction = posPlayer - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            
            if (!(distance <= sight )) return;
            gun.transform.rotation = Quaternion.Euler(0.0f, 0.0f, transform.rotation.eulerAngles.z + angle);
            if (!_canFire) return;
            animator.SetBool("Throw", true);
            Instantiate(projectile, transform.position, gun.transform.rotation);
            _canFire = false;
            StartCoroutine(ShootCooldown(attackSpeed)); //this number changes the rate of fire on the enemies
        }

        private IEnumerator ShootCooldown(float cooldown)
        {
            yield return new WaitForSeconds(cooldown);
            _canFire = true;
        }
        
    }
}
