using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class LaserPointer : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Behaviour_Pose controllerPose;
    public SteamVR_Action_Boolean teleportAction;

    public GameObject laserPrefab;
    private GameObject laser;
    private Transform laserTransform;
    private Vector3 hitPoint;

    // 1
    public Transform cameraRigTransform;
    // 2
    public GameObject teleportReticlePrefab;
    // 3
    private GameObject reticle;
    // 4
    private Transform teleportReticleTransform;
    // 5
    public Transform headTransform;
    // 6
    public Vector3 teleportReticleOffset;
    // 7
    public LayerMask teleportMask;
    // 8
    private bool shouldTeleport;


    private void ShowLaser(RaycastHit hit)
    {
        // 1
        laser.SetActive(true);
        // 2
        laserTransform.position = Vector3.Lerp(controllerPose.transform.position, hitPoint, .5f);
        // 3
        laserTransform.LookAt(hitPoint);
        // 4
        laserTransform.localScale = new Vector3(laserTransform.localScale.x,
                                                laserTransform.localScale.y,
                                                hit.distance);
    }

    private void Teleport()
    {
        // 1
        shouldTeleport = false;
        // 2
        reticle.SetActive(false);
        // 3
        Vector3 difference = cameraRigTransform.position - headTransform.position;
        // 4
        difference.y = 0;
        // 5
        cameraRigTransform.position = hitPoint + difference;
    }



    public void Start()
    {
        laser = Instantiate(laserPrefab);
        laserTransform = laser.transform;
        // 1
        reticle = Instantiate(teleportReticlePrefab);
        // 2
        teleportReticleTransform = reticle.transform;


    }

    public void Update()
    {
        if (teleportAction.GetState(handType))
        {
            RaycastHit hit;

            if (Physics.Raycast(controllerPose.transform.position, transform.forward, out hit, 10, teleportMask))
            {
                hitPoint = hit.point;
                ShowLaser(hit);
                // 1
                reticle.SetActive(true);
                // 2
                teleportReticleTransform.position = hitPoint + teleportReticleOffset;
                // 3
                shouldTeleport = true;

            }
        }
        else
        {
            laser.SetActive(false);
            reticle.SetActive(false);

        }

        if (teleportAction.GetStateUp(handType) && shouldTeleport)
        {
            Teleport();
        }

    }

}
