using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float fireRate = 1f;
    [SerializeField] private Color componentColor = new Color(0.2f, 0.75f, 1f);

    void Start()
    {
        ComponentColorUtility.Apply(gameObject, componentColor);
        InvokeRepeating(nameof(Fire), 1f, fireRate);
    }

    void Fire()
    {
        if (projectilePrefab == null) return;

        GameObject p = Instantiate(projectilePrefab, transform.position, transform.rotation);
    }
}
