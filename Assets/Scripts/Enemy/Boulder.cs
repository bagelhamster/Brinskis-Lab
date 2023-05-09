using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
     void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FPSController.OnTakeDamage(50);
        }
    }
    void Start()
    {
        StartCoroutine(DestroyBoulder());
    }
    IEnumerator DestroyBoulder()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
