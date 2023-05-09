using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawn : MonoBehaviour
{
    public GameObject Self;
    public GameObject Boss;
    void Start()
    {
        
    }

    void Update()
    {
        if(Boss == null)
        {
            Self.SetActive(false);
        }
    }
}
