using UnityEngine;
using System.Collections;

public class LookTowardTarget : MonoBehaviour
{

    public Transform TargetTra;

    [Range(0, 100)]
    public float rotSmoothing = 50f;
    public enum CamEnum { VirusCam, MedicCam };
    public CamEnum myCamEnum = CamEnum.VirusCam;

    void Start()
    {
        if (myCamEnum == 0)
        {
            TargetTra = GameObject.FindGameObjectWithTag("SmallV").transform;

        }
        if (myCamEnum != 0)
        {
            TargetTra = GameObject.FindGameObjectWithTag("SmallM").transform;


        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Quaternion rot = Quaternion.LookRotation(TargetTra.transform.position - transform.position);
        // rot = new Quaternion(rot.x - , rot.y, -rot.z, rot.w);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, rotSmoothing * Time.deltaTime);
    }
}
