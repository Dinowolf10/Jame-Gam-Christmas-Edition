using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningCutscene : MonoBehaviour
{
    [SerializeField]
    private AudioSource openingCutsceneSoundManager;

    [SerializeField]
    private AudioClip doorbellSound;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("PlaySounds");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PlaySounds()
    {
        yield return new WaitForSeconds(6.0f);

        openingCutsceneSoundManager.PlayOneShot(doorbellSound);
    }
}
