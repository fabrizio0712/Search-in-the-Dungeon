using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    // Components
    [SerializeField] private Rigidbody rigidbody;

    // Variables
    [SerializeField] private float projectileDamage;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float lifeSpan = 10f;
    private float currentTime = 0f;
    [SerializeField] private GameObject owner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime < lifeSpan) currentTime += Time.deltaTime;
        else
        {
            currentTime = 0f;
            gameObject.SetActive(false);
        }
    }
    public void SetParent(GameObject parent) 
    {
        owner = parent;
        transform.position = owner.transform.position;
        transform.forward = owner.transform.forward;
    }
    private void OnEnable()
    {
        rigidbody.linearVelocity = transform.forward * projectileSpeed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != owner)
        {
            Debug.Log("Non Owner Projectile Trigger");
            if (other.gameObject.tag != "Player") 
            {
                Debug.Log("Non Player Projectile Trigger");
                gameObject.SetActive(false); 
            }
            else
            {
                Debug.Log("Player Projectile Trigger");
                other.gameObject.GetComponent<PlayerController>().GetDamage(projectileDamage);
                gameObject.SetActive(false);
            }
        }
    }
}
