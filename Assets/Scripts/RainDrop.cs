using UnityEngine;
using System.Collections;
using Player;

public class RainDrop : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {    
         Destroy(gameObject);
    }
}
