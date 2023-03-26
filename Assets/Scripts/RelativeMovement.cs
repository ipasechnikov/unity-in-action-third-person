using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(CharacterController))]
public class RelativeMovement : MonoBehaviour
{
    [SerializeField] Transform target;

    public float rotSpeed = 15.0f;
    public float moveSpeed = 6.0f;

    public float jumpSpeed = 15.0f;
    public float gravity = -9.8f;
    public float terminalVelocity = -10.0f;
    public float minFall = -1.5f;

    private float verSpeed;
    private ControllerColliderHit contact;
    private CharacterController charController;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        verSpeed = minFall;
        charController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var movement = Vector3.zero;

        var horInput = Input.GetAxis("Horizontal");
        var verInput = Input.GetAxis("Vertical");

        if (horInput != 0 || verInput != 0)
        {
            var right = target.right;
            var forward = Vector3.Cross(right, Vector3.up);

            movement = (right * horInput) + (forward * verInput);
            movement *= moveSpeed;
            movement = Vector3.ClampMagnitude(movement, moveSpeed);

            var direction = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);
        }

        var hitGround = false;
        if (verSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out var hit))
        {
            var check = (charController.height + charController.radius) / 1.9f;
            hitGround = hit.distance <= check;
        }

        anim.SetFloat("Speed", movement.sqrMagnitude);

        if (hitGround)
        {
            if (Input.GetButtonDown("Jump"))
                verSpeed = jumpSpeed;
            else
            {
                verSpeed = minFall;
                anim.SetBool("Jumping", false);
            }
        }
        else
        {
            verSpeed += gravity * 5 * Time.deltaTime;
            if (verSpeed < terminalVelocity)
                verSpeed = terminalVelocity;

            if (contact != null)
                anim.SetBool("Jumping", true);

            // Raycast didn't detect the ground, but the capsule is touching the ground
            if (charController.isGrounded)
            {
                if (Vector3.Dot(movement, contact.normal) < 0)
                    movement = contact.normal * moveSpeed;
                else
                    movement += contact.normal * moveSpeed;
            }
        }

        movement.y = verSpeed;
        movement *= Time.deltaTime;
        charController.Move(movement);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        contact = hit;
    }
}
