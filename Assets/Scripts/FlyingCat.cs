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
    private GameObject mainCam;
    private GameObject knight;
    public Rigidbody2D _rb;
    private CircleCollider2D _circleCollider;
    private Animator anim;
    private AudioManager audioManager;
    
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

    private void Update()
    {
        Debug.Log(cannotKill);
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
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        mainCam = GameObject.Find("Main Camera");
        scene = mainCam.GetComponent<SceneHandler>();
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
        audioManager.stopPlaySFX();
        audioManager.playSFX(audioManager.slingRelease);
        audioManager.playSFX(audioManager.flying);
    }

    public void IncreaseGravityScale(float newGravityScale)
    {
        _rb.gravityScale = newGravityScale;
    }

    private void SuccessSequence()
    {
        transform.rotation = new Quaternion(0, 0, 0, 0);
        anim.SetBool("isIdle", true);
        _hasBeenLaunched = false;
        hasSucceeded = true;
        scene.nextLevel.gameObject.SetActive(true);
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        _shouldFaceVelocityDirectionl = false;

        if (collision.gameObject.tag == "Environment" && !hasSucceeded)
        {
            audioManager.stopPlaySFX();
            audioManager.playSFX(audioManager.landing);
            KillCat();
        }

        else if (collision.gameObject.tag == "Target" && !aiMove.noDie && !cannotKill)
        {
            audioManager.stopPlaySFX();
            audioManager.playSFX(audioManager.hitEnemy);
            SuccessSequence();
        }

        else if (collision.gameObject.tag == "Boss" && !_isDead)
        {
            audioManager.stopPlaySFX();
            audioManager.playSFX(audioManager.hitEnemy);
            SuccessSequence();
        }

        
    }

}
