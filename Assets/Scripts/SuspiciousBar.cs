using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspiciousBar : MonoBehaviour
{
    [SerializeField] private GameObject fillBar;
    [SerializeField] private float hookPower = 5f;
    [SerializeField] private float hookDegredationPower = 3.5f;
    private float hookProgress;

    private void Update()
    {
        BarUpdate(GameManager.instance.detected);

        if (hookProgress >= 1f)
        {
            GameManager.instance.Win();
        }
    }

    private void BarUpdate(bool isDetect)
    {
        Vector2 ls = fillBar.transform.localScale;
        ls.x = hookProgress;
        fillBar.transform.localScale = ls;

        if (isDetect)
        {
            hookProgress += hookPower * Time.deltaTime;
        }
        else
        {
            hookProgress -= hookDegredationPower * Time.deltaTime;
        }

        hookProgress = Mathf.Clamp(hookProgress, 0f, 1f);
    }
}
