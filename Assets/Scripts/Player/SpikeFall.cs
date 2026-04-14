using Player;
using UnityEngine;

public class SpikeFall : MonoBehaviour
{
    public GameObject playerRefer;
    public int trap = 500;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRefer = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player here");
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                Debug.Log("damage");

                playerHealth.currentHealth -= trap;
            }
        }
    }
}
