using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelativeMovement : MonoBehaviour
{
    [SerializeField] Transform target;

    public float rotSpeed = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        
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

            var direction = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);
        }
    }
}
