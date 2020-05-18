using System;
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
    private float lastMaterialPropertyAmount= 0.0f;
    private float currentMaterialPropertyAmount= 0.0f;
    // How much the amount increments on trigger
    public float materialPropertyIncrement = 0.01f;
    public float smoothTime = 0.25f;

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

        // Reset material property
        SetMaterialProperty(0.0f);
    }

    private void Reset()
    {
        currentMaterialPropertyAmount = 0.0f;
    }

    public void IncrementMaterialProperty()
    {
        // Increment and clamp dissolveAmount
        lastMaterialPropertyAmount = currentMaterialPropertyAmount;
        currentMaterialPropertyAmount += materialPropertyIncrement;
        ClampMaterialPropertyAmount();
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
        if(currentMaterialPropertyAmount >= 0.0f && currentMaterialPropertyAmount < 1.0f)
        {
            float velocity = 0.0f;
            // Smoothly interpolate between values
            //            float matPropValue = Mathf.Lerp(currentMaterialPropertyAmount, 1.0f, 0.1f * Time.deltaTime);

            float matPropValue = Mathf.SmoothDamp(currentMaterialPropertyAmount, 1.0f, ref velocity, smoothTime, Time.deltaTime);
            lastMaterialPropertyAmount += velocity;
            SetMaterialProperty(matPropValue);
        }
    }
}
