using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class FlyingCat : MonoBehaviour
{
    private PatrolMovement aiMove;
    private SceneHandler scene;
    private GameObject knight;
    public Rigidbody2D _rb;
    private CircleCollider2D _circleCollider;
    private Animator anim;
    
    public bool _hasBeenLaunched;
    private bool _shouldFaceVelocityDirectionl;
    public bool _isDead;
    public bool hasSucceeded;
    public bool cannotKill;

    private void FixedUpdate()
    {
        if (_hasBeenLaunched && _shouldFaceVelocityDirectionl)
        {
            transform.right = _rb.linearVelocity;
        }

    }

    private void KillCat()
    {
        transform.rotation = new Quaternion(0, 0, 0, 0);
        anim.SetBool("isFlying", false);
        _isDead = true;
        cannotKill = true;
        _hasBeenLaunched = false;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _circleCollider = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
        knight = GameObject.Find("EnemyModel");
        aiMove = knight.GetComponent<PatrolMovement>();
        _rb.bodyType = RigidbodyType2D.Kinematic;
        _circleCollider.enabled = false;
        _isDead = false;
        cannotKill = false;
        hasSucceeded = false;
        anim.SetBool("isIdle", true);
        anim.SetBool("isFlying", true);


    }
    public void LaunchCat(Vector2 direction, float force)
    {
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _circleCollider.enabled = true;
        _rb.AddForce(direction * force, ForceMode2D.Impulse);
        _hasBeenLaunched = true;
        _shouldFaceVelocityDirectionl = true;
        anim.SetBool("isIdle", false);

    }

    public void IncreaseGravityScale(float newGravityScale)
    {
        _rb.gravityScale = newGravityScale;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        _shouldFaceVelocityDirectionl = false;

        if (collision.gameObject.tag == "Target" && !aiMove.noDie && !_isDead)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
            anim.SetBool("isIdle", true);
            _hasBeenLaunched = false;
            hasSucceeded = true;
            //play victory animation
            scene.LoadNextScene();
        }

        else if (collision.gameObject.tag == "Environment" && !hasSucceeded)
        {
            KillCat();
        }
    }

}
