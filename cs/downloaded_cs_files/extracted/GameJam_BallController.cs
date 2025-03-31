using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BallController : MonoBehaviour {
	Rigidbody rb;
    public Vector3 StartVelocity;
    public Paddle startPaddle;
    public Sprite[] catBall;
    public Sprite[] bloodSplatterArray;
    public Image imageSprite;
    public Image bloodSprite;
    private int catAniCount = 0;
    private int bloodAniCount = 0;
    public bool isAttached = false;
    public Vector3 initPosition;
    public bool hasHit = false;
    
	// Use this for initialization
	void Start () {
        initPosition = transform.position;
        //startPaddle.Attach(this);
		bounciness = Mathf.Clamp(bounciness, 0f, 1f);
		rb = GetComponent<Rigidbody>();
	}

    public void Reset()
    {
        //startPaddle.Attach(this);
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.R))
			rb.AddForce (new Vector3 (Random.Range (-3, 3), 0, 0));
        AnimateCat();
    }

    void AnimateCat() {
        imageSprite.sprite = catBall[catAniCount];
        catAniCount++;
        if (catAniCount >= 12) catAniCount = 0;
    }

    IEnumerator AnimateBlood() {
        yield return new WaitForEndOfFrame();
		if (bloodAniCount <= 13) 
        bloodSprite.sprite = bloodSplatterArray[bloodAniCount];
        bloodAniCount++;

        if (bloodAniCount <= 13) 
            StartCoroutine(AnimateBlood());

    }

    public float bounciness;
	private Vector3 lastVelocity;
	
	void FixedUpdate()
	{
		lastVelocity = rb.velocity;
        if (lastVelocity.magnitude < 4.0) lastVelocity = lastVelocity.normalized * 4.0f;
	}

    public Transform paddleTransform;

    void OnCollisionEnter(Collision collision)
	{
        if(collision.transform.tag != null) {
            if (isAttached) return;
            switch (collision.transform.tag) {
                case "RightQFrame":
                    bloodAniCount = 0;
                    StartCoroutine(AnimateBlood());
                    transform.GetComponent<TrailRenderer>().material.SetColor("_TintColor", Color.red);
                    hasHit = true;
                    break;
                case "LeftQFrame":
                    bloodAniCount = 0;
                    StartCoroutine(AnimateBlood());
                    transform.GetComponent<TrailRenderer>().material.SetColor("_TintColor", Color.green);
                    hasHit = true;
                    break;
                default:
                    break;
            }

        }

        Vector3 normal = Vector3.zero;
        Paddle paddle = collision.transform.GetComponent<Paddle>();
        if (paddle != null)
        {
            Vector3 contactPoint = collision.contacts[0].point;
            Vector3 paddlePos = paddle.transform.position;
            paddlePos.x = (float)(paddlePos.x + Mathf.Sign(paddle.transform.position.x) * 0.4);
            Vector3 relPos = contactPoint - paddlePos;
            relPos.z = 0;
            Vector3 relPosNorm = relPos / relPos.magnitude;
            rb.velocity = lastVelocity.magnitude * relPosNorm;
            hasHit = true;
        }

        foreach (ContactPoint c in collision.contacts)
        {
            normal += c.normal;
        }

        normal.Normalize();

        Vector3 inVelocity = lastVelocity;
		
        if (collision.transform.tag != null && collision.transform.tag == "Wall")
        {
            //          outVelocity += Vector3.left * paddle.GetVelocity();
            Vector3 outVelocity = bounciness * (-2f * (Vector3.Dot(inVelocity, normal) * normal) + inVelocity);
            rb.velocity = outVelocity;
            //if (Mathf.Sign(wall.transform.position.y) == 1)
            //{
            //    rb.position = rb.position + Vector3.down * ;
            //}
            //rb.position = rb.position + outVelocity * ((float) 0.2);
            //position = newPos;

        }
        //rb.velocity = outVelocity;

		//rb.velocity = outVelocity;
        if (rb.velocity.x > 0) imageSprite.transform.rotation = Quaternion.Euler(new Vector3(0, -180, 0));
        else if (rb.velocity.x < 0) imageSprite.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }

}
