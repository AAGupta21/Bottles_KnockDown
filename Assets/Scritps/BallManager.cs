using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    [SerializeField] private GameObject BallPrefab = null;
    [SerializeField] private GameObject MiniBallPrefab = null;
    [SerializeField] private Vector3 SpawnPoint = Vector3.zero;
    public static int Lives = 6;

    [SerializeField] private float Max_Force = 10f;
    [SerializeField] private float Min_Force = 0.1f;
    [SerializeField] private float RateofPowerChange = 0.2f;

    [SerializeField] private float Min_Angle = 0f;
    [SerializeField] private float Max_Angle = 80f;
    [SerializeField] private float RateofAngleChange = 1f;

    [SerializeField] private float WaitDuration = 4f;
    [SerializeField] private float MaxWaitDuration = 5f;
    [SerializeField] private float TimeDiff = 0.2f;
    [SerializeField] private int NumberOfProjectedBalls = 10;
    [SerializeField] private float X_Limit = 20f;
    [SerializeField] private float Y_Limit = 10f;

    private List<GameObject> MiniBalls = null;
    private GameObject Ball = null;

    private float Angle = 0f;
    private float Power = 0.1f;
    
    private bool DrawProjection = false;

    private void Start()
    {
        Ball = Instantiate(BallPrefab, SpawnPoint, Quaternion.identity, transform);

        MiniBalls = new List<GameObject>();

        for (int i = 0; i < NumberOfProjectedBalls - 1; i++)
        {
            MiniBalls.Add(Instantiate(MiniBallPrefab, transform));
            MiniBalls[i].SetActive(false);
        }
    }

    private void Update()
    {
        if (GameManager.gameStage == GameStage.Shooting)
        {
            if (!DrawProjection)
            {
                DrawProjection = true;
                ActivateMiniBalls();
                SetMiniBallsPosition(Power, Angle);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Angle += RateofAngleChange;
                SetMiniBallsPosition(Power, Angle);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                Angle -= RateofAngleChange;
                SetMiniBallsPosition(Power, Angle);
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                Power += RateofPowerChange;
                SetMiniBallsPosition(Power, Angle);
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                Power -= RateofPowerChange;
                SetMiniBallsPosition(Power, Angle);
            }

            Power = Mathf.Clamp(Power, Min_Force, Max_Force);
            Angle = Mathf.Clamp(Angle, Min_Angle, Max_Angle);

            if(Input.GetKey(KeyCode.Space))
            {
                GameManager.gameStage = GameStage.ShotTaken;
                Fire();
            }
        }

        if (GameManager.gameStage == GameStage.SetBall)
            SetBall();

        if (GameManager.gameStage == GameStage.ResetBall)
            ResetBall();

        if (GameManager.gameStage == GameStage.ResetBallLoc)
            ReloadBallLoc();
    }

    private void ReloadBallLoc()
    {
        Ball.transform.GetChild(0).transform.localPosition = Vector3.zero;
        Ball.GetComponentInChildren<Rigidbody>().velocity = Vector3.zero;
        Ball.GetComponentInChildren<Rigidbody>().angularVelocity = Vector3.zero;

        GameManager.gameStage = GameStage.SetGame;
    }

    private void SetBall()
    {
        StopAllCoroutines();
        Ball.transform.GetChild(0).transform.localPosition = Vector3.zero;
        Ball.GetComponentInChildren<Rigidbody>().velocity = Vector3.zero;
        Ball.GetComponentInChildren<Rigidbody>().angularVelocity = Vector3.zero;

        GameManager.gameStage = GameStage.GotOne;
    }

    private void ResetBall()
    {
        StopAllCoroutines();
        Lives--;
        if(Lives > 0)
        {
            Ball.transform.GetChild(0).transform.localPosition = Vector3.zero;
            Ball.GetComponentInChildren<Rigidbody>().velocity = Vector3.zero;
            Ball.GetComponentInChildren<Rigidbody>().angularVelocity = Vector3.zero;

            GameManager.gameStage = GameStage.Shooting;
        }
        else
        {
            GameManager.gameStage = GameStage.RemoveBottles;
        }
    }
    
    private void Fire()
    {
        DrawProjection = false;
        DeActivateMiniBalls();

        Vector3 Dir = new Vector3(Power * Mathf.Cos(Angle * Mathf.Deg2Rad), Power * Mathf.Sin(Angle * Mathf.Deg2Rad), 0f);

        Ball.GetComponentInChildren<Rigidbody>().AddForce(Dir, ForceMode.Impulse);

        StartCoroutine(CheckBall());
        StartCoroutine(MaxDurationWait());
    }

    private IEnumerator MaxDurationWait()
    {
        yield return new WaitForSeconds(MaxWaitDuration);
        StopCoroutine(CheckBall());
        StartCoroutine(WaittoEnd());
    }

    private IEnumerator CheckBall()
    {
        while (GameManager.gameStage == GameStage.ShotTaken)
        {
            yield return new WaitForSeconds(0.1f);
            if (Ball.transform.GetChild(0).transform.position.y < -Y_Limit || Ball.transform.GetChild(0).transform.position.y > Y_Limit
                || Ball.transform.GetChild(0).transform.position.x > X_Limit || Ball.transform.GetChild(0).transform.position.x < -X_Limit || (Ball.GetComponentInChildren<Rigidbody>().velocity.magnitude < 0.3f))
            {
                GameManager.gameStage = GameStage.ResetBall;
            }
        }
    }

    private IEnumerator WaittoEnd()
    {
        yield return new WaitForSeconds(WaitDuration);
        GameManager.gameStage = GameStage.ResetBall;
    }

    private void SetMiniBallsPosition(float p, float a)
    {
        for (int i = 1; i < NumberOfProjectedBalls; i++)
        {
            Vector3 pos = Ball.transform.GetChild(0).transform.position +
                new Vector3(i * TimeDiff * p * Mathf.Cos(a * Mathf.Deg2Rad), (i * TimeDiff * p * Mathf.Sin(a * Mathf.Deg2Rad)) - (i * TimeDiff * i * TimeDiff * 0.5f * Physics.gravity.magnitude), 0f);

            MiniBalls[i - 1].transform.position = pos;
        }
    }

    private void DeActivateMiniBalls()
    {
        foreach (GameObject g in MiniBalls)
            g.SetActive(false);
    }

    private void ActivateMiniBalls()
    {
        foreach (GameObject g in MiniBalls)
            g.SetActive(true);
    }
}