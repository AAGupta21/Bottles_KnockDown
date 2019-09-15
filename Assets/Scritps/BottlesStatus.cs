using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottlesStatus : MonoBehaviour
{
    public bool IsCurrTarget = true;
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ball" && IsCurrTarget)
        {
            IsCurrTarget = false;
            GameManager.gameStage = GameStage.SetBall;
        }
    }
}