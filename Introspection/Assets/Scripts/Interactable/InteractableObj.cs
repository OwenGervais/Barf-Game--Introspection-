using System;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class InteractableObj : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    
    [SerializeField] private Material unSelected;
    [SerializeField] private Material selected;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Selected()
    {
        meshRenderer.material = selected;
    }

    private void Unselected()
    {
        meshRenderer.material = unSelected;
    }

    public virtual void Interacted()
    {
        Debug.Log("Interacted with " + gameObject.name);
    }
}
