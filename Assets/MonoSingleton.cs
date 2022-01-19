using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogWarning(typeof(T).ToString() + " is NULL.");
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        _instance = (T)this;
    }
}