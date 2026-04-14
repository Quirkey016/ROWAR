using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject playerRef;
    public string text;
    public TextMeshProUGUI tutorial;

    private void Start()
    {    
        playerRef = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tutorial.text = text;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
