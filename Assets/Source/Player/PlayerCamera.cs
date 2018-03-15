using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.PlaceholderName.Player {

    public class PlayerCamera : MonoBehaviour {

        public PlayerController playerController;
        public Vector3 offsetFromPlayer;

        // Update is called once per frame
        void LateUpdate() {
            transform.position = playerController.transform.position + offsetFromPlayer;
            transform.LookAt (playerController.transform);
        }
    }

}
