using UnityEngine;
using UnityEngine.UI;

public class RhythmCircle : MonoBehaviour
{
    public Image bigCircleImage; // UI Image component for the big circle
    public Image smallCircleImage; // UI Image component for the small circle
    public float shrinkSpeed = 1f; // Speed at which the big circle shrinks
    public float initialScale = 2f; // Initial scale of the big circle
    public float targetScale = 0.2f; // Target scale of the big circle to match the small circle

    private bool isAnimating = false;
    private bool isClicked = false;

    void Start()
    {
        ResetAnimation();
    }

    void Update()
    {
        if (isAnimating && !isClicked)
        {
            UpdateAnimation();
        }
    }

    // Function to start the animation
    public void StartAnimation()
    {
        ResetAnimation();
        isAnimating = true;
        isClicked = false;
    }

    // Function to reset the animation
    private void ResetAnimation()
    {
        bigCircleImage.transform.localScale = new Vector3(initialScale, initialScale, initialScale);
    }

    // Function to update the animation
    private void UpdateAnimation()
    {
        // Shrink the big circle
        bigCircleImage.transform.localScale -= new Vector3(shrinkSpeed, shrinkSpeed, 0) * Time.deltaTime;

        // Check if the big circle's scale matches the small circle's scale
        if (Mathf.Abs(bigCircleImage.transform.localScale.x - targetScale) < 0.05f)
        {
            // Check for mouse click
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 localMousePosition = bigCircleImage.rectTransform.InverseTransformPoint(Input.mousePosition);
                if (bigCircleImage.rectTransform.rect.Contains(localMousePosition))
                {
                    OnCircleClicked();
                }
            }
        }

        // Destroy the big circle if it shrinks too much
        if (bigCircleImage.transform.localScale.x <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Function to handle circle click
    private void OnCircleClicked()
    {
        isClicked = true;
        isAnimating = false;
        Debug.Log("Perfect Timing!");
        // Perform additional actions on click, such as scoring or feedback
        Destroy(gameObject); // Example: Destroy the circle on click
    }
}
