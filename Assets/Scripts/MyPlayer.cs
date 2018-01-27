using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityStandardAssets.Cameras;
using System;

public class MyPlayer : MonoBehaviour
{

    public InputDevice Device { get; set; }

    Renderer cachedRenderer;
    Rigidbody m_rigidbody;
    [Range(0, 50)] public float walkspeed = 10f;
    OneAxisInputControl rightStickX;
    OneAxisInputControl rightStickY;

    public enum PlayersEnum { Player1Virus, Player2Medic };
    public PlayersEnum myPlayersEnum = PlayersEnum.Player2Medic;



    public void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        cachedRenderer = GetComponent<Renderer>();

        if (myPlayersEnum == 0)
        {
            SetVirusCam();
            print(this.name + " is Virus");

        }
        if (myPlayersEnum != 0)
        {
            SetMedicCam();
            print(this.name + " is Medic");

        }
    }



    void Update()
    {

        if (Device == null)
        {
            // If no controller set, just make it translucent white.
            cachedRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, 0.2f);
            print("No controller set for " + this.name);
        }
        else
        {


            if (Device.Action1.WasPressed)
            {
                print("hellos ");
            }


            m_rigidbody.AddForce(new Vector3(Device.Direction.X, 0, Device.Direction.Y) * walkspeed);
            //   m_rigidbody.AddForce(Device.Direction.Vector * walkspeed);
            // transform.Rotate(Vector3.down, 500.0f * Time.deltaTime * Device.Direction.X, Space.World);
            // transform.Rotate(Vector3.right, 500.0f * Time.deltaTime * Device.Direction.Y, Space.World);
        }
        transform.forward = new Vector3(Device.Direction.X, 0, Device.Direction.Y);
        Debug.DrawRay(transform.position, transform.forward);
    }


    void SetVirusCam()
    {

        GameObject vCam = GameObject.FindGameObjectWithTag("VCam");
        vCam.GetComponent<AutoCam>().Target = this.transform;
    }

    void SetMedicCam()
    {

        GameObject mCam = GameObject.FindGameObjectWithTag("MCam");
        mCam.GetComponent<AutoCam>().Target = this.transform;
    }
}




