using UnityEngine;
using UnityEngine.UI;

public class RhythmCircle : MonoBehaviour
{
    [SerializeField] Image bigCircleImage; // UI Image component for the big circle
    [SerializeField] Image smallCircleImage; // UI Image component for the small circle
    [SerializeField] float shrinkSpeed = 1f; // Speed at which the big circle shrinks
    [SerializeField] float initialScale = 1f; // Initial scale of the big circle
    [SerializeField] float targetScale = 0.2f; // Target scale of the big circle to match the small circle

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

    public void StartAnimation()
    {
        ResetAnimation();
        isAnimating = true;
        isClicked = false;
        this.gameObject.SetActive(true);
    }

    private void ResetAnimation()
    {
        bigCircleImage.transform.localScale = new Vector3(initialScale, initialScale, initialScale);
    }

    private void UpdateAnimation()
    {
        bigCircleImage.transform.localScale -= new Vector3(shrinkSpeed, shrinkSpeed, 0) * Time.deltaTime;

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

       
        if (bigCircleImage.transform.localScale.x <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnCircleClicked()
    {
        isClicked = true;
        isAnimating = false;
        Debug.Log("Perfect Timing!");
        //TODO : VFX(파티클),화면 효과, 사운드 추가
        this.gameObject.SetActive(false); 
    }
}