using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private SpriteRenderer render;

    private float resetX;
    private Vector3 startPos;

    public void Initialize(float resetX, float height)
    {
        bool fade = Random.Range(0, 1) == 1 ? true : false;

        if (fade)
            StartCoroutine(FadeCloud(0, 0.15f));

        this.resetX = resetX;
        transform.position = transform.position + new Vector3(Random.Range(-9f, 90f), height + Random.Range(-4f, 4f));
        startPos = transform.position + new Vector3(-60f, 0);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector2(resetX, transform.position.y), Time.deltaTime * speed);

        float xDis = resetX - transform.position.x;
        if(xDis <= 5f)
        {
            ResetPosition();
        }
    }

    private void ResetPosition()
    {
        transform.position = startPos;
    }

    private IEnumerator FadeCloud(float target, float dur)
    {
        float t = 0.0f;
        while (t <= dur)
        {
            float alpha = Mathf.Lerp(render.color.a, target, t / dur);
            render.color = new Color(render.color.r, render.color.g, render.color.b, alpha);

            t += Time.deltaTime;
            yield return null;
        }
    }
}
