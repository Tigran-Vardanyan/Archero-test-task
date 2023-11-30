using UnityEngine;

public class Movement : MonoBehaviour
{
    public FloatingJoystick joystick;
    public float speed = 5f;

    private Rigidbody rb;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Get input from the joystick
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        // Calculate movement direction
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Check if there is any input
        if (movement.magnitude >= 0.1f)
        {
            // Calculate the rotation angle based on the camera's forward direction
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;

            // Smoothly rotate the player towards the movement direction
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            rb.rotation = Quaternion.Euler(0f, angle, 0f);

            // Move the player using Rigidbody
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            rb.velocity = moveDirection.normalized * speed;
        }
        else
        {
            // If there is no input, stop the player
            rb.velocity = Vector3.zero;
        }
    }
}
