using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T _instance = null;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                T instance = GameObject.FindObjectOfType<T>() as T; // 이미 T 가 있다면 T를 바로 반환
                if (instance == null)
                {
                    instance = new GameObject(typeof(T).ToString(), typeof(T)).GetComponent<T>();
                }
                InstanceInit(instance);
                Debug.Assert(_instance != null, typeof(T).ToString() + "Singleton Falled");
            }
            return _instance;
        }
    }

    private static void InstanceInit(Object instance)
    {
        _instance = instance as T;
#if !UNITY_EDITOR
        _instance.Init();
    }
    // 씬 전환시 삭제 여부
    public virtual void Init()
    {
        DontDestroyOnLoad(_instance);
#endif
    }
    public virtual void OnDestroy()
    {
        _instance = null;
    }
}
