using UnityEngine;
using System.Collections;
using Player;

public class CloudTurret : MonoBehaviour
{
    public GameObject rain;
    public static bool isRaining;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isRaining = true;
        StartCoroutine(ShootCooldown(Random.Range(0.3f, 0.7f)));  
    }


    private IEnumerator ShootCooldown(float cooldown)
    {
        while (isRaining)
        {
            Instantiate(rain, transform.position, Quaternion.Euler(0, 0, 180));
            yield return new WaitForSeconds(cooldown);
        }
    }
}
