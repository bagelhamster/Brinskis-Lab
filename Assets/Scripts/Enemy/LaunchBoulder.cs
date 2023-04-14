using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchBoulder : MonoBehaviour
{
    public GameObject boulder;
    float speed = 100f;
    int inter=0;
    public GameObject boulderLaunch;
    private void OnTriggerEnter(Collider other)
    {
        if (inter==0) {
            GameObject shotInstance = Instantiate(boulder,boulderLaunch.transform.position , boulderLaunch.transform.rotation);
            shotInstance.GetComponent<Rigidbody>().AddForce(boulderLaunch.transform.forward*speed);
            inter++;
        }
    }
}
