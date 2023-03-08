using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SelectGun : MonoBehaviour
{
    public GameObject Gun1;
    public GameObject Gun2;
    public GameObject Gun3;
    public GameObject Gun1Ammo;
    public GameObject Gun2Ammo;
    public GameObject Gun3Ammo;
    public GameObject Gun1Cross;
    public GameObject Gun2Cross;
    public GameObject Gun3Cross;

    void Start()
    {
        Gun1.gameObject.SetActive(true);
        Gun1Ammo.gameObject.SetActive(true);
        Gun2.gameObject.SetActive(false);
        Gun2Ammo.gameObject.SetActive(false);
        Gun3.gameObject.SetActive(false);
        Gun3Ammo.gameObject.SetActive(false);
        Gun1Cross.gameObject.SetActive(true);
        Gun2Cross.gameObject.SetActive(false);
        Gun3Cross.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Slot1"))
        {
            Gun1.gameObject.SetActive(true);
            Gun1Ammo.gameObject.SetActive(true);
            Gun2.gameObject.SetActive(false);
            Gun2Ammo.gameObject.SetActive(false);
            Gun3.gameObject.SetActive(false);
            Gun3Ammo.gameObject.SetActive(false);
            Gun1Cross.gameObject.SetActive(true);
            Gun2Cross.gameObject.SetActive(false);
            Gun3Cross.gameObject.SetActive(false);

        }
        if (Input.GetButton("Slot2"))
        {
            Gun1.gameObject.SetActive(false);
            Gun1Ammo.gameObject.SetActive(false);
            Gun2.gameObject.SetActive(true);
            Gun2Ammo.gameObject.SetActive(true);
            Gun3.gameObject.SetActive(false);
            Gun3Ammo.gameObject.SetActive(false);
            Gun1Cross.gameObject.SetActive(false);
            Gun2Cross.gameObject.SetActive(true);
            Gun3Cross.gameObject.SetActive(false);
        }
        if (Input.GetButton("Slot3"))
        {
            Gun1.gameObject.SetActive(false);
            Gun1Ammo.gameObject.SetActive(false);
            Gun2.gameObject.SetActive(false);
            Gun2Ammo.gameObject.SetActive(false);
            Gun3.gameObject.SetActive(true);
            Gun3Ammo.gameObject.SetActive(true);
            Gun1Cross.gameObject.SetActive(false);
            Gun2Cross.gameObject.SetActive(false);
            Gun3Cross.gameObject.SetActive(true);

        }
    }
}
