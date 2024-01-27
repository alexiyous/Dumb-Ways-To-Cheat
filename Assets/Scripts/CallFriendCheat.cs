using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallFriendCheat : MonoBehaviour
{
    [SerializeField] private int limitCount = 5;
    [SerializeField] private float cheatingLength = 1f;
    private bool isCoroutineRunning = false;
    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (count >= limitCount)
        {
            GameManager.instance.questTracker += 1;
            gameObject.SetActive(false);
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

    private IEnumerator Cheating()
    {
        isCoroutineRunning = true;
        GameManager.instance.isCheating = true;
        yield return new WaitForSeconds(cheatingLength);
        GameManager.instance.isCheating = false;
        isCoroutineRunning = false;
    }

}
