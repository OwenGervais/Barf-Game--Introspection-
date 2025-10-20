using UnityEngine;

public class DestroyInteract : InteractableObj
{
    private GameObject targetObj;

    public override void Interacted()
    {
        base.Interacted();
        
        if (targetObj != null)
        {
            Destroy(targetObj);
        }
    }
}
