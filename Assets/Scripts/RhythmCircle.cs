using UnityEngine;
using UnityEngine.UI;

public class RhythmCircle : MonoBehaviour
{
    [SerializeField] Image bigCircleImage; // UI Image component for the big circle
    [SerializeField] Image smallCircleImage; // UI Image component for the small circle
    [SerializeField] float shrinkSpeed = 1f; // Speed at which the big circle shrinks
    [SerializeField] float initialScale = 1f; // Initial scale of the big circle
    [SerializeField] float targetScale = 0.2f; // Target scale of the big circle to match the small circle
    [SerializeField] GameObject great;
    [SerializeField] GameObject successEffect;
    private bool isAnimating = false;
    private bool isClicked = false;

    void Start()
    {
        ResetAnimation();
        OffCircle();
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
        SfxManager.Instance.OnCircleOn();
        this.gameObject.SetActive(true);
    }

    private void ResetAnimation()
    {
        bigCircleImage.transform.localScale = new Vector3(initialScale, initialScale, initialScale);
        bigCircleImage.enabled = true;
        smallCircleImage.enabled = true;
        great.SetActive(false);
    }

    private void UpdateAnimation()
    {
        bigCircleImage.transform.localScale -= new Vector3(shrinkSpeed, shrinkSpeed, 0) * Time.deltaTime;

        if (Mathf.Abs(bigCircleImage.transform.localScale.x - targetScale) < 0.05f)
        {
            // Check for mouse click
            if (Input.GetMouseButtonDown(0)||Input.GetMouseButtonDown(1))
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
            bigCircleImage.enabled = false;
            smallCircleImage.enabled = false;
        }
    }

    private void OnCircleClicked()
    {
        isClicked = true;
        isAnimating = false;
        Debug.Log("Perfect Timing!");
        //TODO : VFX(파티클),화면 효과, 사운드 추가
        // this.gameObject.SetActive(false);
        SfxManager.Instance.OnCircleSuccess();
        successEffect.SetActive(true);
        great.SetActive(true);
        Invoke("OffGreat", 2);
        OffCircle();
    }
    private void OffCircle()
    {
        smallCircleImage.enabled = false;
        bigCircleImage.enabled = false;
    }
    private void OffGreat()
    {
        great.SetActive(false);
        successEffect.SetActive(false);

    }
}