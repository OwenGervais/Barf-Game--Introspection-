using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Transform orientation;
    [SerializeField] private float range;
    private RaycastHit objectHit;
    private GameObject lastObject;

    private void Update()
    {
        Vector3 direction = orientation.TransformDirection(Vector3.forward);
        Debug.DrawRay(orientation.position, direction * range, Color.green);
        if (Physics.Raycast(orientation.position, direction, out objectHit, range))
        {
            if (objectHit.transform.CompareTag("Interactable"))
            {
                if (objectHit.transform.gameObject != lastObject)
                {
                    if (lastObject != null)
                    {
                        lastObject.SendMessage("Unselected");
                    } 
                    lastObject = objectHit.transform.gameObject;
                    objectHit.transform.gameObject.SendMessage("Selected");
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    objectHit.transform.gameObject.SendMessage("Interacted");
                }
            }
        }
        else if (lastObject != null)
        {
            lastObject.SendMessage("Unselected");
            lastObject = null;
        } 
    }
}
