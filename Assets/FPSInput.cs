using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{
    public float speed = 6.0f;
    public float gravity = -9.8f;

    private CharacterController charController;

    // Start is called before the first frame update
    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        var deltaX = Input.GetAxis("Horizontal") * speed;
        var deltaZ = Input.GetAxis("Vertical") * speed;

        var movement = new Vector3(deltaX, 0, deltaZ);

        // Limit diagonal movement to the same speed as movement along an axis
        movement = Vector3.ClampMagnitude(movement, speed);

        // Apply gravity
        movement.y = gravity;

        // Make movement speed frame-rate independent
        movement *= Time.deltaTime;

        // Transform the movement vector from local to global coordinates
        movement = transform.TransformDirection(movement);

        // Move player by that vector
        charController.Move(movement);
    }
}
