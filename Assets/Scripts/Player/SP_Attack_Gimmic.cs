using UnityEngine;
using UnityEngine.UI;
public class SP_Attack_Gimmic : MonoBehaviour
{
    [SerializeField] GameObject _spAttackbg;
    [SerializeField] Image correctCircle;
    [SerializeField] Image moveCircle;
    public float moveSpeed = 5f;
    private bool isMoving = false;
    private RectTransform rectTransform;
    private Vector2 startPosition;
    private Vector2 endPosition;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPosition = new Vector2(500f, 0f); 
        endPosition = new Vector2(-500f, 0f); 
        rectTransform.anchoredPosition = startPosition;
    }

    public void StartGimic()
    {
        _spAttackbg.gameObject.SetActive(true);
        //isMoving = true;
       // rectTransform.anchoredPosition = startPosition;
    }

    private void Update()
    {
        if (isMoving)
        {
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, endPosition, moveSpeed * Time.deltaTime);
            if (rectTransform.anchoredPosition == endPosition)
            {
                isMoving = false;
                OnAnimationEnd();
            }
        }
    }

    private void OnAnimationEnd()
    {
        Debug.Log("Animation Ended");
    }

    public bool CheckTiming()
    {
        float distance = Vector2.Distance(rectTransform.anchoredPosition, Vector2.zero); 
        return distance < 50f; 
    }
}
