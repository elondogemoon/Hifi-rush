using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SP_Attack_Gimmic : MonoBehaviour
{
    [SerializeField] GameObject _spAttackbg;
    [SerializeField] Image correctCircle;
    [SerializeField] Image moveCirclePrefab; // Prefab for moveCircle
    [SerializeField] public Image gimmicGauge;
    [SerializeField] Image offCircle;
    [SerializeField] int moveCircleCount = 5; // Number of moveCircles
    private float moveSpeed = 20f;
    private Coroutine moveCoroutine;
    private List<Image> moveCircles = new List<Image>();
    private int _checkCount;
    private void OnEnable()
    {
        gimmicGauge.fillAmount = 0;
        CreateMoveCircles();
        StartGimic(); // Start the gimmick when enabled
    }

    private void OnDisable()
    {
        EndGimic();
        ClearMoveCircles();
    }

    public void StartGimic()
    {
        _spAttackbg.SetActive(true);
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = StartCoroutine(MoveCircles());
    }

    public void EndGimic()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
            moveCoroutine = null;
        }
        CheckSuccessorFail(); // Check success or failure when gimmick ends
        gimmicGauge.fillAmount = 0;
        ClearMoveCircles();
    }

    private void CreateMoveCircles()
    {
        float spacing = 60f / (moveCircleCount - 1); // Calculate spacing between circles
        for (int i = 0; i < moveCircleCount; i++)
        {
            Image moveCircle = Instantiate(moveCirclePrefab, _spAttackbg.transform);
            moveCircle.rectTransform.anchoredPosition = new Vector2(i * spacing, moveCircle.rectTransform.anchoredPosition.y); // Set position with fixed spacing
            moveCircles.Add(moveCircle);
        }
    }

    private void ClearMoveCircles()
    {
        foreach (var circle in moveCircles)
        {
            Destroy(circle.gameObject);
        }
        moveCircles.Clear();
    }

    private IEnumerator MoveCircles()
    {
        while (true)
        {
            bool allCirclesPassed = true;
            foreach (var circle in moveCircles)
            {
                circle.rectTransform.anchoredPosition += Vector2.left * moveSpeed * Time.deltaTime;

                if (circle.rectTransform.anchoredPosition.x > offCircle.rectTransform.anchoredPosition.x)
                {
                    allCirclesPassed = false;
                }

                if (Input.GetMouseButtonDown(0)) // Check for mouse click
                {
                    CheckCorrect(circle);
                }
            }

            if (allCirclesPassed)
            {
                EndGimic(); // End the gimmick when all circles have passed the offCircle
                yield break;
            }

            yield return null;
        }
    }

    private void CheckCorrect(Image moveCircle)
    {
        if (correctCircle != null)
        {
            if (Vector2.Distance(correctCircle.rectTransform.localPosition, moveCircle.rectTransform.localPosition) < 5f)
            {
                gimmicGauge.fillAmount += 0.2f;
                moveCircle.rectTransform.anchoredPosition = new Vector2(200, moveCircle.rectTransform.anchoredPosition.y);
                _checkCount++;
                // Move the circle out of visible range
            }
        }
    }

    public void OffAttackGimic()
    {
        Debug.Log(_checkCount);
        _spAttackbg.gameObject.SetActive(false);
    }

    public void CheckSuccessorFail()
    {
        if (gimmicGauge.fillAmount < 0.8f)
        {
            RhythmManager.Instance.FailSpAttack();
            OffAttackGimic();
        }
        else
        {
            // 성공 시 수행할 작업
        }
    }
}
