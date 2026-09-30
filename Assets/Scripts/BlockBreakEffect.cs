using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBreakEffect : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particleSystemPrefab;

    [SerializeField]
    private int poolSize = 25;
    private Queue<ParticleSystem> pools = new Queue<ParticleSystem>();
    public static BlockBreakEffect Instance;

    // Khởi tạo Singleton cho BlockBreakEffect và giữ lại đối tượng khi chuyển Scene
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this);
    }

    // Khởi tạo hàng đợi đối tượng (Object Pool) cho hiệu ứng hạt vỡ khối
    void Start()
    {
        Init();
    }

    // Tạo sẵn một số lượng ParticleSystem vào Pool để tối ưu hiệu năng bộ nhớ
    void Init()
    {
        for (int i = 0; i < poolSize; i++)
        {
            ParticleSystem particleSystem = Instantiate(particleSystemPrefab, transform);
            pools.Enqueue(particleSystem);
            particleSystem.gameObject.SetActive(false);
        }
    }

    // Lấy một hiệu ứng hạt từ Pool và kích hoạt tại vị trí ô gạch bị vỡ
    public void Play(Transform origin)
    {
        ParticleSystem ps = null;
        if (pools.Count > 0)
        {
            ps = pools.Dequeue();
        }
        if (ps == null)
        {
            ps = Instantiate(particleSystemPrefab, transform);
        }
        ps.transform.position = origin.position;
        ps.gameObject.SetActive(true);
        ps.Play();

        // Tính thời gian tồn tại của hạt để lên lịch thu hồi về Pool
        float duration = ps.main.duration + ps.main.startLifetime.constantMax;
        StartCoroutine(OnFinishEffect(ps, duration));
    }

    // Chờ cho hiệu ứng hạt phát xong rồi thu hồi đối tượng về hàng đợi (Pool)
    private IEnumerator OnFinishEffect(ParticleSystem ps, float duration)
    {
        yield return new WaitForSeconds(duration);
        pools.Enqueue(ps);
        ps.gameObject.SetActive(false);
    }
}

