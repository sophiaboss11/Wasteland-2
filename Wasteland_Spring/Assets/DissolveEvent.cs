using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Sets a float value in a shader
[RequireComponent(typeof(MeshRenderer))]
public class DissolveEvent : MonoBehaviour 
{
    private MeshRenderer mesh;
    private Material dissolveMat;
    // Shader string
    public string materialProperty = "RenderPerCent";
    // Current value
    private float currentMaterialPropertyAmount= 0.0f;
    // How much the amount increments on trigger
    public float materialPropertyIncrement = 0.01f;

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

        SetMaterialProperty(0.0f);
    }

    private void Reset()
    {
        currentMaterialPropertyAmount = 0.0f;
    }

    public void IncrementMaterialProperty()
    {
        // Increment and clamp dissolveAmount
        currentMaterialPropertyAmount+= materialPropertyIncrement;
        ClampMaterialPropertyAmount();

        // Set the material property
        SetMaterialProperty(currentMaterialPropertyAmount);
    }

    private void SetMaterialProperty(float newAmount)
    {
        dissolveMat.SetFloat(materialProperty, newAmount);
    }

    // Clamp the value of dissolveAmount to 0.0 <= x <= 1.0
    private void ClampMaterialPropertyAmount()
    {
        currentMaterialPropertyAmount = (currentMaterialPropertyAmount > 1.0f) ? 1.0f : currentMaterialPropertyAmount;
        currentMaterialPropertyAmount = (currentMaterialPropertyAmount < 0.0f) ? 0.0f : currentMaterialPropertyAmount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
