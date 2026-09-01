using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : MonoBehaviour
{
    // Components
    [SerializeField] private GameObject body;
    private FieldOfView fieldOfView;

    // Attack Elements
    [SerializeField] private GameObject projectile;
    [SerializeField] private List<GameObject> projectilePool = new List<GameObject>();
    
    // Attack Variables
    [SerializeField] private float attackCooldown;
    private float currentAttackTime = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fieldOfView = GetComponent<FieldOfView>();
        /*GameObject firstPoolObject = Instantiate(projectile);
        firstPoolObject.GetComponent<EnemyProjectile>().SetParent(body);
        firstPoolObject.SetActive(false);
        projectilePool.Add(firstPoolObject);*/
    }

    // Update is called once per frame
    void Update()
    {
        if (fieldOfView.HasVisualTarget)
        {
            body.transform.forward = fieldOfView.GetTarget().bounds.center - body.transform.position;
            if (currentAttackTime < attackCooldown) currentAttackTime += Time.deltaTime;
            else
            {
                Attack();
                currentAttackTime = 0;
            }
        }
        else
        {
            currentAttackTime = 0;
            transform.RotateAroundLocal(Vector3.up, 5 * Time.deltaTime);
        }
    }
    private GameObject GetPoolObject() 
    {
        for (int i = 0; i < projectilePool.Count; i++) 
        {
            if (!projectilePool[i].activeInHierarchy) 
            {
                return projectilePool[i];
            }
        }
        GameObject newObjectInPool = Instantiate(projectile);
        newObjectInPool.SetActive(false);
        newObjectInPool.GetComponent<EnemyProjectile>().SetParent(body);
        projectilePool.Add(newObjectInPool);
        return newObjectInPool;
    }

    private void Attack() 
    {
        GameObject currentProjectile = GetPoolObject();
        currentProjectile.transform.position = body.transform.position;
        currentProjectile.transform.forward = body.transform.forward;
        currentProjectile.SetActive(true);
    }
}
