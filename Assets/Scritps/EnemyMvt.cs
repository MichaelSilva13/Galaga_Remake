using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

public class EnemyMvt : MonoBehaviour
{
    [SerializeField] public Transform finalPosition;
    [SerializeField] private PathCreator mainPath;
    private Rigidbody2D _rigidbody2D;
    private float distTravelled = 0f;
    private const float MARGIN = 0.15f;
    private PathCreator attackPath;
    [SerializeField] public float attackTimer = 3f, shootCooldown = 1f, missileForce = 10f;
    [SerializeField] public float _speed = 5f, attackSpeed = 5f;
    [SerializeField] private int burstAmmount = 3;
    [SerializeField] private int minRow, maxRow;
    private int posIndex = 1;
    [SerializeField] private bool positionned;
    private AudioSource attackSound;
    private Life _life;
    public int maxClones = 20;

    public PathCreator MainPath
    {
        get => mainPath;
        set => mainPath = value;
    }

    private Vector2 _dirrection;
    // Start is called before the first frame update
    public void EnableValues()
    {
        //Transform grid = GameObject.FindWithTag("Grid").transform;
        
        // path.Add(finalPosition);
        distTravelled = 0;
        positionned = false;
        transform.parent = null;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        transform.position = mainPath.path.GetPointAtDistance(0);
        attackPath = GetComponentInChildren<PathCreator>();
        StartCoroutine(Shooting());
        finalPosition = GameObject.FindGameObjectWithTag("Grid").GetComponent<EnemyGrid>().GetAvailableSpot(minRow, maxRow);
        _life = GetComponent<Life>();
        attackSound = GameObject.FindWithTag("EnemySound").GetComponent<AudioSource>();
        // transform.position = path[0].position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Life>().Death();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (!positionned)
        {
            if (distTravelled < mainPath.path.length - 0.5)
            {
                distTravelled += Time.deltaTime * _speed;
                transform.position = mainPath.path.GetPointAtDistance(distTravelled);
                Vector3 next = mainPath.path.GetPointAtDistance(distTravelled + Time.deltaTime * _speed);
                _dirrection = next - transform.position;
                float angle = Mathf.Atan2(_dirrection.y, _dirrection.x) * Mathf.Rad2Deg;
                Quaternion wanted = Quaternion.Euler(new Vector3(0, 0, angle - 90));
                transform.rotation = wanted;
            }
            else
            {
                _dirrection = finalPosition.position - transform.position;
                _rigidbody2D.velocity = _dirrection.normalized * _speed;
                float angle = Mathf.Atan2(_dirrection.y, _dirrection.x) * Mathf.Rad2Deg;
                Quaternion wanted = Quaternion.Euler(new Vector3(0, 0, angle - 90));
                transform.rotation = Quaternion.Lerp(transform.rotation, wanted, Time.deltaTime * 3f);
            }

            if (Math.Abs(_dirrection.magnitude) <= MARGIN)
            {
                Transform attTransform = attackPath.transform;
                attTransform.parent = transform;
                attTransform.position = transform.position;
                attTransform.rotation = Quaternion.Euler(0, 0 ,0);
                positionned = true;
                _rigidbody2D.velocity = Vector2.zero;
                transform.parent = finalPosition;
                transform.position = finalPosition.position;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                StartCoroutine(AttackCoolDown());
            }
        }
        else
        {
            transform.position = finalPosition.position;
        }
    }

    IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(attackTimer);
        attackPath.transform.parent = null;
        attackPath.transform.rotation = Quaternion.Euler(0, 0, 0);
        distTravelled = 0;
        mainPath = attackPath;
        positionned = false;
        transform.parent = null;
        attackSound.Play();
    }

    IEnumerator Shooting()
    {
        while (true)
        {
            for (int i = 0; i < burstAmmount; i++)
            {
                GameObject missile = GameObjectPoolController.Dequeue("EnemyProjectile").gameObject;
                missile.transform.position = transform.position + Vector3.down;
                missile.transform.localScale = new Vector3(1, -1, 1);
                missile.SetActive(true);
                Rigidbody2D missileRb = missile.GetComponent<Rigidbody2D>();
                missileRb.AddForce(Vector2.down * missileForce, ForceMode2D.Impulse);
                yield return new WaitForSeconds(0.2f);
            }
            
            yield return new WaitForSeconds(shootCooldown);
        }
    }

}
