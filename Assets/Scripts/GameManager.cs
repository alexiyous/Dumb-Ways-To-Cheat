using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private TeacherController teacher;
    [HideInInspector]
    public bool isCheating;
    [HideInInspector]
    public bool detected;
    [HideInInspector]
    public int questTracker = 0;
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        SetDetect();
    }

    public void SetDetect()
    {
        if (isCheating && teacher.isDetecting)
        {
            detected = true;
        }
        else
        {
            detected = false;
        }
    }

    public void Win()
    {
        Debug.Log("Win");
    }

    public void Lose()
    {
        Debug.Log("Lose");
    }
}
