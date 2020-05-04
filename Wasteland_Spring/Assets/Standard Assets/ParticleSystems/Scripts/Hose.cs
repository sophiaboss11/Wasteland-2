using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

namespace Valve.VR.InteractionSystem.Sample
{
    public class Hose : MonoBehaviour
    {
        // Object references
        public Rigidbody projectile;
        public Transform Spawnpoint;
        public ParticleSystem[] hoseWaterSystems;
        public Renderer systemRenderer;

        // Toggles system renderer 
        public KeyCode keyCode; 

        // Mouse button for firing hose 
        public MouseButton fireMouseButton = MouseButton.LeftMouse;

        // Hose parameters
        public float maxPower = 20;
        public float minPower = 5;
        public float changeSpeed = 5;
        public float WaitTime = 0.1f; // Spawn Delay between rigidbodies being spawned
        // Current power of hose
        private float m_Power;
        int fireDelay = 0;
        // True if the button that controls the hose is pressed, false otherwise
        private bool hoseKeyPressed = false;


        // Update is called once per frame
        private void Update()
        {
            // Mouse button is pressed?
            bool mouseButtonPressed = FireMouseButtonIsPressed();

            // Set power of hose
            m_Power = UpdateHosePower(hoseKeyPressed || mouseButtonPressed);

            // Draw debug particles if '1' key is pressed
            if (Input.GetKeyDown(keyCode))
            {
                systemRenderer.enabled = !systemRenderer.enabled;
            }
            
            // Set particle system properties 
            foreach (var system in hoseWaterSystems)
            {
				ParticleSystem.MainModule mainModule = system.main;
                mainModule.startSpeed = m_Power;
                var emission = system.emission;
                emission.enabled = (m_Power > minPower*1.1f);
            }

            // If hose key OR mouse button is pressed, fire hose 
            if(hoseKeyPressed || mouseButtonPressed)
            {
                FireProjectile();
            }

        }

        public void FireProjectile()
        {
            // If hose not active, don't fire!
            if (!this.isActiveAndEnabled)
            {
                return;
            }

            // Delay condition
            if (fireDelay == 0)
            {
                fireDelay = 1;

                // Wait before firing again
                StartCoroutine(FireDelayer(WaitTime));

                // Instantiate a new Rigidbody object and fire it
                Rigidbody clone;
                clone = (Rigidbody)Instantiate(projectile, Spawnpoint.position, projectile.rotation);
                clone.velocity = Spawnpoint.TransformDirection(Vector3.forward * 20);

            }

        }

        // Interface method for HoseVRInputModule
        public void SetHoseButtonPressed(bool hoseOn)
        {
            hoseKeyPressed = hoseOn;
        }

        // Returns current state of mouse button for 2d debug control
        private bool FireMouseButtonIsPressed()
        {
            return Input.GetMouseButton((int)fireMouseButton);
        }

        // Sets power of hose based upon how long the hose key has been pressed
        private float UpdateHosePower(bool hoseOn)
        {
            // bool hoseIsOn = Input.GetMouseButton(0);
            return Mathf.Lerp(m_Power, hoseOn ? maxPower : minPower, Time.deltaTime*changeSpeed);
        }

        // Delays next hose fire by waitTime
        IEnumerator FireDelayer(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            fireDelay = 0;
        }




    }
}
