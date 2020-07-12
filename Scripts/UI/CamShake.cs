using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{
    public Animator anim;

    public void Shake()
    {
        int rando = Random.Range(0, 1);
        switch(rando)
        {
            case 0:
                anim.SetTrigger("Shake");
                break;
            case 1:
                anim.SetTrigger("Shake2");
                break;
        }
    }

    public void BigShake()
    {
        anim.SetTrigger("BigShake");
    }
}
