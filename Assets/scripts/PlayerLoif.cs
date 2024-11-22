using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLoif : MonoBehaviour
{
    private bool dead = false;

    [SerializeField] AudioSource deathSound;

    private void Update()
    {
        if (transform.position.y < -1f && !dead)
        {
            MarrShaashika();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            MarrShaashika();
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Movement>().enabled = false;
        }
    }

    private void MarrShaashika()
    {
        
        Invoke(nameof(ReloadLevel), 1.3f);
        dead = true;
        Debug.Log("maar gaya");
        deathSound.Play();

        //dazee kay moo destroy() method onn use kaway shwa destroy(gameobject) kho dee method sa harsa remove shee
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
