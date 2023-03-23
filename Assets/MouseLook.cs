using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public enum RotationAxes
    {
        MouseX, MouseY, MouseXY
    }

    public RotationAxes axes = RotationAxes.MouseX;
    public float sensitivityHor = 9.0f;
    public float sensitivityVert = 9.0f;

    public float minimumVert = -45.0f;
    public float maximumVert = 45.0f;

    // Current vertical angle
    private float verticalRot = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (axes)
        {
            // Horizontal rotation
            case RotationAxes.MouseX:
                {
                    var mouseX = Input.GetAxis("Mouse X");
                    var horizontalRot = mouseX * sensitivityHor;
                    transform.Rotate(0, horizontalRot, 0);
                    break;
                }
                
            // Vertical rotation
            case RotationAxes.MouseY:
                {
                    var mouseY = Input.GetAxis("Mouse Y");
                    verticalRot -= mouseY * sensitivityVert;
                    verticalRot = Mathf.Clamp(verticalRot, minimumVert, maximumVert);

                    // Keep the same Y angle (i.e., no horizontal rotation)
                    var horizontalRot = transform.localEulerAngles.y;

                    // Create a new rotation from the stored variables
                    transform.localEulerAngles = new Vector3(verticalRot, horizontalRot, 0);

                    break;
                }

            // Both horizontal and vertical rotations
            case RotationAxes.MouseXY:
                {
                    // Calculate horizontal rotation
                    var mouseX = Input.GetAxis("Mouse X");
                    var horizontalRotDelta = mouseX * sensitivityHor;
                    var horizontalRot = transform.localEulerAngles.y + horizontalRotDelta;

                    // Calculate vertical rotation
                    var mouseY = Input.GetAxis("Mouse Y");
                    verticalRot -= mouseY * sensitivityVert;
                    verticalRot = Mathf.Clamp(verticalRot, minimumVert, maximumVert);

                    // Apply both rotations
                    transform.localEulerAngles = new Vector3(verticalRot, horizontalRot, 0);

                    break;
                }

            default:
                throw new NotSupportedException();
        }
    }
}
