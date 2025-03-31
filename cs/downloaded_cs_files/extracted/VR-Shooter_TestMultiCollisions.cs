using UnityEngine;
using System.Collections;

public class TestMultiCollisions : MonoBehaviour
{

    //Collisions
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Grabable")) print("Augoo");
    }

    void OnTriggerStay(Collider other)
    {

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Grabable")) print("Augoo");
    }
}
