using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float speed = 5;
    CharacterController cc;

    public float gravity = -10;
    float yvelocity = 0;
    public float jumpPower = 5;
    int jumpis = 0;


    // Start is called before the first frame update
    void Start()
    {
    cc = GetComponent<CharacterController>();    
    }

    // Update is called once per frame
    void Update()
    {
        float h= ARAVRInput.GetAxis("Horizontal");
        float v= ARAVRInput.GetAxis("Vertical");
        Vector3  dir = new Vector3(h,0,v);


        yvelocity += gravity * Time.deltaTime;



        if(cc.isGrounded){
            yvelocity = 0;
            jumpis = 0;
        }
                dir.y = yvelocity;
        cc.Move(dir * speed * Time.deltaTime);


        if(jumpis == 0 & ARAVRInput.GetDown(ARAVRInput.Button.Two, ARAVRInput.Controller.RTouch)){
            jumpis = 1;
            yvelocity = jumpPower;
            Debug.Log(jumpis);
        }
    }

}
