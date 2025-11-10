using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportStraight : MonoBehaviour
{
    public Transform teleportCircleUI;

    LineRenderer lr;

    Vector3 originScale = Vector3.one * 0.02f;
    // Start is called before the first frame update
    void Start()
    {
        teleportCircleUI.gameObject.SetActive(false);
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ARAVRInput.GetDown(ARAVRInput.Button.HandTrigger, ARAVRInput.Controller.RTouch)){
            lr.enabled = true;
        }
        else if(ARAVRInput.GetUp(ARAVRInput.Button.HandTrigger, ARAVRInput.Controller.RTouch)){
            lr.enabled = false;
            
            if(teleportCircleUI.gameObject.activeSelf){
                GetComponent<CharacterController>().enabled = false;
                transform.position = teleportCircleUI.position + Vector3.up;
                GetComponent<CharacterController>().enabled = true;
            }
            
            teleportCircleUI.gameObject.SetActive(false);

        }


        else if(ARAVRInput.Get(ARAVRInput.Button.HandTrigger, ARAVRInput.Controller.RTouch)){

            Ray ray = new Ray(ARAVRInput.RHandPosition, ARAVRInput.RHandDirection);
            RaycastHit hitinfo;
            int layer = 1 << LayerMask.NameToLayer("Terrain");

            if(Physics.Raycast(ray, out hitinfo, 200, layer)){
                lr.SetPosition(0,ray.origin);
                lr.SetPosition(1,hitinfo.point);
                teleportCircleUI.gameObject.SetActive(true);
                teleportCircleUI.position = hitinfo.point;
                teleportCircleUI.forward = hitinfo.normal;
                teleportCircleUI.localScale = originScale * Mathf.Max(1,hitinfo.distance);
            }

            else{
                lr.SetPosition(0,ray.origin);
                lr.SetPosition(1, ray.origin + ARAVRInput.LHandDirection * 200);
                teleportCircleUI.gameObject.SetActive(false);
            }
        }
    }
}
