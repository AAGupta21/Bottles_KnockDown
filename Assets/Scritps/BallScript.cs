using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public static int BounceCount = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Walls")
            BounceCount++;
    }
}