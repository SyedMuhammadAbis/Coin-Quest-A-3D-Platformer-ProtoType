using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private float movementForce = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;

    [SerializeField] AudioSource jumpSound;


    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }


    void Update()
    {    //dazee horizontslinput or verticalinput variables dee
         //or da brackets kay naya horizontal aur verticle unity input system wala components dee
        float horizontalinput = Input.GetAxis("Horizontal");
        float verticalinput = Input.GetAxis("Vertical");

        rb.velocity = new Vector3(horizontalinput * movementForce, rb.velocity.y, verticalinput * movementForce);


        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }

    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        jumpSound.Play();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyHead"))
        {
            Destroy(collision.transform.parent.gameObject);
            Jump();
        }
    }




    // kooza da .1f yo spherefield ya gol field  joree tower defense game kay lakka sangay buildigoonjo
    // yo invisible field wee dee field kay sa kala ground rashee no dee ta da pata walagay gee .
    //da .1f field wala size day. wah kya comments hain hahahaha pakhto kay
    // dee deray tareekay dee box cast sphere cast lkka da aur raycast etc
    //box cast kay moo warna box collider taw karay wa main player na
    //aur dee ka ykooza khpoo kay empty object walagawal.
    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, .1f, ground);
    }
}
