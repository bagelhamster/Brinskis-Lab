using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFixes : MonoBehaviour
{
    public float interval;

    // Update is called once per frame
    void Update()
    {
        if (interval > 0)
        {
            interval -= Time.deltaTime;
        }
        else
        {
            enabled = false;
            Destroy(gameObject);

        }
    }

}
