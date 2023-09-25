using UnityEngine;

/// <summary>
/// Singleton pattern.
/// </summary>
public class Singleton<T> : MonoBehaviour where T : Component
{
    protected static T instance;
    public static bool HasInstance => instance != null;
    public static T TryGetInstance() => HasInstance ? instance : null;
    public static T Current => instance;

    /// <summary>
    /// Singleton design pattern
    /// </summary>
    /// <value>The instance.</value>
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name + "_AutoCreated";
                    instance = obj.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    /// <summary>
    /// On awake, we initialize our instance. Make sure to call base.Awake() in override if you need awake.
    /// </summary>
    protected virtual void Awake()
    {
        if (!Application.isPlaying)
        {
            return;
        }

        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this as T;
    }
}