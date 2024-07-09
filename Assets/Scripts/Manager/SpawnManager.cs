using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField] GameObject _swordEnemy;
    [SerializeField] GameObject _gunEnemy;

    // 구역별 스폰 포인트 배열
    [SerializeField] Transform[] _zone1SpawnPoints;
    [SerializeField] Transform[] _zone2SpawnPoints;
    [SerializeField] Transform[] _zone3SpawnPoints;

    // 각 구역의 현재 스폰 인덱스
    private int _currentSpawnIndexZone1 = 0;
    private int _currentSpawnIndexZone2 = 0;
    private int _currentSpawnIndexZone3 = 0;

    // 구역 열거형
    public enum Zone { Zone1, Zone2, Zone3 }

    // Start is called before the first frame update
    void Start()
    {
        // 여기에 초기화 코드가 필요하면 추가
    }

    // Update is called once per frame
    void Update()
    {
        // 필요시 업데이트 코드 추가
    }

    public void SpawnEnemy(string enemyType, Zone zone)
    {
        GameObject enemyToSpawn = null;
        Transform[] spawnPoints = null;
        int currentSpawnIndex = 0;

        // 적 유형에 따라 스폰할 적을 선택
        if (enemyType == "sword")
        {
            enemyToSpawn = _swordEnemy;
        }
        else if (enemyType == "gun")
        {
            enemyToSpawn = _gunEnemy;
        }

        // 구역에 따라 스폰 포인트 배열과 인덱스 설정
        switch (zone)
        {
            case Zone.Zone1:
                spawnPoints = _zone1SpawnPoints;
                currentSpawnIndex = _currentSpawnIndexZone1;
                break;
            case Zone.Zone2:
                spawnPoints = _zone2SpawnPoints;
                currentSpawnIndex = _currentSpawnIndexZone2;
                break;
            case Zone.Zone3:
                spawnPoints = _zone3SpawnPoints;
                currentSpawnIndex = _currentSpawnIndexZone3;
                break;
        }

        if (enemyToSpawn != null && spawnPoints != null && spawnPoints.Length > 0)
        {
            // 현재 스폰 포지션에서 적을 소환
            Instantiate(enemyToSpawn, spawnPoints[currentSpawnIndex].position, spawnPoints[currentSpawnIndex].rotation);

            // 다음 스폰 포지션으로 인덱스 증가, 배열의 길이를 넘으면 처음으로 돌아감
            currentSpawnIndex = (currentSpawnIndex + 1) % spawnPoints.Length;

            // 구역별 현재 스폰 인덱스 업데이트
            switch (zone)
            {
                case Zone.Zone1:
                    _currentSpawnIndexZone1 = currentSpawnIndex;
                    break;
                case Zone.Zone2:
                    _currentSpawnIndexZone2 = currentSpawnIndex;
                    break;
                case Zone.Zone3:
                    _currentSpawnIndexZone3 = currentSpawnIndex;
                    break;
            }
        }
        else
        {
            Debug.LogWarning("적 유형이 올바르지 않거나 스폰 포지션이 설정되지 않았습니다: " + enemyType);
        }
    }
}
