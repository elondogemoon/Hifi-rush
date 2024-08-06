using UnityEngine;

public class SpeedUpZone : MonoBehaviour
{
    [SerializeField] private AudioClip newBackgroundMusic;
    [SerializeField] private float newBPM;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BeatManager.Instance.ChangeBackgroundMusic(newBackgroundMusic, newBPM);
            SpawnManager.Instance.SpawnEnemy("gun", SpawnManager.Zone.Zone2);
            SpawnManager.Instance.SpawnEnemy("gun", SpawnManager.Zone.Zone2);
        }
    }
}
