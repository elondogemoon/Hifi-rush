using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_Gimmick : MonoBehaviour
{
    [SerializeField] GameObject _spAttackbg;
    [SerializeField] Image correctCircle;
    [SerializeField] Image leftClickCirclePrefab; // Prefab for left click circles
    [SerializeField] Image rightClickCirclePrefab; // Prefab for right click circles
    [SerializeField] public Image gimmicGauge;
    [SerializeField] Image offCircle;
    [SerializeField] int totalCircleCount = 6; // Total number of circles
    [SerializeField] AudioSource _audioSource;
    private float moveSpeed = 10f;
    private Coroutine moveCoroutine;
    private List<MoveCircle> moveCircles = new List<MoveCircle>();
    private int _checkCount;
    public Animator _assistAnimator;

    private enum ClickType { Left, Right }

    private void OnEnable()
    {
        gimmicGauge.fillAmount = 0;
        CreateMoveCircles();
        StartGimic();
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
        CheckSuccessorFail();
        ClearMoveCircles();
    }

    private void CreateMoveCircles()
    {
        float spacing = 60f / (totalCircleCount - 1);

        for (int i = 0; i < totalCircleCount; i++)
        {
            ClickType clickType = (Random.Range(0, 2) == 0) ? ClickType.Left : ClickType.Right; // Randomly assign click type
            Image moveCircleImage;

            if (clickType == ClickType.Left)
            {
                moveCircleImage = Instantiate(leftClickCirclePrefab, _spAttackbg.transform);
            }
            else
            {
                moveCircleImage = Instantiate(rightClickCirclePrefab, _spAttackbg.transform);
            }

            moveCircleImage.rectTransform.anchoredPosition = new Vector2(i * spacing, moveCircleImage.rectTransform.anchoredPosition.y);
            MoveCircle moveCircle = new MoveCircle(moveCircleImage, clickType);
            moveCircles.Add(moveCircle);
        }
    }

    private void ClearMoveCircles()
    {
        foreach (var circle in moveCircles)
        {
            Destroy(circle.Image.gameObject);
        }
        moveCircles.Clear();
    }

    private IEnumerator MoveCircles()
    {
        while (true)
        {
            bool allCirclesPassed = true;

            for (int i = moveCircles.Count - 1; i >= 0; i--)
            {
                var circle = moveCircles[i];
                if (circle.Image.enabled)
                {
                    circle.Image.rectTransform.anchoredPosition += Vector2.left * moveSpeed * Time.deltaTime;

                    if (circle.Image.rectTransform.anchoredPosition.x > offCircle.rectTransform.anchoredPosition.x)
                    {
                        allCirclesPassed = false;
                    }

                    if (Input.GetMouseButtonDown(0) && circle.ClickType == ClickType.Left) // Left click
                    {
                        CheckCorrect(circle);
                    }
                    else if (Input.GetMouseButtonDown(1) && circle.ClickType == ClickType.Right) // Right click
                    {
                        CheckCorrect(circle);
                    }
                }
            }

            if (allCirclesPassed)
            {
                // 모든 원이 화면을 벗어나면, 성공 또는 실패 판단
                CheckSuccessorFail();
                yield break;
            }

            yield return null;
        }
    }

    private void CheckCorrect(MoveCircle moveCircle)
    {
        if (correctCircle != null)
        {
            if (Vector2.Distance(correctCircle.rectTransform.localPosition, moveCircle.Image.rectTransform.localPosition) < 5f)
            {
                _assistAnimator.SetTrigger("1");
                _audioSource.Play();
                gimmicGauge.fillAmount += 0.1f;
                moveCircle.Image.enabled = false;
                moveCircle.Image.rectTransform.anchoredPosition = new Vector2(200, moveCircle.Image.rectTransform.anchoredPosition.y);
                _checkCount++;
                CheckGimmick();
            }
        }
    }

    public void OffAttackGimic()
    {
        Debug.Log(_checkCount);
        _spAttackbg.gameObject.SetActive(false);
    }

    public void CheckGimmick()
    {
        if (_checkCount >= 7)
        {
            BeatManager.Instance.HighVolume();
            GameManager.Instance.OffBossGimmic();
            this.gameObject.SetActive(false);
        }
        else
        {
            // 필요 시 추가 로직
        }
    }

    public void CheckSuccessorFail()
    {
        if (gimmicGauge.fillAmount < 0.8f)
        {
            BeatManager.Instance.HighVolume();
            RhythmManager.Instance.FailBossGimmick();
            GameManager.Instance.OffBossGimmic();
            this.gameObject.SetActive(false);
        }
        else
        {
            _assistAnimator.SetTrigger("Success");
            BeatManager.Instance.HighVolume();
            RhythmManager.Instance.SuccessBossGimmick();
            GameManager.Instance.SuccessGimmic();
            this.gameObject.SetActive(false);

            gimmicGauge.fillAmount = 0;
            GameManager.Instance.OffBossGimmic();
        }
    }

    private class MoveCircle
    {
        public Image Image { get; private set; }
        public ClickType ClickType { get; private set; }

        public MoveCircle(Image image, ClickType clickType)
        {
            Image = image;
            ClickType = clickType;
        }
    }
}
