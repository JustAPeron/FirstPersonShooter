using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    //Variables
    
    #region Variables

    [Header("Bullet Statistics")]
    public float lifespan;

    private float timeActive;

    private float damage;

    public float Damage { get => damage; set => damage = value; }
    #endregion



    private void OnEnable()
    {
        timeActive = Time.time;
    }

    private void OnDisable()
    {
        gameObject.transform.position = new Vector3(0, -2000, 0);
    }

    private void Update()
    {
        if (Time.time - timeActive >= lifespan)
            gameObject.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {
        //gameObject.SetActive(false);

        // TODO collisions with Objects
        if (other.CompareTag("Enemy"))
            other.GetComponent<EnemyController>().TakeDamage(damage);
        
        else if (other.CompareTag("Player"))
            other.GetComponent<PlayerController>().TakeDamage(damage);
    }
}
