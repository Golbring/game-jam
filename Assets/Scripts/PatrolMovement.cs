using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PatrolMovement : MonoBehaviour
{

    private FlyingCat catCode;
    private GameObject catClone;
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Animator animator;
    private Transform currentPoint;
    private BoxCollider2D boxCollider; 

    public float speed;
    private float walkingSpeed = 2;
    private float dodgeTimer = 0;
    private float idleTimer = 0;
    private float gravity = 0;
    private bool isDead;
    public bool noDie;
    private bool isIdling;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        currentPoint = pointB.transform;
        animator.SetBool("isRunning", true);
        isDead = false;
    }

    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        dodgeTimer += Time.deltaTime;
        idleTimer += Time.deltaTime;
        FindCat();

        if (isDead == false)
        {
            MoveDirection();
            SetDirectionPoint();

            if (dodgeTimer > 5 && !noDie)
            {
                StartCoroutine(DodgeRoll());
            }

            if (idleTimer > 11 && !isIdling)
            {
                animator.SetBool("isIdle", true);
                StartCoroutine(IdleAnimation());
            }
        }
        else { return; }
    }

    private void FindCat()
    {
        catClone = GameObject.Find("Cat Assassin(Clone)");
        catCode = catClone.GetComponent<FlyingCat>();
    }

    private void SetDirectionPoint()
    {
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            Flip();
            currentPoint = pointA.transform;
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            Flip();
            currentPoint = pointB.transform;
        }
    }

    private void MoveDirection()
    {
        if (currentPoint == pointB.transform)
        {
            rb.linearVelocity = new Vector2(speed, 0);
        }
        else
        {
            rb.linearVelocity = new Vector2(-speed, 0);
        }
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }

    private IEnumerator DodgeRoll()
    {
        noDie = true;
        speed = 0;
        animator.SetBool("isDodging", true);
        yield return new WaitForSeconds(2);
        animator.SetBool("isDodging", false);
        speed = walkingSpeed;
        dodgeTimer = 0;
        noDie = false;
    }

    private IEnumerator IdleAnimation()
    {
        isIdling = true;
        speed = 0;
        yield return new WaitForSeconds(3);
        speed = walkingSpeed;
        animator.SetBool("isIdle", false);
        idleTimer = 0;
        dodgeTimer = 0;
        isIdling = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !noDie && !catCode.cannotKill)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            animator.SetTrigger("deathTrigger");
            isDead = true;
            rb.gravityScale = gravity;
            boxCollider.enabled = false;
        }
    }
}
