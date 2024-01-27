using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StealAnswerCheat : MonoBehaviour
{
    [SerializeField] private TeacherController teacher;
    [SerializeField] private Button areaInput;
    [SerializeField] private Image fillBar;
    [SerializeField] private float fillAmount = 0.1f;
    private bool firstTime = true;
    private bool isClicked = false;
    private float hookProgress;
    private bool isCoroutineRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!firstTime)
        {
            ShowButton();
        }

        if (hookProgress >= 1f)
        {
            GameManager.instance.questTracker += 1;
            gameObject.SetActive(false);
        }
    }

    private void ShowButton()
    {
        if (teacher.NextRouteIndex == 1 || teacher.NextRouteIndex == 3)
        {
            if (!teacher.ShouldWait) return;
            areaInput.gameObject.SetActive(true);
        }
        else
        {
            if (!isCoroutineRunning)
            {
                StartCoroutine(DelayInactive());
            }
        }
    }

    private IEnumerator DelayInactive()
    {
        isCoroutineRunning = true;
        if (isClicked)
        {
            GameManager.instance.isCheating = true;
        }
        yield return new WaitForSeconds(teacher.MoveDuration);
        GameManager.instance.isCheating = false;
        areaInput.gameObject.SetActive(false);
        isCoroutineRunning = false;
    }

    public void Cheating()
    {
        isClicked = true;
        if (firstTime)
        {
            areaInput.gameObject.SetActive(true);
            firstTime = false;
        }

        if (!firstTime)
        {
            IncreaseBar(fillAmount);
        }
    }

    private void IncreaseBar(float fillAmount)
    {
        Vector2 ls = fillBar.transform.localScale;
        ls.x = hookProgress;
        fillBar.transform.localScale = ls;

        hookProgress += fillAmount;
        isClicked = false;
    }
}
