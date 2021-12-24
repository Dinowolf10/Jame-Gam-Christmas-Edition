using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningCutscene : MonoBehaviour
{
    [SerializeField]
    private AudioSource openingCutsceneSoundManager;

    [SerializeField]  private AudioClip doorbellSound;
    [SerializeField] private AudioClip yawnSound;
    [SerializeField] private AudioClip huhSound;
    [SerializeField] private AudioClip shockedSound;
    [SerializeField] private AudioClip ohCrapSound;
    [SerializeField] private AudioClip uhOhSounds;

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
        openingCutsceneSoundManager.PlayOneShot(yawnSound);

        yield return new WaitForSeconds(2.0f);

        openingCutsceneSoundManager.PlayOneShot(huhSound);

        yield return new WaitForSeconds(2.0f);

        openingCutsceneSoundManager.PlayOneShot(shockedSound);

        yield return new WaitForSeconds(2.0f);

        openingCutsceneSoundManager.PlayOneShot(doorbellSound);

        yield return new WaitForSeconds(2.0f);

        openingCutsceneSoundManager.PlayOneShot(uhOhSounds);

        yield return new WaitForSeconds(2.0f);

        openingCutsceneSoundManager.PlayOneShot(ohCrapSound);
    }
}
