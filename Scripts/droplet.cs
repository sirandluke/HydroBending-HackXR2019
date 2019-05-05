using OculusSampleFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droplet : MonoBehaviour {

    public static List<droplet> droplets;
    public float speedWeight;
    public float speedBias;
    public float distWeight;
    public float distBias;

    public float distMult;

    public float range;

    public float speed;
    public float defaultSpeed;

    public float antiGrav;
    bool clump;

    // Use this for initialization
    void Start () {
		droplets = GetComponentInParent<dropletManager>().droplets;
    }

    // Update is called once per frame
    private void FixedUpdate() {
        if (GetComponent<DistanceGrabbable>().isGrabbed)
        {
            Attract();
        }
        else
        {
            //Might want to make visible again later?
            gameObject.GetComponent<Renderer>().enabled = true;
            for (int i = 0; i < droplets.Count; i++)
            {
                Rigidbody otherRb = droplets[i].GetComponent<Rigidbody>();
                otherRb.useGravity = true;
            }
        }
    }

    void Attract()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
        //Jellyfish by varying speed droplets gets attracted?
        for (int i = 0; i < droplets.Count; i++)
        {
            
            //Might havce to do checks for stationary droplets (not apply forces to them), including this or droplet in other hand
            Rigidbody otherRb = droplets[i].GetComponent<Rigidbody>();

            Vector3 direction = this.transform.position - droplets[i].transform.position;
            if(direction.magnitude == 0)
            {
                continue;
            }
            float distance = direction.magnitude;
            if (distance < range) {
                otherRb.useGravity = false;
                /*
                if(rightGrab && !leftGrab)
                {
                    speed = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger)*speedWeight + speedBias;
                }
                else if(leftGrab){
                    if (!rightGrab)
                    {
                        speed = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) * speedWeight + speedBias;
                    }
                    else if(OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) * speedWeight + speedBias > speed)
                    {
                        speed = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) * speedWeight + speedBias;
                    }
                }
                */

                //ignores hand orientation??
                float rightVal = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
                float leftVal = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
                if (rightVal > leftVal)
                {
                    speed = speedWeight * rightVal + speedBias;
                    //clump = (rightVal == 1);
                }
                else
                {
                    speed = speedWeight * leftVal + speedBias;
                    //clump = clump || (leftVal == 1);
                }
                //Debug.Log("Speed is: " + speed);

                direction /= direction.magnitude;
                /*
                if (clump)
                {
                    otherRb.AddForce((direction * speed) * distMult / distance);
                }
                else
                {
                */
                    otherRb.AddForce(direction * speed);
                //}
            }
        }
    }
}
