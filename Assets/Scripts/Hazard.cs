using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] private Color componentColor = new Color(0.85f, 0.1f, 0.1f);

    private void Awake()
    {
        ComponentColorUtility.Apply(gameObject, componentColor);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;

        Player p = other.GetComponent<Player>();
        if (p == null) p = other.GetComponentInParent<Player>();
        if (p != null) p.Die();
    }
}
