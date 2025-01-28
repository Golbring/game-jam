using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    private SlingShotHandler slingCode;
    private FlyingCat catCode;
    public GameObject catClone;
    public GameObject slingshot;
    public CinemachineTargetGroup targetGroup;
    public CinemachineCamera followCam;
    public CinemachineCamera panningCam;

    public Vector3 pannSpeed;
    private float idleTimer;

    private void Start()
    {
        followCam.Priority = 1;
        panningCam.Priority = 0;
        catCode = catClone.GetComponent<FlyingCat>();
        slingCode = slingshot.GetComponent<SlingShotHandler>();
    }

    void Update()
    { 
        FollowCat();

        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && !catCode._hasBeenLaunched)
        {
            panningCam.Priority = 2;
            MovePanCam();
        }
        
        if (Input.GetKeyDown(KeyCode.R) || slingCode._drawSling)
        {
            panningCam.Priority = 0;
            panningCam.transform.position = followCam.transform.position;
        }
    }

    private void FollowCat()
    {
        catClone = GameObject.Find("Cat Assassin(Clone)");
        catCode = catClone.GetComponent<FlyingCat>();
        targetGroup.AddMember(catClone.transform, 1f, 0f);
    }

    void MovePanCam()
    {
        if (Input.GetKey(KeyCode.A))
        {
            panningCam.transform.position -= pannSpeed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            panningCam.transform.position += pannSpeed;
        }
    }
}
