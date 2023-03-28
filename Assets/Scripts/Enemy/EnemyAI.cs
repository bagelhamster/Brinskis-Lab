using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class EnemyAI : MonoBehaviour
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
    public LineRenderer lineRenderer;
    public GameObject gun;

    [SerializeField] private AudioSource enemy = default;
    [SerializeField] private AudioClip[] gunsounds = default;


    private void Update()
    {
        inSight = Physics.CheckSphere(transform.position, sightRange, IsPlayer);
        inAttack = Physics.CheckSphere(transform.position, attackRange, IsPlayer);

        if (!inSight && !inAttack) Patrol();
        if(inSight&&!inAttack)Chase();
        if(inAttack&&inSight) Attack();

    }
    private void Awake()
    {
        player=GameObject.Find("Player").transform;
        agent=GetComponent<NavMeshAgent>();
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
            ShootPlayer();
            alreadyAtacked = true;
            Invoke(nameof(ResetAttack), fireRate);
        }
    }

    IEnumerator Linego()
    {
        yield return new WaitForSeconds(0.2f);
        lineRenderer.enabled = false;
    }
    private void ShootPlayer()
    {
        lineRenderer.enabled = true;
        Ray ray = new Ray(gun.transform.position, gun.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, attackRange))
        {
            lineRenderer.SetPosition(0, gun.transform.position);
            lineRenderer.SetPosition(1, hitInfo.point);
            enemy.PlayOneShot(gunsounds[UnityEngine.Random.Range(0, gunsounds.Length - 1)]);

            if (hitInfo.collider.CompareTag("Player"))
            {
                FPSController.OnTakeDamage(damage);
                StartCoroutine(Linego());
            }

        }



    }
    private void ResetAttack()
    {
        alreadyAtacked = false;
    }
}