using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ControllerGrabFireScript : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Behaviour_Pose controllerPose;
    public SteamVR_Action_Boolean grabAction;
    public GameObject controller;
    public GameObject bulletOrigin; 

    private GameObject collidingObject;
    private GameObject objectInHand;

    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }

    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }
        collidingObject = null;
    }

    public void SetCollidingObject(Collider col)
    {
        if (collidingObject || !col.GetComponent<Rigidbody>() || !col.gameObject.CompareTag("Ammo"))
        {
            return;
        }

        collidingObject = col.gameObject;
    }

    private void GrabObject()
    {
        GetComponent<AudioSource>().Play();
        
        objectInHand = collidingObject;
        collidingObject = null;

        var joint = AddFixedJoint();
        objectInHand.transform.forward = controller.transform.forward;
        joint.transform.position = bulletOrigin.transform.position;
        joint.transform.forward = bulletOrigin.transform.forward;
        joint.transform.rotation = bulletOrigin.transform.rotation;

        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();


        //objectInHand.transform.rotation = controller.transform.rotation * new Quaternion(20.0f, 20.0f, 20.0f, 20.0f);


    }

    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 30000;
        fx.breakTorque = 30000;
        return fx;
    }

    private void ReleaseObject()
    {
        if (GetComponent<FixedJoint>())
        {
            objectInHand.tag = "LiveAmmo";
            objectInHand.GetComponent<BallProjectileScript>().ChangeMaterial();
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            objectInHand.GetComponent<Rigidbody>().velocity = controllerPose.GetVelocity();
            objectInHand.GetComponent<Rigidbody>().angularVelocity = controllerPose.GetAngularVelocity();
            objectInHand.GetComponent<Rigidbody>().AddForce(transform.forward * 15.0f, ForceMode.VelocityChange);
            Destroy(objectInHand, 10.0f);
        }

        objectInHand = null;
    }

    void Update()
    {
        if (grabAction.GetLastStateDown(handType))
        {
            if (collidingObject)
            {
                GrabObject();
            }
        }
        if (grabAction.GetLastStateUp(handType))
        {
            if (objectInHand)
            {
                ReleaseObject();
            }
        }
    }
}
