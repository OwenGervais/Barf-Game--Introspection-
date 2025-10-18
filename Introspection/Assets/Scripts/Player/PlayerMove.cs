using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField] private float speed;

    public bool locked = false;

    private float gravity = 0f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!locked)
        {
            float vertical = Input.GetAxisRaw("Vertical");
            float horizontal = Input.GetAxisRaw("Horizontal");
            Vector2 moveInput = new Vector2(horizontal, vertical);

            Vector3 moveDirection = transform.forward * moveInput.y + transform.right * moveInput.x;

            gravity += Physics.gravity.y * Time.deltaTime;
            if (controller.isGrounded) gravity = 0;

            float magnitude = Mathf.Clamp01(moveDirection.magnitude) * speed;
            moveDirection.Normalize();

            Vector3 velocity = moveDirection * magnitude;
            velocity = AdjustVelocityToSlope(velocity);
            velocity.y += gravity;

            controller.Move(velocity * Time.deltaTime);
        }
    }

    private Vector3 AdjustVelocityToSlope(Vector3 velocity)
    {
        var ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 0.2f))
        {
            var slopeRotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            var adjustedVelocity = slopeRotation * velocity;

            if (adjustedVelocity.y < 0)
            {
                return adjustedVelocity;
            }
        }

        return velocity;
    }
}
