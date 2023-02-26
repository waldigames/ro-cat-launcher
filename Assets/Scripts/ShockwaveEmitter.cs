using UnityEngine;

public class ShockwaveEmitter : MonoBehaviour
{
    public float maxDistance = 10f;
    public PlayerController pc;

    void Start()
    {
        pc = FindObjectOfType<PlayerController>();
    }
    void Update()
    {

    }
    public void TriggerShockwave(Vector3 startPoint)
    {
        Collider[] colliders = Physics.OverlapSphere(startPoint, maxDistance);

        foreach (Collider collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direction = (rb.transform.position - startPoint).normalized;
                float distance = Vector3.Distance(rb.transform.position, startPoint);
                float power = Mathf.Lerp(pc.ExplosionForce, 0f, distance / maxDistance);
                pc.MakeBoncy();
                rb.AddForce(direction * power, ForceMode.Impulse);
            }
        }
    }
}