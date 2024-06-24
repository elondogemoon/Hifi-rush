using System.Collections;
using UnityEngine;
using UnityEngine.UI;
//Ȱ��ȭ �� �� �������� moveCircle�� ���;� �Ѵ�.
//correctCircle�� ��ġ�� moveCircle�� ��ġ�� ������ ���콺 Ŭ��
//���� �� gimmicGauge�� fillamount ����
//���� ��  fail�ִϸ��̼� ���
//Disable�� ������ �ʱ�ȭ
public class SP_Attack_Gimmic : MonoBehaviour
{
    [SerializeField] GameObject _spAttackbg;
    [SerializeField] Image correctCircle;
    [SerializeField] Image moveCircle;
    [SerializeField] Image gimmicGauge;
    [SerializeField] Image offCircle;
    private float moveSpeed = 10f;
    private Coroutine moveCoroutine;

    private void OnEnable()
    {
        gimmicGauge.fillAmount = 0;
        moveCircle.rectTransform.anchoredPosition = new Vector2(0, moveCircle.rectTransform.anchoredPosition.y);
       
    }

    private void OnDisable()
    {
        EndGimmic();
    }

    public void StartGimic()
    {
        _spAttackbg.SetActive(true);
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = StartCoroutine(MoveCircle());
    }

    public void EndGimmic()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
            moveCoroutine = null;
        }
        gimmicGauge.fillAmount = 0;
        moveCircle.rectTransform.anchoredPosition = new Vector2(0, moveCircle.rectTransform.anchoredPosition.y);
    }

    private IEnumerator MoveCircle()
    {
        while (true)
        {
            moveCircle.rectTransform.anchoredPosition += Vector2.left * moveSpeed * Time.deltaTime;

            if (moveCircle.rectTransform.anchoredPosition.x == offCircle.rectTransform.anchoredPosition.x)
            {
                moveCircle.enabled = false;
                yield break; 
            }
            CheckCorrect();
            yield return null; 
        }
    }

    private void CheckCorrect()
    {
        if(correctCircle != null)
        {
            if(correctCircle.rectTransform.localPosition == moveCircle.rectTransform.localPosition)
            {
                
            }
        }
    }
    public void OffAttackGimic()
    {
        _spAttackbg.gameObject.SetActive(false);
    }

}
