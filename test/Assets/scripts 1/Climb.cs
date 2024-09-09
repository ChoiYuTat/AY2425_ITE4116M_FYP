using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Climb : MonoBehaviour
{
    public Rigidbody rd;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "luna")
        {
            other.GetComponent<luna>().climb(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "luna")
        {
            other.GetComponent<luna>().climb(false);
            rd.useGravity = true;
            other.GetComponent<luna>().cisclimb(false);
        }
    }
}
