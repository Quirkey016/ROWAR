using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public float maxHealth = 50f;
        public float currentHealth = 50f;
        [SerializeField] private Slider healthBar;
        public bool isInvincible = false;
        private SpriteRenderer sr;
        public GameObject deathScreen;
        public TextMeshProUGUI heals;
        public int healsLeft;
        public Attack attack;

        private void Start()
        {
            sr = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
            if (currentHealth <= 0f) {Die();}

            healsLeft = attack.healsLeft;
            if (healsLeft > -1)
            {
                heals.text = "heals:" + healsLeft;
            }
        }

        public void TakeDamage(int amount)
        {
            if (!isInvincible)
            {
                currentHealth -= amount;

                if (currentHealth <= 0)
                {
                    Die();
                }
            }
        }

        private void Die()
        {
            Time.timeScale = 0f;
            Pause.IsPaused = true;
            deathScreen.SetActive(true);
        }
    }
}
