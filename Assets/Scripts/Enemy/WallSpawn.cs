using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawn : MonoBehaviour
{
    public GameObject Self;
    public GameObject Boss;
    public GameObject HealthBar;
    void Start()
    {
        
    }

    void Update()
    {
        if(Boss == null)
        {
            HealthBar.SetActive(false);
            Self.SetActive(false);
        }
    }
}
