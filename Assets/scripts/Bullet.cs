using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject burstEffectPrefab;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with something other than the bullet itself
        if (collision.gameObject != this.gameObject)
        {
            GameObject burstEffect = Instantiate(burstEffectPrefab, transform.position, Quaternion.identity);

            Destroy(this.gameObject);
            Destroy(burstEffect, 2f);
        }
    }
}
