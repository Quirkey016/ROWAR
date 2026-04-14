using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Player
{
    public class Attack : MonoBehaviour
    {
        public enum MainSpell
        {
            firebolt,
            DevSpell,
        }

        public enum SecondarySpell
        {
            shield
        }

        public enum potions
        {
            heal
        }

        #region Spells
        public GameObject fireBolt;
        public GameObject devSpell;
        public GameObject shield;
        #endregion


        private Camera cam;
        KeyCode attack = KeyCode.Mouse0;
        KeyCode block = KeyCode.Mouse1;
        KeyCode heal = KeyCode.F;
        public MainSpell mS;
        public SecondarySpell sS;
        public potions p;
        private GameObject currentSpell;
        private GameObject currentSSpell;
        public float mSCooldown = 0.5f;
        public float sSCooldown = 2f;
        public float healSpellNumber = 3f;
        public bool canShoot = true;
        public bool canBlock = true;
        public bool isBlocking = false;
        public bool healSpellAvailable = true;
        public PlayerHealth health;
        [SerializeField] public Transform spellSpawn;
    
        public int healsLeft = 3;

        public GameObject shieldReadyIndicator;
        public GameObject spellReadyIndicator;
  

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            cam = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.F4))
            {
                currentSpell = devSpell;
            }
            
            
            switch (canBlock)
            {
                case true:
                    shieldReadyIndicator.SetActive(true);
                    break;
                case false:
                    shieldReadyIndicator.SetActive(false);
                    break;
            }
            switch (canShoot)
            {
                case true:
                    spellReadyIndicator.SetActive(true);
                    break;
                case false:
                    spellReadyIndicator.SetActive(false);
                    break;
            }


            if (Pause.IsPaused) return;
            if (Input.GetKeyDown(attack) && canShoot && !isBlocking)
            {
                ShootSpell();
            }

            if (Input.GetKeyDown(block) && canBlock)
            {
                isBlocking = true;
                BlockSpell();
            }

            if(isBlocking && Input.GetKeyUp(block))
            {
                isBlocking = false;
            }

            if (Input.GetKeyDown(heal) && healSpellAvailable && healsLeft > 0)
            {
                HealSpell();
                healsLeft--;
            }

            switch (mS)
            {
                case MainSpell.firebolt:
                    currentSpell = fireBolt;
                    break;   
                case MainSpell.DevSpell:
                    currentSpell = devSpell;
                    break;
            }

            switch (sS)
            {
                case SecondarySpell.shield:
                    currentSSpell = shield;
                    break;
            }

            switch (p)
            {
                case potions.heal:
                    break;
            }
        }

        public void ShootSpell()
        {
            Instantiate(currentSpell, spellSpawn.position, spellSpawn.rotation);
            canShoot = false;
            Debug.Log("shots fired");
            StartCoroutine(Cooldown(mSCooldown, () => { canShoot = true; }));
        }


        public void HealSpell()
        {
            health.currentHealth += 35;
        }


        public void BlockSpell()
        {
            Instantiate(currentSSpell, spellSpawn.position, spellSpawn.rotation);
            canBlock = false;
            StartCoroutine(Cooldown(sSCooldown, () => { canBlock = true; }));
        }



        public IEnumerator Cooldown(float i, Action b)
        {
            Debug.Log("reloading");
            yield return new WaitForSeconds(i);
            Debug.Log("waiting");
            Debug.Log("b");
            b?.Invoke();
        }
    }
}
