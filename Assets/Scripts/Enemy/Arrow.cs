using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FPSController.OnTakeDamage(23);
        }
    }

    void Start()
    {
        StartCoroutine(DestroyArrow());
    }
    IEnumerator DestroyArrow()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
