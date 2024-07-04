using UnityEngine;
using UnityEngine.UI;

public class RhythmCircle : MonoBehaviour
{
    [SerializeField] Image bigCircleImage; // UI Image 컴포넌트 (큰 원)
    [SerializeField] Image smallCircleImage; // UI Image 컴포넌트 (작은 원)
    [SerializeField] float shrinkSpeed = 1f; // 큰 원이 줄어드는 속도
    [SerializeField] float initialScale = 1f; // 큰 원의 초기 크기
    [SerializeField] float targetScale = 0.2f; // 작은 원과 일치하는 큰 원의 목표 크기
    [SerializeField] GameObject great; // "Great" 텍스트
    [SerializeField] GameObject successEffect; // 성공 효과
    [SerializeField] GameObject damageParticlePrefab; // 데미지 파티클 프리팹
    [SerializeField] Transform playerTransform; // 플레이어의 위치 참조
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
            // 마우스 클릭 확인
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
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

        SfxManager.Instance.OnCircleSuccess();
        successEffect.SetActive(true);
        great.SetActive(true);
        Invoke("OffGreat", 2);
        OffCircle();

        PlayDamageParticle();
    }

    private void PlayDamageParticle()
    {
        if (damageParticlePrefab != null && playerTransform != null)
        {
            // 플레이어 앞에 첫 번째 파티클 생성
            Vector3 spawnPosition = playerTransform.position + playerTransform.forward * 2f;
            GameObject damageParticle = Instantiate(damageParticlePrefab, spawnPosition, Quaternion.identity);

            // 필요에 따라 파티클의 방향을 플레이어의 앞쪽으로 설정
            damageParticle.transform.forward = playerTransform.forward;

            // 두 번째 파티클 생성을 0.5초 지연
            Invoke("SpawnSecondParticle", 0.5f);
        }
    }

    private void SpawnSecondParticle()
    {
        if (damageParticlePrefab != null && playerTransform != null)
        {
            // 첫 번째 파티클의 위치를 기준으로 앞쪽에 두 번째 파티클 생성
            Vector3 spawnPosition = playerTransform.position + playerTransform.forward * 4f; // 첫 번째 파티클 앞에 생성되도록 위치 조정
            GameObject damageParticle2 = Instantiate(damageParticlePrefab, spawnPosition, Quaternion.identity);

            // 필요에 따라 파티클의 방향을 플레이어의 앞쪽으로 설정
            damageParticle2.transform.forward = playerTransform.forward;
        }
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

    public bool IsAnimating()
    {
        return isAnimating;
    }
}
