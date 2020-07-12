using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesAfter : MonoBehaviour
{
    public float DesTime;
    void Start()
    {
        Invoke("Des", DesTime);
    }

    void Des()
    {
        Destroy(gameObject);
    }

}
