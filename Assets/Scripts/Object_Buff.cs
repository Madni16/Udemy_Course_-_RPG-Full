using System.Collections;
using UnityEngine;

public class Object_Buff : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("Buff details")]
    [SerializeField] private float buffDuration = 4;
    [SerializeField] private bool canBeUsed = true;

    [Header("Floaty movement")]
    [SerializeField] private float floatSpeed = 1f;
    [SerializeField] private float floatRange = .1f;
    private Vector3 startPosition;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        startPosition = transform.position;
    }

    private void Update()
    {
        float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatRange;
        transform.position = startPosition + new Vector3(0, yOffset);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canBeUsed)
            return;

        StartCoroutine(BuffCoroutine(buffDuration));
    }

    private IEnumerator BuffCoroutine(float duration)
    {
        canBeUsed = false;
        sr.color = Color.clear;
        Debug.Log($"Buff is applied for: {duration} seconds");

        yield return new WaitForSeconds(duration);

        Debug.Log("Buff has expired!");

        Destroy(gameObject);
    }
}
