using System.Collections;
using UnityEngine;

namespace Player
{
    public class Spells : MonoBehaviour
    {
        public enum SpellType
        {
            Firebolt,
            EnemySpell,
            DevSpell,
        
        }

        [SerializeField] Rigidbody2D rb;
        public float spellSpeed;
        public float spellDamage; 
        public float spellLifetime;
        public SpellType currentSpell;
    
        private float _leftConstr = Screen.width;
        private float _rightConstr = Screen.width;
        private float _topConstr = Screen.height;
        private float _bottomConstr = Screen.height;
        public float buffer = 0.1f;
        private Camera _mainCam;
        private float _distanceZ;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            StartCoroutine(FireboltLife());
        
            _mainCam = Camera.main;
            if (_mainCam == null) return;
            _distanceZ = Mathf.Abs(_mainCam.transform.position.z + transform.position.z);
            _leftConstr = _mainCam.ScreenToWorldPoint(new Vector3(0f, 0f, _distanceZ)).x;
            _rightConstr = _mainCam.ScreenToWorldPoint(new Vector3(Screen.width, 0f, _distanceZ)).x;
            _bottomConstr = _mainCam.ScreenToWorldPoint(new Vector3(0f, 0f, _distanceZ)).y;
            _topConstr = _mainCam.ScreenToWorldPoint(new Vector3(0f, Screen.height, _distanceZ)).y;
        }





        public void Update()
        {
            switch (currentSpell)
            {
                case SpellType.Firebolt:
                    spellSpeed = 60;
                    spellDamage = 6;
                    break;
                case SpellType.EnemySpell:
                    spellSpeed = 10;
                    spellDamage = 6;
                    break;
                case SpellType.DevSpell:
                    spellSpeed = 50;
                    spellDamage = 50;
                    break;
            }

            rb.AddForce(spellSpeed * Time.deltaTime * transform.right, ForceMode2D.Impulse);
        }


        public IEnumerator FireboltLife()
        {
            yield return new WaitForSeconds(spellLifetime);
            Destroy(gameObject);
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<EnemyHealth>())
            {
                var tempEnemyHealthScript = other.gameObject.GetComponent<EnemyHealth>();
                tempEnemyHealthScript.EnemyTakeDamage(spellDamage);
            }

            if (other.gameObject.GetComponent<PlayerHealth>())
            {
                var tempPlayerHealthScript = other.gameObject.GetComponent<PlayerHealth>();
                tempPlayerHealthScript.TakeDamage((int)spellDamage);
            }
        
            Destroy(gameObject);
        }
    }
}
