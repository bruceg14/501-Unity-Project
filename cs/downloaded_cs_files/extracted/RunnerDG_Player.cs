using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // Bools
    public bool onGround;
    private bool _isSliding;
    public bool isInvulnerable;
    [HideInInspector]public bool isShieldActive;
    public bool canJump = true;
    public bool canSlide = true;
    

    // GameObjects
    public GameObject shield;

    // Floats
    public float speedModifier;

    // RigidBodys
    private Rigidbody _rb;

    // BoxCollider
    //public BoxCollider playerCollider;
    public SphereCollider playerCollider;

    // Layers
    public LayerMask platformLayer;

    // Enum
    private enum _Swipe { None, Up, Down, Left, Right };

    private static _Swipe swipeDirection;

    // Sprite
    public SpriteRenderer spriteRenderer;
    public Sprite speedDown;
    public Sprite speedUp;
    public Sprite none;

    private static Player _instance;

    public static Player instance
    {
        get { return _instance; }
    }

    void Start()
    {
        _instance = this;
        //_verticalTargetPosition = transform.position;
        _rb = GetComponent<Rigidbody>();
        //_playerCollider = GetComponent<BoxCollider>();

    }

    void Update()
    {
        if (GameCanvas.instance.isGamePaused || Time.timeScale < 0.9f)
        {
            return;
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {             
                transform.position = new Vector3(Mathf.Clamp(transform.position.x + touch.deltaPosition.x / speedModifier, -1.85f, 1.85f),
                    transform.position.y, transform.position.z);

                if (transform.position.y + touch.deltaPosition.y > 33.5f && canJump && !onGround)
                {
                    RotatePlayer.instance.JumpAnim();
                    canJump = false;
                    _rb.AddForce(0, 1500f * Time.fixedDeltaTime, 0, ForceMode.Impulse);
                    Invoke("CanJumpAgain", 0.1f);
                }
                else if (transform.position.y + touch.deltaPosition.y < -33.5f && canSlide)
                {
                    if (!onGround && !_isSliding)
                    {
                        transform.localScale /= 1.5f;
                        _isSliding = true;
                        StartCoroutine(Sliding());
                        Invoke("CanSlideAgain", 0.25f);
                    }
                    else
                    {
                        _rb.AddForce(0, -1000f * Time.fixedDeltaTime, 0, ForceMode.Impulse);
                        Invoke("CanSlideAgain", 0.1f);
                    }
                    canSlide = false;
                }
            }
        }

        // works this way for some reason, TO FIX LATER
        float __extraHeight = 100f;
        onGround = Physics.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, Vector3.down, 
            Quaternion.identity, __extraHeight, platformLayer);

        if (Input.GetKeyDown(KeyCode.I))
        {
            ChangeVulnerability();
        }

        if (Input.GetKey(KeyCode.A) && transform.position.x >= -1.8)
        {
            transform.position = new Vector3(transform.position.x - 0.10f, transform.position.y, transform.position.z);
        }

        if (Input.GetKey(KeyCode.D) && transform.position.x <= 1.8)
        {
            transform.position = new Vector3(transform.position.x + 0.10f, transform.position.y, transform.position.z);
        }


        if (Input.GetKeyDown(KeyCode.S))
        {
            if (!onGround && !_isSliding)
            {
                transform.localScale /= 1.5f;
                _isSliding = true;
                StartCoroutine(Sliding());
            }
            else
            {
                _rb.AddForce(0, -1000f * Time.fixedDeltaTime, 0, ForceMode.Impulse);
            }
        }

        // works this way for some reason, TO FIX LATER
        if (Input.GetKeyDown(KeyCode.Space) && !onGround || Input.GetKeyDown(KeyCode.W) && !onGround)
        {
            _rb.AddForce(0, 1500f * Time.fixedDeltaTime, 0, ForceMode.Impulse);
            RotatePlayer.instance.JumpAnim();
        }

    }

    void FixedUpdate()
    {

        
    }

    private IEnumerator Sliding()
    {
        yield return new WaitForSeconds(0.6f);
        transform.localScale *= 1.5f;
        _isSliding = false;
    }

    public void ActiveShield()
    {
        isShieldActive = true;
        shield.SetActive(true);
    }

    public void DeActiveShield()
    {
        isShieldActive = false;
        shield.SetActive(false);
    }

    public void ChangeVulnerability()
    {
        isInvulnerable = !isInvulnerable;
    }

    public void CanJumpAgain()
    {
        canJump = true;
    }

    public void CanSlideAgain()
    {
        canSlide = true;
    }
}
