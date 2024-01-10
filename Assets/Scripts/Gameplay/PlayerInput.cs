using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerInput : MonoBehaviour
{
    private Player player;
    private float horizontal, vertical;
    private Vector2 loookTarget;

    // Start is called before the first frame update
    private void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!GameManager.GetInstance().IsPlaying())
            return;

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        loookTarget = Input.mousePosition;

        player.AttackTimer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
            player.UseWeapon();

        if (Input.GetMouseButtonDown(1))
            player.Nuke();
    }

    private void FixedUpdate()
    {
        player.Move(new Vector2(horizontal, vertical), loookTarget);
    }
    //should be added to PLayer Object
}
