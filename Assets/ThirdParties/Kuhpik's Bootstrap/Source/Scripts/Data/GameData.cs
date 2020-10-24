using UnityEngine;

namespace Kuhpik
{
    /// <summary>
    /// Used to store game data. Change it the way you want.
    /// </summary>
    public class GameData {
        private Rigidbody boatRigidbody;
        private Transform boatTransform;
        
        private Camera mainCamera;
        private Transform mainCameraTransform;

        public Rigidbody BoatRigidbody => boatRigidbody;
        public Transform BoatTransform => boatTransform;
        public Camera MainCamera => mainCamera;
        public Transform MainCameraTransform => mainCameraTransform;

        public void SetVariables(Rigidbody boatRigidbody, Camera mainCamera) {
            this.boatRigidbody = boatRigidbody;
            boatTransform = boatRigidbody.transform;

            this.mainCamera = mainCamera;
            mainCameraTransform = mainCamera.transform;
        }
    }
}