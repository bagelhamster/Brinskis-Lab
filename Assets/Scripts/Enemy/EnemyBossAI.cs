using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;
using UnityEngine.UI;

public class EnemyBossAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask IsGround, IsPlayer;

    public Vector3 walkPoint;
    bool pointSet;
    public float pointRange;

    public float fireRate;
    bool alreadyAtacked;

    public float sightRange, attackRange;
    public bool inSight, inAttack;

    [SerializeField]
    public float damage;

    [SerializeField]
    public float lightninDamage;
    public LineRenderer lineRenderer;
    public GameObject gun;

    [SerializeField] private AudioSource enemy = default;
    [SerializeField] private AudioClip[] gunsounds = default;

    private Vector3 positionish;

    public GameObject boulder;
    public GameObject arrow;
    public float speed = 100f;
    public float arrowSpeed = 200f;
    //[SerializeField] private Image HealthBar;


    private void Update()
    {
        inSight = Physics.CheckSphere(transform.position, sightRange, IsPlayer);
        inAttack = Physics.CheckSphere(transform.position, attackRange, IsPlayer);
        InvokeRepeating("posit", 0, 1);
        if (!inSight && !inAttack) Patrol();
        if(inSight&&!inAttack)Chase();
        if(inAttack&&inSight) Attack();
        if (agent.transform.position == positionish)pointSet=false;



    }
    private void Awake()
    {
        player=GameObject.Find("Player").transform;
        agent=GetComponent<NavMeshAgent>();
    }
    private void posit()
    {
        positionish = agent.transform.position;
    }
    private void Patrol()
    {
        if (!pointSet) GetWalkPoint();
        if(pointSet)agent.SetDestination(walkPoint);
        Vector3 distance=transform.position-walkPoint;
        if(distance.magnitude <1f)pointSet=false;
    }
    private void GetWalkPoint()
    {
        float z = Random.Range(-pointRange, pointRange);
        float x = Random.Range(-pointRange, pointRange);
        walkPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);
        if (Physics.Raycast(walkPoint, -transform.up, 4f, IsGround)) pointSet = true;
    }
    private void Chase()
    {
        agent.SetDestination(player.position);
    }
    private void Attack()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAtacked)
        {
            
            int randomAttack=Random.Range(1, 100);

            if (randomAttack <= 33)
            {
                ThrowRock();
                alreadyAtacked = true;
                Invoke(nameof(ResetAttack), fireRate);
            }
            if (randomAttack <= 66 && randomAttack > 33)
            {
                Lightning();
                alreadyAtacked = true;
                Invoke(nameof(ResetAttack), fireRate);
            }
            if (randomAttack <= 100 && randomAttack > 66)
            {
                ShootArrow();
                alreadyAtacked = true;
                Invoke(nameof(ResetAttack), fireRate);
            }

            Debug.Log(randomAttack);
        }
    }

    IEnumerator Linego()
    {
        yield return new WaitForSeconds(0.2f);
        lineRenderer.enabled = false;
    }

    IEnumerator lightnin()
    {
        Vector3 lightningcast=new Vector3(player.position.x,50, player.position.z);

        yield return new WaitForSeconds(0.5f);
        lineRenderer.enabled = true;

        Ray rayc = new Ray(lightningcast, -Vector3.up);
        if (Physics.Raycast(rayc, out RaycastHit hitcInfo, attackRange))
        {
            lineRenderer.SetColors(Color.yellow, Color.yellow);
            lineRenderer.SetPosition(0, lightningcast);
            lineRenderer.SetPosition(1, hitcInfo.point);
            enemy.PlayOneShot(gunsounds[Random.Range(0, gunsounds.Length - 1)]);
            Debug.DrawLine(lightningcast, hitcInfo.point, Color.cyan);
            if (hitcInfo.collider.CompareTag("Player"))
            {
                FPSController.OnTakeDamage(lightninDamage);
           
            }
            yield return new WaitForSeconds(0.2f);
            lineRenderer.enabled = false;
        }
    }

    /*private void ShootPlayer()
    {
        lineRenderer.enabled = true;
        Ray ray = new Ray(gun.transform.position, gun.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, attackRange))
        {
            lineRenderer.SetColors(Color.red,Color.red);
            lineRenderer.SetPosition(0, gun.transform.position);
            lineRenderer.SetPosition(1, hitInfo.point);
            enemy.PlayOneShot(gunsounds[Random.Range(0, gunsounds.Length - 1)]);

            if (hitInfo.collider.CompareTag("Player"))
            {
                FPSController.OnTakeDamage(damage);
                StartCoroutine(Linego());
            }

        }



    }*/
    private void ThrowRock()
    {
        GameObject shotInstance = Instantiate(boulder,gun.transform.position, gun.transform.rotation);
        shotInstance.GetComponent<Rigidbody>().AddForce(gun.transform.forward * speed);
    }
    private void ShootArrow()
    {
        GameObject shotInstance = Instantiate(arrow, gun.transform.position, gun.transform.rotation);
        shotInstance.GetComponent<Rigidbody>().AddForce(gun.transform.forward * arrowSpeed);
    }
    private void Lightning()
    {
        StartCoroutine(lightnin());
        StartCoroutine(Linego());
    }
    private void ResetAttack()
    {
        alreadyAtacked = false;
    }
}
