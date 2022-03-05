using UnityEngine;

public class CloudsMovement : MonoBehaviour
{
    public Color gray = new Color(0.8207547f, 0.8207547f, 0.8207547f);
    public Color white = Color.white;
    public Color lightGrey = new Color(0.9056604f, 0.9056604f, 0.9056604f);
    private float speed;
    public void Start()
    {
        speed = Random.Range(0.01f, 0.05f);
        #region COLOR SWATCH
        if (speed <= 0.02f)
        {
            GetComponent<SpriteRenderer>().color = gray;
            GetComponent<SpriteRenderer>().sortingOrder = -3;
        }
        if (speed <= 0.03f && speed > 0.02f)
        {
            GetComponent<SpriteRenderer>().color = lightGrey;
            GetComponent<SpriteRenderer>().sortingOrder = -2;
        }
        if (speed >= 0.05f) { GetComponent<SpriteRenderer>().color = white; GetComponent<SpriteRenderer>().sortingOrder = -1; }
        #endregion
        transform.localPosition = new Vector3(Random.Range(-24f, 24f), Random.Range(-10, 10), 0);
        var rndScale = Random.Range(0.1755237f + 0.1f, 0.1755237f - 0.1f);
        transform.localScale = new Vector3(rndScale, rndScale, 0);
    }
    public void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BC"))
        {
            Respawn();
        }
    }
    private void Respawn()
    {
        speed = Random.Range(0.01f, 0.05f);
        #region COLOR SWATCH
        if (speed <= 0.02f)
        {
            GetComponent<SpriteRenderer>().color = gray;
            GetComponent<SpriteRenderer>().sortingOrder = -3;
        }
        if (speed <= 0.03f && speed > 0.02f)
        {
            GetComponent<SpriteRenderer>().color = lightGrey;
            GetComponent<SpriteRenderer>().sortingOrder = -2;
        }
        if (speed >= 0.05f) { GetComponent<SpriteRenderer>().color = white; GetComponent<SpriteRenderer>().sortingOrder = -1; }
        #endregion
        transform.localPosition = new Vector3(Random.Range(24f, 40f), Random.Range(-10, 10), 0);
        var rndScale = Random.Range(0.1755237f + 0.1f, 0.1755237f - 0.1f);
        transform.localScale = new Vector3(rndScale, rndScale, 0);
    }
    private void FixedUpdate()
    {
        transform.localPosition -= new Vector3(speed, 0, 0);

    }
}
