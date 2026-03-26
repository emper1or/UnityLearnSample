using UnityEngine;
using UnityEngine.Events;

public class ObstacleItem : MonoBehaviour
{
    [Range(0f, 1f)]
    public float currentValue = 1f;
    public UnityEvent onDestroyObstacle;

    private Renderer _renderer;

    void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void GetDamage(float value)
    {
        currentValue -= value;
        currentValue = Mathf.Clamp01(currentValue);
        
        _renderer.material.color = Color.Lerp(Color.red, Color.white, currentValue);

        if (currentValue <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        onDestroyObstacle?.Invoke();
        Destroy(gameObject);
    }
}