using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour

{

    Rigidbody2D playerRB;
    Animator playerAnimator;
    public float moveSpeed = 1f;
    public float jumpSpeed = 1f, jumpFrequncy = 1f, nextJumpTime;

    bool facingRight = true;

    public bool isGrounded = false;
    public Transform GroundPosition;
    public float GroundRadius;
    public LayerMask GroundLayer;

    //yorum sat�rlar� kodun �st�ne yaz�lm��t�r

    //y�z�evirme



    void Awake()

    {



    }



    // Start is called before the first frame update

    void Start()

    {

        playerRB = GetComponent<Rigidbody2D>();

        //animasyon ge�i�leri i�in yaz�lm��t�r

        playerAnimator = GetComponent<Animator>();





    }



    // Update is called once per frame

    void Update()

    {

        //hareket etme kodunu �a��r�r

        HorizontalMove();
        OnGroundCheck();

        if (playerRB.velocity.x < 0 && facingRight)

        {

            FlipFace();

            //y�z �evirme

        }



        else if (playerRB.velocity.x > 0 && !facingRight)

        {

            FlipFace();

            //y�z �evirme

        }

        if (Input.GetAxis("Vertical") > 0 && isGrounded && (nextJumpTime < Time.timeSinceLevelLoad))
        {
            nextJumpTime = Time.timeSinceLevelLoad + jumpFrequncy;
            Jump();
        }
    }



    void FixedUptade()

    {



    }



    void HorizontalMove()

    {

        //hareket etme

        playerRB.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, playerRB.velocity.y);

        //animasyon ge�i�leri i�in yaz�lm��t�r

        playerAnimator.SetFloat("PlayerSpeed", Mathf.Abs(playerRB.velocity.x));

    }



    //y�z �evirme flip face

    void FlipFace()

    {

        facingRight = !facingRight;

        Vector3 tempLocalScale = transform.localScale;

        tempLocalScale.x *= -1;

        transform.localScale = tempLocalScale;

    }

    void Jump()
    {
        playerRB.AddForce(new Vector2(0f, jumpSpeed));
    }
    //karakterin yerde olup olmad��� i�in yaz�lm��t�r
    void OnGroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(GroundPosition.position, GroundRadius, GroundLayer);
        playerAnimator.SetBool("isGroundedanim" , isGrounded);
    }
}