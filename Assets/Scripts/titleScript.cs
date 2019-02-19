using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titleScript : MonoBehaviour
{
    [SerializeField] private GameObject potion;

    private void Start()
    {
        potion.GetComponent<Animator>().SetTrigger("StartTrigger");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("FirstScene");
        }
    }
}
