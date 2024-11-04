using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerM : MonoBehaviour
{
    [Header("Config movement")]
    public float moveSpeed = 5f;            
    public float jumpForce = 5f;            
    public float groundCheckDistance = 1.1f;
    public float airControlFact = 0.5f;
    public float sprintMultiply = 2f;

    [Header("Config sensivility")]
    public float mouseSensitivity = 100f;   

    private bool isGrounded;                
    private Rigidbody rb;                   

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);


        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);

        MovePlayer();
    }

    private void MovePlayer()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        float speed = moveSpeed;

        if (moveZ < 0) speed *= 0.5f;       
        else if (moveX != 0) speed *= 0.75f;

        if (isGrounded && Input.GetKey(KeyCode.LeftShift) && moveZ > 0)
        {
            speed *= sprintMultiply;
        }

        if (!isGrounded)
        {
            speed *= airControlFact;
        }

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        rb.MovePosition(transform.position + move.normalized * speed * Time.deltaTime);
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
