using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskObject : MonoBehaviour
{
    public GameObject[] maskObj;

    [SerializeField]
    private bool includeChild = false;

    [SerializeField]
    private Material maskMaterial;

    private void Start()
    {
        for (int i = 0; i < maskObj.Length; i++)
        {
            if (includeChild)
            {
                MeshRenderer[] materials = maskObj[i].GetComponentsInChildren<MeshRenderer>();

                foreach (MeshRenderer renderer in materials)
                {
                    renderer.material = maskMaterial;
                    renderer.material.renderQueue = 3002;
                }
            }
            else
            {
                MeshRenderer renderer = GetComponent<MeshRenderer>();

                renderer.material = maskMaterial;
                renderer.material.renderQueue = 3002;
            }
        }
    }
}