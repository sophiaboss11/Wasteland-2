

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR.InteractionSystem.Sample;

namespace Valve.VR.InteractionSystem.Sample
{
    public class Hose2 : MonoBehaviour
    {
        public SteamVR_Action_Boolean plantAction;

        public Hand hand;

        public GameObject prefabToPlant;
        public Hose hose;

        private bool hoseOn = false;

        private void Start()
        {
           // prefabToPlant.SetActive(false);
        }
        private void OnEnable()
        {
            if (hand == null)
                hand = this.GetComponent<Hand>();

            if (plantAction == null)
            {
                Debug.LogError("<b>[SteamVR Interaction]</b> No plant action assigned", this);
                return;
            }

            plantAction.AddOnChangeListener(OnPlantActionChange, hand.handType);
        }

        private void OnDisable()
        {
            if (plantAction != null)
                plantAction.RemoveOnChangeListener(OnPlantActionChange, hand.handType);
        }

        private void OnPlantActionChange(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources inputSource, bool newValue)
        {
            if (newValue)
            {
                Plant();
            }
        }

        public void Plant()
        {
            Debug.Log("Active");
            if (!hoseOn)
            {
                //            prefabToPlant.SetActive(true);

                hose.FireProjectile();
                //            hoseOn = true;
            }
            else
            {
                //            hoseOn = false;
            }
        }


    }

}
