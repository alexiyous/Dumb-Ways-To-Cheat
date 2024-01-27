using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LegTableCheat : MonoBehaviour
{
    [SerializeField] private SpriteRenderer areaInput;
    [SerializeField] private Image fillBackgroundBar;
    [SerializeField] private Image fillBar;
    [SerializeField] private float fillSpeed = 5f;
    private bool showBar = true;
    private bool cheatFlag = false;

    private float timeElapsed;
    private float timeBarElapsed;
    [SerializeField] private float timeToFill = 3; 
    private Vector2 currentFillAmount;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Checker();
    }

    private void Checker()
    {
        // Convert mouse position to world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Ensure the z-coordinate is 0 in 2D space

        if (IsMouseInsideSquare(mousePosition))
        {
            if (timeElapsed < timeToFill)
            {
                timeElapsed += Time.deltaTime;
                GameManager.instance.isCheating = false;
            }
            else
            {
                if (showBar)
                {
                    fillBackgroundBar.gameObject.SetActive(true);
                    showBar = false;
                }
                IncreaseBar();
                cheatFlag = false;
            }
        }
        else
        {
            if (!cheatFlag)
            {
                GameManager.instance.isCheating = false;
                cheatFlag = true;
            }

            if (!showBar)
            {
                fillBackgroundBar.gameObject.SetActive(false);
                showBar = true;
            }
            timeElapsed = 0;
            timeBarElapsed = 0f;
            currentFillAmount = fillBar.transform.localScale;
        }

        if (fillBar.transform.localScale.x >= 1f)
        {
            GameManager.instance.questTracker += 1; 
            gameObject.SetActive(false);
        }
    }

    private bool IsMouseInsideSquare(Vector3 mousePosition)
    {
        // Get the square position and size
        Vector3 squarePosition = areaInput.transform.position;
        Vector3 squareSize = areaInput.bounds.size;

        // Check if the mouse position is within the square bounds
        return mousePosition.x >= squarePosition.x - squareSize.x / 2 &&
               mousePosition.x <= squarePosition.x + squareSize.x / 2 &&
               mousePosition.y >= squarePosition.y - squareSize.y / 2 &&
               mousePosition.y <= squarePosition.y + squareSize.y / 2;
    }

    private void IncreaseBar()
    {
        GameManager.instance.isCheating = true;
        timeBarElapsed += Time.deltaTime;
        fillBar.transform.localScale = new Vector2(Mathf.Lerp(currentFillAmount.x, 1f, 
            timeBarElapsed / fillSpeed), currentFillAmount.y);
    }
}
