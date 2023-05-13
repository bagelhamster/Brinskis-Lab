using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHealth : MonoBehaviour
{
    public GameObject HealthBar;
    int inter = 0;
    public GameObject boss;
    private void Start()
    {
        HealthBar.SetActive(false);
        boss.GetComponent<EnemyBossAI>().enabled = false;


    }
    private void OnTriggerEnter(Collider other)
    {
        if (inter == 0&&other.CompareTag("Player"))
        {
            HealthBar.SetActive(true);
            boss.GetComponent<EnemyBossAI>().enabled = true;
            inter++;
        }
    }
}
