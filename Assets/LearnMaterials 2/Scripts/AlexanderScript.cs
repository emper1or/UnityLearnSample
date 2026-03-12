using UnityEngine;
using System.Collections;

public class AlexanderScript : SampleScript
{
    [Header("Настройки спавна")]

    [SerializeField]
    [Tooltip("Префаб, который будет копироваться")]
    private GameObject prefab;

    [SerializeField]
    [Tooltip("Количество копий")]
    [Min(1)]
    private int count = 5;

    [SerializeField]
    [Tooltip("Расстояние между копиями")]
    [Min(0.1f)]
    private float step = 1f;

    [Header("Дополнительно")]

    [SerializeField]
    [Tooltip("Создавать объекты как дочерние к этому объекту")]
    private bool makeChildren = false;

    [SerializeField]
    [Tooltip("Задержка между созданием объектов")]
    [Min(0f)]
    private float delayBetweenSpawns = 0f;

    public override void Use()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        for (int i = 0; i < count; i++)
        {
            // Вычисляем позицию: от текущей позиции объекта с шагом
            Vector3 spawnPosition = transform.position + transform.right * (i * step);

            // Создаем объект
            GameObject newObject = Instantiate(prefab, spawnPosition, Quaternion.identity);

            // Если нужно, делаем дочерним
            if (makeChildren)
            {
                newObject.transform.SetParent(transform);
            }

            // Если есть задержка — ждем
            if (delayBetweenSpawns > 0 && i < count - 1)
            {
                yield return new WaitForSeconds(delayBetweenSpawns);
            }
        }
    }
}