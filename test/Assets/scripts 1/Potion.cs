using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    private luna luna;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("trigger"+collider);
        luna = collider.GetComponent<luna>();
        if (luna != null && luna.Health < luna.MaxHealth)
        {
            luna.ChangHealth(1);
            Destroy(gameObject);
        }
       
    }
}
