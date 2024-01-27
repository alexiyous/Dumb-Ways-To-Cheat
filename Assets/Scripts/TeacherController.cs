using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class TeacherController : MonoBehaviour
{
    [ListDrawerSettings(ShowIndexLabels = true)]
    [SerializeField] private List<Transform> routesPosition = new List<Transform>();
    [FoldoutGroup("Move Config")]
    [SerializeField] private float moveSpeed = 1f;
    [FoldoutGroup("Move Config")]
    [SerializeField] private float moveDuration = 5f;
    [FoldoutGroup("Move Config")]
    [SerializeField] private float waitTime = 1f;
    [FoldoutGroup("Move Config")]
    [SerializeField] private float sizePerception = 1f;
    [FoldoutGroup("Move Config")]
    [SerializeField] private SpriteRenderer sprite;

    [FoldoutGroup("Notif Config")]
    [SerializeField] private GameObject notif;
    [FoldoutGroup("Notif Config")]
    [SerializeField] private float speed = 1f;
    [FoldoutGroup("Notif Config")]
    [SerializeField] private float range = 1f;

    private Vector2 originScale = Vector2.one;
    private Vector2 initialScale;
    private Vector2 initialPosition;
    private Vector2 notifOriginPosition;
    private float originAlpha = 1f;
    private float moveCounter;
    private float waitCounter;
    private int currentRouteIndex = 0;
    private int nextRouteIndex = 0;
    private bool isCalculating = true;
    private bool shouldWait = false;
    private State currentState;

    private void Start()
    {
        currentRouteIndex = Random.value > 0.5f ? 0 : 2;
        transform.position = routesPosition[currentRouteIndex].position;
        originScale = transform.localScale;
        notifOriginPosition = notif.transform.position;
        originAlpha = sprite.color.a;
    }

    private void Update()
    {

        Movement();

    }

    private void Movement()
    {
        switch (currentRouteIndex)
        {
            case 0:
                if (isCalculating)
                {
                    currentState = State.Move;
                    transform.localScale = originScale;
                    initialScale = transform.localScale;
                    initialPosition = transform.position;
                    var indexArray = new int[] { 1, 2, 4 };
                    var index = Random.Range(0, indexArray.Length);
                    nextRouteIndex = indexArray[index];
                    shouldWait = Random.value > 0.5f ? true : false;
                    isCalculating = false;

                }

                if (nextRouteIndex == 1)
                {
                    MoveToTargetOnState(nextRouteIndex, initialPosition, initialScale, shouldWait, true, true);
                }

                if (nextRouteIndex == 2)
                {
                    MoveToTargetOnState(nextRouteIndex, initialPosition, initialScale, shouldWait, false);
                }

                if (nextRouteIndex == 4)
                {
                    MoveToTargetOnState(nextRouteIndex, initialPosition, initialScale, shouldWait, false);
                }
                break;

            case 1:
                if (isCalculating)
                {
                    currentState = State.Move;
                    transform.localScale = Vector2.one * sizePerception;
                    initialScale = transform.localScale;
                    initialPosition = transform.position;
                    nextRouteIndex = 0;
                    shouldWait = Random.value > 0.5f ? true : false;
                    isCalculating = false;
                }

                if (nextRouteIndex == 0)
                {
                    MoveToTargetOnState(nextRouteIndex, initialPosition, initialScale, shouldWait, false, true);
                }
                break;

            case 2:
                if (isCalculating)
                {
                    currentState = State.Move;
                    transform.localScale = originScale;
                    initialScale = transform.localScale;
                    initialPosition = transform.position;
                    var indexArray = new int[] { 0, 4, 3 };
                    var index = Random.Range(0, indexArray.Length);
                    nextRouteIndex = indexArray[index];
                    shouldWait = Random.value > 0.5f ? true : false;
                    isCalculating = false;
                }

                if (nextRouteIndex == 0)
                {
                    MoveToTargetOnState(nextRouteIndex, initialPosition, initialScale, shouldWait, false);
                }

                if (nextRouteIndex == 3)
                {
                    MoveToTargetOnState(nextRouteIndex, initialPosition, initialScale, shouldWait, true, true);
                }

                if (nextRouteIndex == 4)
                {
                    MoveToTargetOnState(nextRouteIndex, initialPosition, initialScale, shouldWait, false);
                }
                break;

            case 3:
                if (isCalculating)
                {
                    currentState = State.Move;
                    transform.localScale = Vector2.one * sizePerception;
                    initialScale = transform.localScale;
                    initialPosition = transform.position;
                    nextRouteIndex = 2;
                    shouldWait = Random.value > 0.5f ? true : false;
                    isCalculating = false;
                }

                if (nextRouteIndex == 2)
                {
                    MoveToTargetOnState(nextRouteIndex, initialPosition, initialScale, shouldWait, false, true);
                }
                break;

            case 4:
                if (isCalculating)
                {
                    currentState = State.Move;
                    transform.localScale = originScale;
                    initialScale = transform.localScale;
                    initialPosition = transform.position;
                    nextRouteIndex = Random.value > 0.5f ? 0 : 2;
                    shouldWait = Random.value > 0.5f ? true : false;
                    isCalculating = false;
                }

                if (nextRouteIndex == 0)
                {
                    MoveToTargetOnState(nextRouteIndex, initialPosition, initialScale, shouldWait, false);
                }

                if (nextRouteIndex == 2)
                {
                    MoveToTargetOnState(nextRouteIndex, initialPosition, initialScale, shouldWait, false);
                }
                break;
        }
    }

    private void MoveToTargetOnState(int nextRouteIndex, Vector2 initialPosition, Vector2 initialScale, bool wait, bool increaseSize, bool fade = false)
    {
        if (moveCounter < moveDuration && currentState == State.Move)
        {
            transform.position = Vector2.Lerp(initialPosition, routesPosition[nextRouteIndex].position, moveCounter / moveDuration);
            moveCounter += Time.deltaTime;
        }

        if (increaseSize)
        {
            IncreaseSize(moveCounter / moveDuration, initialScale);
            if (fade)
            {

                Fade(moveCounter / moveDuration, 1f, 0.5f);
            }
        }
        else
        {
            DecreaseSize(moveCounter / moveDuration, initialScale);

            if (fade)
            {
                Fade(moveCounter / moveDuration, 0.5f, originAlpha);
            }

        }

        if (moveCounter / moveDuration >= 1f)
        {
            var delay = false;
            currentState = State.Idle;
            Debug.Log("Move to next route");
            transform.position = routesPosition[nextRouteIndex].position;
            transform.localScale = increaseSize ? Vector2.one * sizePerception : originScale;
            if (wait)
            {
                delay = Wait();
                if (delay)
                {
                    isCalculating = true;
                    moveCounter = 0f;
                    notif.transform.position = new Vector2(notif.transform.position.x, transform.position.y + notifOriginPosition.y);
                    currentRouteIndex = nextRouteIndex;
                    shouldWait = false;
                }
            }
            else if (!wait)
            {
                isCalculating = true;
                moveCounter = 0f;
                notif.transform.position = new Vector2(notif.transform.position.x, transform.position.y + notifOriginPosition.y);
                currentRouteIndex = nextRouteIndex;
                shouldWait = false;
            }

        }
    }

    private void IncreaseSize(float duration, Vector2 initialScale)
    {
        transform.localScale = Vector2.Lerp(initialScale, Vector2.one * sizePerception, duration);
    }

    private void DecreaseSize(float duration, Vector2 initialScale)
    {
        transform.localScale = Vector2.Lerp(initialScale, originScale, duration);
    }

    private bool Wait()
    {
        waitCounter += Time.deltaTime;
        Debug.Log(waitCounter);
        if (waitCounter >= waitTime)
        {
            waitCounter = 0f;
            notif.SetActive(false);
            return true;
        }
        else
        {
            notif.SetActive(true);
            if (!(nextRouteIndex == 1 || nextRouteIndex == 3))
            {

                NotifMovement();
            }
            return false;
        }
    }

    private void NotifMovement()
    {
        float yPos = Mathf.PingPong(Time.time * speed, 1) * range;
        notif.transform.position = new Vector3(notif.transform.position.x, transform.position.y + yPos, notif.transform.position.z);
    }

    private void Fade(float fadeDuration, float startAlpha, float endAlpha)
    {
        float fadeAmount = Mathf.Lerp(startAlpha, endAlpha, fadeDuration);
        Color spriteColor = sprite.color;
        spriteColor.a = fadeAmount;
        sprite.color = spriteColor;
    }

    private enum State
    {
        Idle,
        Move
    }
}
