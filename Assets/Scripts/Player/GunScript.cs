using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunScript : MonoBehaviour
{
    public int damage;
    public float fireRate,spread,range,reloadTime,shotGap;
    public int magSize, bulletsPerShot;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;
    bool shooting, reloading, canShoot;
    public Camera cam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;
    public GameObject muzzleFlash;
    public GameObject bulletHoleGraphic;

    public TextMeshProUGUI text;


    private void Awake()
    {
        bulletsLeft=magSize;
        canShoot = true;
    }
    private void Update()
    {
        MyInput();
        text.SetText(bulletsLeft/bulletsPerShot + " / " + magSize/bulletsPerShot);

    }
    private void MyInput()
    {
        if(allowButtonHold)shooting = Input.GetButton("Fire");
        else shooting = Input.GetButtonDown("Fire");

        if (Input.GetButtonDown("Reload") && bulletsLeft < magSize && !reloading) Reload();

        if (canShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerShot;
            Shoot();
        }
    }


    private void Reload()
    {
        reloading = true;
        Invoke("ReloadDone", reloadTime);
    }
    private void ReloadDone()
    {
        bulletsLeft = magSize;
        reloading = false;
    }

    private void Shoot()
    {
        canShoot = false;
        float spreadx = Random.Range(-spread, spread);
        float spready = Random.Range(-spread, spread);
        Vector3 direction = cam.transform.forward + new Vector3(spreadx, spready, 0);


        if (Physics.Raycast(cam.transform.position, direction, out rayHit, range))
        {
            if (rayHit.collider.CompareTag("Enemy"))
            {
                //Make the enemy take damage of some kind
                Debug.Log(rayHit.transform.name);
               //Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));

            }
                    Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));

        }
        //Adds bullet holes if wanted not sure yet
        //Destroy(BulletHole,2f);
        Instantiate(muzzleFlash, attackPoint.position,attackPoint.rotation,attackPoint.parent);
        //muzzleFlash.transform.parent = attackPoint.transform;
        bulletsLeft--;
        bulletsShot--;
        Invoke("ResetShot", shotGap);
        if(bulletsShot>0&&bulletsLeft>0)
        Invoke("Shoot", shotGap);
    }
    private void ResetShot()
    {
        canShoot = true;
    }
}
