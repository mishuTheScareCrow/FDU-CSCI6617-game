using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 8f;
    public float lifeTime = 3f;
    [SerializeField] private Color componentColor = new Color(1f, 0.95f, 0.25f);

    void Start()
    {
        ComponentColorUtility.Apply(gameObject, componentColor);
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
