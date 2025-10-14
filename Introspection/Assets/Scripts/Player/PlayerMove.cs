using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private new Rigidbody rigidbody;
    [SerializeField] private float speed;

    public bool locked = false;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!locked)
        {
            float vertical = Input.GetAxisRaw("Vertical");
            float horizontal = Input.GetAxisRaw("Horizontal");
            Vector2 moveInput = new Vector2(horizontal, vertical);

            Vector3 moveDirection = transform.forward * moveInput.y + transform.right * moveInput.x;

            rigidbody.linearVelocity = moveDirection * speed;
        }
    }
}
