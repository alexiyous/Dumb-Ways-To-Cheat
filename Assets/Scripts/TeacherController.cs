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
    [SerializeField] private float waitTime = 1f;
    [SerializeField] private float sizePerception = 1f;

    private Vector2 originScale = Vector2.one;
    private Vector2 initialScale;
    private Vector2 initialPosition;
    private float moveCounter;
    private float waitCounter;
    private int currentRouteIndex = 0;
    private int nextRouteIndex = 0;
    private bool isCalculating = true;
    private bool shouldWait = false;
    private State currentState;

    private void Start()
    {
        currentRouteIndex = Random.Range(0, routesPosition.Count);
        transform.position = routesPosition[currentRouteIndex].position;
        originScale = transform.localScale;
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
                    nextRouteIndex = Random.value > 0.5f ? 1 : 2;
                    shouldWait = Random.value > 0.5f ? true : false;
                    isCalculating = false;

                }

                if (nextRouteIndex == 1)
                {
                    MoveToTargetOnState(nextRouteIndex, initialPosition, initialScale, shouldWait, true);
                }

                if (nextRouteIndex == 2)
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
                    MoveToTargetOnState(nextRouteIndex, initialPosition, initialScale, shouldWait, false);
                }
                break;

            case 2:
                if (isCalculating)
                {
                    currentState = State.Move;
                    transform.localScale = originScale;
                    initialScale = transform.localScale;
                    initialPosition = transform.position;
                    nextRouteIndex = Random.value > 0.5f ? 0 : 3;
                    shouldWait = Random.value > 0.5f ? true : false;
                    isCalculating = false;
                }

                if (nextRouteIndex == 0)
                {
                    MoveToTargetOnState(nextRouteIndex, initialPosition, initialScale, shouldWait, false);
                }

                if (nextRouteIndex == 3)
                {
                    MoveToTargetOnState(nextRouteIndex, initialPosition, initialScale, shouldWait, true);
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
                    MoveToTargetOnState(nextRouteIndex, initialPosition, initialScale, shouldWait, false);
                }
                break;
        }
    }

    private void MoveToTargetOnState(int nextRouteIndex, Vector2 initialPosition, Vector2 initialScale, bool wait, bool increaseSize)
    {
        if (moveCounter < moveDuration && currentState == State.Move )
        {
            transform.position = Vector2.Lerp(initialPosition, routesPosition[nextRouteIndex].position, moveCounter / moveDuration);
            moveCounter += Time.deltaTime;
        }

        if (increaseSize)
        {
            IncreaseSize(moveCounter / moveDuration, initialScale);
        }
        else
        {
            DecreaseSize(moveCounter / moveDuration, initialScale);
        }

        if (moveCounter / moveDuration >= 1f)
        {
            currentState = State.Idle;
            Debug.Log("Move to next route");
            transform.position = routesPosition[nextRouteIndex].position;
            transform.localScale = increaseSize ? Vector2.one * sizePerception : originScale;
            var delay = Wait();
            if (delay && wait)
            {
                isCalculating = true;
                moveCounter = 0f;
                currentRouteIndex = nextRouteIndex;
            }
            else if (!wait)
            {
                isCalculating = true;
                moveCounter = 0f;
                currentRouteIndex = nextRouteIndex;
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
            return true;
        }
        else
        {
            return false;
        }
    }

    private enum State
    {
        Idle,
        Move
    }
}
