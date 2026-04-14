using UnityEngine;
using System.Collections;
using Player;

public class RainStop : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CloudTurret.isRaining = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
