using System;
using UnityEngine;
using System.Collections;


namespace Valve.VR.InteractionSystem.Sample
{
    public class Hose : MonoBehaviour
    {

        public Rigidbody projectile;
        public Transform Spawnpoint;
        public KeyCode keyCode;

        public float maxPower = 20;
        public float minPower = 5;
        public float changeSpeed = 5;
        public ParticleSystem[] hoseWaterSystems;
        public Renderer systemRenderer;

        private float m_Power;
        int fireDelay = 0;

        // Update is called once per frame
        private void Update()
        {
            m_Power = Mathf.Lerp(m_Power, Input.GetMouseButton(0) ? maxPower : minPower, Time.deltaTime*changeSpeed);

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                systemRenderer.enabled = !systemRenderer.enabled;
            }

            foreach (var system in hoseWaterSystems)
            {
				ParticleSystem.MainModule mainModule = system.main;
                mainModule.startSpeed = m_Power;
                var emission = system.emission;
                emission.enabled = (m_Power > minPower*1.1f);
            }

            if (Input.GetKey(keyCode))
            {
                FireProjectile();
 
            }




        }

        public void FireProjectile()
        {
            if (fireDelay == 0)
            {
                fireDelay = 1;
                StartCoroutine(FireDelayer(0.1f));
                //fire animation etc.
                Rigidbody clone;
                clone = (Rigidbody)Instantiate(projectile, Spawnpoint.position, projectile.rotation);
                clone.velocity = Spawnpoint.TransformDirection(Vector3.forward * 20);
            }

        }


        IEnumerator FireDelayer(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            fireDelay = 0;
        }



    }
}
