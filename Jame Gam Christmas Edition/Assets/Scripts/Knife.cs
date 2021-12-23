using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField]
    private ChoppingFoodManager choppingFoodManager;

    [SerializeField]
    private Transform slashTransform;

    [SerializeField]
    private Animator slashAnimator;

    [SerializeField]
    private float mouseX, mouseY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMouseInput();
    }

    private void UpdateMouseInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Transform collisionTransform = collision.transform;

        if (collisionTransform.gameObject.tag == "ChoppableFood" && (Mathf.Abs(mouseX) > 0 || Mathf.Abs(mouseY) > 0))
        {
            slashAnimator.SetBool("isSlashing", false);
            StopCoroutine("Slash");
            StartCoroutine("Slash");

            collisionTransform.GetComponent<ChoppableFood>().GetChopped();

            collisionTransform.GetComponent<BoxCollider2D>().enabled = false;

            collisionTransform.GetComponent<SpriteRenderer>().enabled = false;

            choppingFoodManager.RemoveFoodToChop(collisionTransform.GetComponent<Rigidbody2D>());
        }
        else if (collisionTransform.gameObject.tag == "SantaHat")
        {
            Debug.Log("Hit " + collisionTransform.gameObject.name);

            choppingFoodManager.LoseGame();
        }
    }

    private IEnumerator Slash()
    {
        Vector2 mousePos = choppingFoodManager.GetMousePosition();

        slashTransform.position = new Vector2(mousePos.x, mousePos.y);

        if (mouseX < 0 || mouseY > 0)
        {
            slashTransform.rotation = Quaternion.Euler(0, 0, 180f);
        }

        //float angle = Mathf.Atan2(slashTransform.position.y, slashTransform.position.x) * Mathf.Rad2Deg;

        //slashTransform.rotation = Quaternion.Euler(0, 0, angle);

        slashAnimator.SetBool("isSlashing", true);

        yield return new WaitForSeconds(0.25f);

        slashAnimator.SetBool("isSlashing", false);
    }
}
