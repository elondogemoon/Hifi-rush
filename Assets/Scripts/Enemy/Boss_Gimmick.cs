using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_Gimmick : MonoBehaviour
{
    [SerializeField] GameObject _spAttackbg;
    [SerializeField] Image correctCircle;
    [SerializeField] Image leftClickCirclePrefab; // 왼쪽 클릭 원의 프리팹
    [SerializeField] Image rightClickCirclePrefab; // 오른쪽 클릭 원의 프리팹
    [SerializeField] public Image gimmicGauge;
    [SerializeField] Image offCircle;
    [SerializeField] int totalCircleCount = 6; // 원의 총 개수
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
        Debug.Log("EndGimic 호출"); // 디버그 로그 추가
        CheckSuccessorFail();
        ClearMoveCircles();
    }

    private void CreateMoveCircles()
    {
        float spacing = 60f / (totalCircleCount - 1);

        for (int i = 0; i < totalCircleCount; i++)
        {
            ClickType clickType = (Random.Range(0, 2) == 0) ? ClickType.Left : ClickType.Right; // 무작위로 클릭 타입 지정
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
                    Debug.Log($"원 위치: {circle.Image.rectTransform.anchoredPosition.x}"); // 디버그 로그 추가

                    // 원이 offCircle보다 왼쪽에 있으면 allCirclesPassed를 false로 설정
                    if (circle.Image.rectTransform.anchoredPosition.x + circle.Image.rectTransform.rect.width / 2 > offCircle.rectTransform.anchoredPosition.x)
                    {
                        allCirclesPassed = false;
                    }

                    if (Input.GetMouseButtonDown(0) && circle.ClickType == ClickType.Left) // 왼쪽 클릭
                    {
                        CheckCorrect(circle);
                    }
                    else if (Input.GetMouseButtonDown(1) && circle.ClickType == ClickType.Right) // 오른쪽 클릭
                    {
                        CheckCorrect(circle);
                    }
                }
            }

            if (allCirclesPassed)
            {
                // 모든 원이 화면을 벗어나면, 성공 또는 실패 판단
                Debug.Log("모든 원이 화면을 벗어남"); // 디버그 로그 추가
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
                gimmicGauge.fillAmount += 0.2f;
                moveCircle.Image.enabled = false;
                moveCircle.Image.rectTransform.anchoredPosition = new Vector2(200, moveCircle.Image.rectTransform.anchoredPosition.y);
                _checkCount++;
                Debug.Log($"원 체크: {_checkCount}"); // 디버그 로그 추가
                CheckGimmick();
            }
        }
    }

    public void OffAttackGimic()
    {
        Debug.Log($"OffAttackGimic 호출, 체크된 원 개수: {_checkCount}"); // 디버그 로그 추가
        _spAttackbg.gameObject.SetActive(false);
    }

    public void CheckGimmick()
    {
        if (_checkCount >= 7)
        {
            GameManager.Instance.SuccessGimmic();
            BeatManager.Instance.HighVolume();
            GameManager.Instance.OffBossGimmic();
            this.gameObject.SetActive(false);
        }
        else
        {
            GameManager.Instance.FailGimmic();
            Debug.Log("else");
        }
    }

    public void CheckSuccessorFail()
    {
        Debug.Log($"Gimmick 체크: {gimmicGauge.fillAmount}"); // 디버그 로그 추가
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
