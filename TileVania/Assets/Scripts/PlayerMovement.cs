using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    SpriteRenderer mySpriteRenderer;
    float gravityStart;
    float dyingVelocity = 5f;
    bool isAlive = true;

    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gravityStart = myRigidBody.gravityScale;
    }

    void Update()
    {
        if (!isAlive){
            return;
        }
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    // the "on something" functions come from the input system
    private void OnMove(InputValue value){
        if (!isAlive){
            return;
        }
        moveInput = value.Get<Vector2>();
    }

    private void OnJump(InputValue value){
        if (!isAlive){
            return;
        }
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){
            return;
        }
        if (value.isPressed){
            myRigidBody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    private void Run(){
        Vector2 playerVelocity = new Vector2(moveInput.x * speed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
        if (Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon){
            myAnimator.SetBool("isRunning", true);
        } else {
            myAnimator.SetBool("isRunning", false);            
        }
    }

    private void FlipSprite(){
        bool playerMoving = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerMoving){
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }

    private void ClimbLadder(){
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"))){
            myRigidBody.gravityScale = gravityStart;
            myAnimator.SetBool("isClimbing", false);            
            return;
        }
        Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, moveInput.y * climbSpeed);
        myRigidBody.velocity = climbVelocity;   
        myRigidBody.gravityScale = 0f;    
        if (Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon){
            myAnimator.SetBool("isClimbing", true);
        } else {
            myAnimator.SetBool("isClimbing", false);            
        }
    }

    private void Die(){
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies"))){
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            mySpriteRenderer.color = Color.red;
            myRigidBody.velocity += new Vector2 (dyingVelocity, dyingVelocity);
        }
    }
}
