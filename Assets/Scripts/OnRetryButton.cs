using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class OnRetryButton : MonoBehaviour
{
    [SerializeField] Button button; // Inspector에서 버튼을 할당하세요
    [SerializeField] int sceneIndex = 1; // 로드할 씬의 인덱스, Inspector에서 설정 가능

    private void Start()
    {
        if (button != null)
        {
            // 버튼 클릭 시 LoadSceneAsync 메서드를 호출
            button.onClick.AddListener(() => StartCoroutine(LoadSceneAsync(sceneIndex)));
        }
    }

    private IEnumerator LoadSceneAsync(int index)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index);

        // 씬 로딩이 완료될 때까지 대기
        while (!asyncOperation.isDone)
        {
            
            Debug.Log("Loading progress: " + (asyncOperation.progress * 100) + "%");
            yield return null; 
        }

       
    }
}
