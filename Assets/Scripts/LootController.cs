using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootController : MonoBehaviour
{
    private GameObject[] redPotions;
    private GameObject[] bluePotions;
    private GameObject[] food;
    void Start()
    {
        redPotions = GameObject.FindGameObjectsWithTag("Red Potion");
        bluePotions = GameObject.FindGameObjectsWithTag("Blue Potion");
        food = GameObject.FindGameObjectsWithTag("Food");

        foreach(GameObject loot in redPotions)
        {
            StartCoroutine("StartAnimation", loot.GetComponent<Animator>());
        }
        foreach (GameObject loot in bluePotions)
        {
            StartCoroutine("StartAnimation", loot.GetComponent<Animator>());
        }
        foreach (GameObject loot in food)
        {
            StartCoroutine("StartAnimation", loot.GetComponent<Animator>());
        }
    }

    IEnumerator StartAnimation(Animator animator)
    {
        yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
        animator.SetTrigger("StartTrigger");
    }
}
