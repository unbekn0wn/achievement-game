using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink_Platform : MonoBehaviour
{
    public int BlinkInterval;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        StartCoroutine(BlinkLoop());
    }

    IEnumerator BlinkLoop()
    {
        while(enabled)
        {
            yield return new WaitForSeconds(BlinkInterval);
            spriteRenderer.enabled = !spriteRenderer.enabled;
            boxCollider2D.enabled = !boxCollider2D.enabled;
        }
    }
}
