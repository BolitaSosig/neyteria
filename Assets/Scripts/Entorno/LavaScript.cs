using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaScript : MonoBehaviour
{
    public float DMG = 30;
    public float DMGrate = 0.1f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(DoDamage(other));
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StopCoroutine(DoDamage(other));
        }
    }

    public IEnumerator DoDamage(Collider2D other)
    {
        while (true) {
            other.SendMessage("GetDamageByEnemy", DMG);
            yield return new WaitForSecondsRealtime(DMGrate);
        }
    }
}
