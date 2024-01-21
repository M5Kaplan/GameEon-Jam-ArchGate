using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float runSpeed;
    [SerializeField] float JumpPower;
    [SerializeField] float coolDown = 1.7f;
    bool canFire = true;


    [SerializeField] GameObject arrow;
    [SerializeField] Transform bow;

    public bool isAlive = true;

    public bool isOpenA = false;
    public bool isOpenB = false;
    public bool isDoorOpened = false;


    Vector2 moveInput;
    Rigidbody2D myRB;
    Animator myAnimator;
    CapsuleCollider2D myBody;
    BoxCollider2D myFeet;

    public GameObject Door;
    private Animator DoorObjectAnimator;

    public GameObject gameOverUI;



    private void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBody = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();

        DoorObjectAnimator = Door.GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isAlive) { return; }
        Run();
        Flip();
        Die();
        DoorOpen();

    }




    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }
        if (value.isPressed && myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myRB.velocity += new Vector2(0f, JumpPower);
            myAnimator.SetTrigger("Jump");
        }
    }

    void OnFire(InputValue value)
    {
        if (!isAlive || !canFire) { return; }

        StartCoroutine(StartFireCooldown());
    }

    IEnumerator StartFireCooldown()
    {
        canFire = false;
        myAnimator.SetTrigger("Attack");
        yield return new WaitForSeconds(1.1f);
        Instantiate(arrow, bow.position, transform.rotation);
        yield return new WaitForSeconds(coolDown);       
        canFire = true;
    }

    void Run()
    {
        if (!isAlive) { return; }
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRB.velocity.y);
        myRB.velocity = playerVelocity;

        bool PlayerSpeed = Mathf.Abs(myRB.velocity.x) > Mathf.Epsilon;
        if (myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myAnimator.SetBool("isRunning", PlayerSpeed);
        }

    }

    void Flip()
    {
        bool PlayerSpeed = Mathf.Abs(myRB.velocity.x) > Mathf.Epsilon;
        if (PlayerSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRB.velocity.x) * 3f, 3f);

        }

    }

    void Die()
    {
        if (myBody.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Die");
            Invoke("Revive", 3f);
        }
    }
    void Revive()
    {
        isAlive = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("ChestA"))
        {
            isOpenA = true;
        }
        if (collision.CompareTag("ChestB"))
        {
            isOpenB = true;
        }
        if (isDoorOpened && collision.CompareTag("Door"))
        {
            gameOverUI.SetActive(true);
        }
    }


    void DoorOpen()
    {
        if (isOpenA && isOpenB)
        {

           
            if (DoorObjectAnimator != null)
            {
                DoorObjectAnimator.SetBool("itemsTaken", true);
                isDoorOpened = true;
            }
        }

    }
}
