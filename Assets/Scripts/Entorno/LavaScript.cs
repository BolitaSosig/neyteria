using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaScript : MonoBehaviour
{
    GameObject player;

    public float DMG = 30;
    public float DMGrate = 0.1f;

    public bool isPlayer = false;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayer = true;
            StartCoroutine(DoDamage());
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayer = false;
            StopCoroutine(DoDamage());
        }
    }

    public IEnumerator DoDamage()
    {
        while (isPlayer && player.GetComponent<PlayerController>().HP > 0) {
            player.SendMessage("GetDamageByEnemy", DMG);
            yield return new WaitForSecondsRealtime(DMGrate);
        }
    }

    void Pause(bool p)
    {
        foreach(Animator a in transform)
            a.enabled = !p;
    }
}
