using System.Collections;
using Unity.Collections;
using UnityEngine;

[HelpURL("https://docs.google.com/document/d/1rdTEVSrCcYOjqTJcFCHj46RvnbdJhmQUb3gHMDhVftI/edit?usp=sharing")]
public class ScalerModule : MonoBehaviour
{
    [Header("Settings")] // Заголовок в инспекторе

    [Tooltip("Целевой размер объекта при активации")]
    [SerializeField] private Vector3 targetScale = new Vector3(2, 2, 2);

    [Tooltip("Скорость изменения размера. Минимум 0.1, максимум 10")]
    [Range(0.1f, 10f)] // Ограничиваем ввод некорректных (отрицательных или слишком больших) значений
    [SerializeField] private float changeSpeed = 1f;

    [Header("Debug Info")]
    [SerializeField, ReadOnly] private Vector3 defaultScale; // ReadOnly — это кастомный атрибут, если его нет, просто SerializeField
    private Transform myTransform;
    private bool toDefault;

    private void Start()
    {
        myTransform = transform;
        defaultScale = myTransform.localScale;
        toDefault = false;
    }

    // ContextMenu позволяет вызвать метод через правую кнопку мыши на компоненте в Play Mode
    [ContextMenu("Activate Module")]
    public void ActivateModule()
    {
        // Если myTransform еще не инициализирован (вдруг вызвали из эдитора до Start)
        if (myTransform == null) myTransform = transform;

        Vector3 target = toDefault ? defaultScale : targetScale;
        StopAllCoroutines();
        StartCoroutine(ScaleCoroutine(target));
        toDefault = !toDefault;
    }

    [ContextMenu("Return To Default State")]
    public void ReturnToDefaultState()
    {
        toDefault = true;
        ActivateModule();
    }

    private IEnumerator ScaleCoroutine(Vector3 target)
    {
        Vector3 start = myTransform.localScale; // Используем localScale для консистентности
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * changeSpeed;
            myTransform.localScale = Vector3.Lerp(start, target, t);
            yield return null;
        }
        myTransform.localScale = target;
    }
}