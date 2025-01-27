using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlyingCat : MonoBehaviour
{
    public Rigidbody2D _rb;
    private CircleCollider2D _circleCollider;
    private Animator anim;
    private bool _hasBeenLaunched;
    private bool _shouldFaceVelocityDirectionl;
    public bool _isDead;
    


    private void FixedUpdate()
    {
        if (_hasBeenLaunched && _shouldFaceVelocityDirectionl)
        {
            transform.right = _rb.linearVelocity;
        }

    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _circleCollider = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
        _rb.bodyType = RigidbodyType2D.Kinematic;
        _circleCollider.enabled = false;
        _isDead = false;
        
        
    }
    public void LaunchCat(Vector2 direction, float force)
    {
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _circleCollider.enabled = true;
        _rb.AddForce(direction * force, ForceMode2D.Impulse);
        _hasBeenLaunched = true;
        _shouldFaceVelocityDirectionl = true;
        anim.SetBool("isFlying", true);

    }

    public void IncreaseGravityScale(float newGravityScale)
    {
        _rb.gravityScale = newGravityScale;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        _shouldFaceVelocityDirectionl = false;

        if (collision.gameObject.tag == "Environment")
        {
            anim.SetBool("isFlying", false);
            _isDead = true;
        }
    }

}
