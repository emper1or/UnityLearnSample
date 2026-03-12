using System.Collections;
using UnityEngine;

public class EliseyScript : SampleScript
{
    [SerializeField]
    [Tooltip("Родительский объект, чьи дочерние объекты будут удалены")]
    private Transform target;

    [SerializeField]
    [Tooltip("Время плавного сжатия в секундах")]
    [Min(0.1f)]
    private float shrinkDuration = 1f;

    [SerializeField]
    [Tooltip("Конечный масштаб объектов при сжатии (0 = полное исчезновение)")]
    [Range(0f, 1f)]
    private float endScale = 0f;

    private void OnValidate()
    {
        shrinkDuration = Mathf.Max(0.1f, shrinkDuration);
        endScale = Mathf.Clamp01(endScale);
    }

    public override void Use()
    {
        if (target == null)
        {
            Debug.LogWarning("Target Transform not assigned", this);
            return;
        }

        if (target.childCount == 0)
        {
            Debug.LogWarning("Target has no children to delete", this);
            return;
        }

        StartCoroutine(ShrinkAndDeleteCoroutine());
    }

    private IEnumerator ShrinkAndDeleteCoroutine()
    {
        Transform[] children = new Transform[target.childCount];
        Vector3[] startScales = new Vector3[target.childCount];

        for (int i = 0; i < target.childCount; i++)
        {
            children[i] = target.GetChild(i);
            if (children[i] != null)
            {
                startScales[i] = children[i].localScale;
            }
        }

        float elapsed = 0f;
        while (elapsed < shrinkDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / shrinkDuration;

            for (int i = 0; i < children.Length; i++)
            {
                if (children[i] != null)
                {
                    Vector3 newScale = Vector3.Lerp(startScales[i], Vector3.one * endScale, t);
                    children[i].localScale = newScale;
                }
            }

            yield return null;
        }

        for (int i = children.Length - 1; i >= 0; i--)
        {
            if (children[i] != null)
            {
                Destroy(children[i].gameObject);
            }
        }
    }
}