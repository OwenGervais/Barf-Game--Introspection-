using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public float Sensitivity {
    get { return sensitivity; }
    set { sensitivity = value; }
	}
	[Range(0.1f, 9f)][SerializeField] float sensitivity = 2f;
	[Range(0f, 90f)][SerializeField] float yRotationLimit = 88f;

	Vector2 rotation = Vector2.zero;
	public Transform rotationHolder;
	const string xAxis = "Mouse X"; //Strings in direct code generate garbage, storing and re-using them creates no garbage
	const string yAxis = "Mouse Y";

    public bool locked = false;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked; //makes cursor in middle of screen
        Cursor.visible = false; //hides cursor
    }

	private void Update()
	{
        if (!locked)
        {
            rotation.x += Input.GetAxis(xAxis) * sensitivity;
            rotation.y += Input.GetAxis(yAxis) * sensitivity;
            rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);
            var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
            var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

            transform.localRotation = yQuat;
            rotationHolder.localRotation = xQuat;
        }
    }
}
