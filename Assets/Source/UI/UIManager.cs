using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.PlaceholderName.UI {

    public class UIManager : MonoBehaviour {

        public static Canvas mainCanvas;
        public static GraphicRaycaster mainCanvasRaycaster;

        private static List<UIWindow> currentWindows = new List<UIWindow> ();

        // Use this for initialization
        void Awake() {
            mainCanvas = GameObject.Find ("MainCanvas").GetComponent<Canvas> ();
            mainCanvasRaycaster = mainCanvas.GetComponent<GraphicRaycaster> ();
        }

        void Update() {
            if (Input.GetKeyDown (KeyCode.Escape))
                CloseAllWindows ();
        }

        public static void AddWindow (UIWindow window) {
            currentWindows.Add (window);
        }

        public static void RemoveWindow (UIWindow window) {
            currentWindows.Remove (window);
        }

        private void CloseAllWindows () { // Since closing a Window also removes it from a list, enumeration cannot be used. Instead use a while-loop.
            while (currentWindows.Count != 0)
                currentWindows [ 0 ].Close ();
        }

        public static T CreateFromResource<T> (string path, Vector3 position) where T : MonoBehaviour {
            GameObject prefab = Resources.Load<GameObject> (path);
            T ui = Instantiate (prefab, position, Quaternion.identity).GetComponent<T> ();
            ui.transform.SetParent (mainCanvas.transform, true);
            return ui;
        }
    }

}