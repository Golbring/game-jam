using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BossMovement : MonoBehaviour
{

    private FlyingCat catCode;
    private GameObject catClone;
    private Rigidbody2D rb;
    private Animator animator;
    private BoxCollider2D boxCollider;

    private float gravity = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        FindCat();
    }

    private void FindCat()
    {
        catClone = GameObject.Find("Cat Assassin(Clone)");
        catCode = catClone.GetComponent<FlyingCat>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !catCode.cannotKill)
        {
            animator.SetTrigger("deathTrigger");
            rb.gravityScale = gravity;
            boxCollider.enabled = false;
        }
    }
}

