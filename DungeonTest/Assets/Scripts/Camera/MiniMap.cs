﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour {

    public Transform player;

    private void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;


        //NOTE: Turn this on to have minimap rotate with the player
        //transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }


}
