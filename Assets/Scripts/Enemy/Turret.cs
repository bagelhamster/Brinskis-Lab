using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    bool detected;
    GameObject player1;
    public Transform enemy;

    public Transform Gun;

    public float FireRate = 1.3f;
    float oTime;
    public float spread;
    public GameObject gun;
    public RaycastHit raycHit;
    public float range;
    [SerializeField]
    public float damage;
    public LineRenderer lineRenderer;  
    void Start()
    {
        oTime = FireRate;
    }


    void Update()
    {
        if (detected)
        {
            enemy.LookAt(player1.transform);
        }
    }

    private void FixedUpdate()
    {
        if (detected)
        {
            FireRate -= Time.deltaTime;

            if (FireRate < 0)
            {
                ShootPlayer();

                FireRate = oTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            detected = true;

            player1 = other.gameObject;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            detected = false;

            player1 = other.gameObject;
        }
    }
    IEnumerator Linego()
    {
        yield return new WaitForSeconds(0.2f);
        lineRenderer.enabled=false;
    }
    private void ShootPlayer()
        {
             lineRenderer.enabled=true;
             Ray ray = new Ray(gun.transform.position, gun.transform.forward);
                if (Physics.Raycast(ray, out RaycastHit hitInfo, range))
                {
            Debug.DrawRay(gun.transform.position, gun.transform.forward, Color.green, 1);
            lineRenderer.SetPosition(0, gun.transform.position);
            lineRenderer.SetPosition(1, hitInfo.point);

            if (hitInfo.collider.CompareTag("Player"))
                    {
                FPSController.OnTakeDamage(damage);
               StartCoroutine(Linego());
            }
          
        }



    }
    /*{
        float spreadx = Random.Range(-spread, spread);
        float spready = Random.Range(-spread, spread);
        Vector3 direction = gun.transform.forward + new Vector3(spreadx, spready, spreadx);


        if (Physics.Raycast(gun.transform.forward,direction, out raycHit, range))
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;

            Debug.DrawRay(gun.transform.position,forward ,Color.green,1);
            lineRenderer.SetPosition(0, gun.transform.position);
            lineRenderer.SetPosition(1, raycHit.point);

            if (raycHit.collider.CompareTag("Player"))
            {
                FPSController.OnTakeDamage(damage);
                Debug.Log(raycHit.transform.name);

            }

        }*/
    
}
