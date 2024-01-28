using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallFriendCheat : MonoBehaviour
{
    [SerializeField] private int limitCount = 5;
    [SerializeField] private float cheatingLength = 1f;
    [SerializeField] private Animator animator;
    [SerializeField] private Button button;
    private bool isCoroutineRunning = false;
    private int count = 0;
    private bool isIdle = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isIdle)
        {

        }
        if (count >= limitCount)
        {
            StartCoroutine(Idle());
          
            GameManager.instance.questTracker += 1;
            button.onClick.RemoveAllListeners();
            count = 0;
        }
    }

    public void OnClicked()
    {
        if (!isCoroutineRunning)
        {
            StartCoroutine(Cheating());
            count += 1;
        }
    }

    private IEnumerator Idle()
    {
        isIdle = false;
        animator.SetTrigger("show");
        yield return new WaitForSeconds(10f);
        isIdle = true;

    }

    private IEnumerator Cheating()
    {
        isCoroutineRunning = true;
        GameManager.instance.isCheating = true;
        yield return new WaitForSeconds(cheatingLength);
        GameManager.instance.isCheating = false;
        isCoroutineRunning = false;
    }

}
