using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshRenderer))]
public class DissolveEvent : MonoBehaviour 
{
    private MeshRenderer mesh;
    private Material dissolveMat;
    private string materialProperty = "RenderPerCent";
    private float dissolveAmount = 0.0f;
    private float dissolveAmountIncrement = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        if(mesh == null)
        {
            Debug.LogError("Error: mesh null in DissolveEvent");
        }

        dissolveMat = mesh.sharedMaterial;
        if(!dissolveMat.HasProperty(materialProperty))
        {
            Debug.LogError("Error: material assigned to mesh does not have required property" +  materialProperty);
        }
    }

    private void Reset()
    {
        dissolveAmount = 0.0f;
    }

    public void IncrementDissolve()
    {
        // Increment and clamp dissolveAmount
        dissolveAmount += dissolveAmountIncrement;
        ClampDissolve();

        // Set the material property
        dissolveMat.SetFloat(materialProperty, dissolveAmount);
    }

    // Clamp the value of dissolveAmount to 0.0 <= x <= 1.0
    private void ClampDissolve()
    {
        dissolveAmount = (dissolveAmount > 1.0f) ? 1.0f : dissolveAmount;
        dissolveAmount = (dissolveAmount < 0.0f) ? 0.0f : dissolveAmount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
