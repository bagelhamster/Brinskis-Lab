using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHealth : MonoBehaviour
{
    public GameObject HealthBar;
    int inter = 0;
    private void Start()
    {
        HealthBar.SetActive(false);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (inter == 0&&other.CompareTag("Player"))
        {
            HealthBar.SetActive(true);
            inter++;
        }
    }
}
