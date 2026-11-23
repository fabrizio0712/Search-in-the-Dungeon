using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] private float tickRate;
    [SerializeField] private float tickDamage;
    [SerializeField] private float currentTickRate;
    [SerializeField] private List<PlayerController> targets;
    [SerializeField] private AudioSource audio;
    private void Start()
    {
        targets = new List<PlayerController>();
        StartCoroutine(RandomDelaySound());
    }

    private void Update()
    {
        if (Time.timeScale == 0)
        {
            if (audio.isPlaying) audio.Pause();
        }
        else
        {
            if (!audio.isPlaying) audio.UnPause();
        }
        if (targets.Count > 0)
        {
            if (currentTickRate >= tickRate)
            {
                foreach (PlayerController pc in targets)
                {
                    pc.GetDamage(tickDamage);
                }
                currentTickRate = 0;
            }
            else currentTickRate += Time.deltaTime;
        }
        else currentTickRate = tickRate;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player") 
        {
            targets.Add(other.GetComponent<PlayerController>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            targets.Remove(other.GetComponent<PlayerController>());
        }
    }
    IEnumerator RandomDelaySound() 
    {
        float timer = Random.Range(0, 10);
        yield return new WaitForSeconds(timer/10);
        audio.Play();
    }
}
