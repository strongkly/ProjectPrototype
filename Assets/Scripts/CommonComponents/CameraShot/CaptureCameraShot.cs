using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace CrazyBox.Components.Functional
{
    [RequireComponent(typeof(Camera))]
    public class CaptureCameraShot : MonoBehaviour
    {
        Camera sourceCamera;

        private void OnEnable()
        {
            sourceCamera = GetComponent<Camera>();
        }

        public void CaptureScreenShot(UnityAction<Texture2D> callBack)
        {
            if (sourceCamera != null)
            {
                StartCoroutine(ScreenShoot(callBack));
            }
        }

        IEnumerator ScreenShoot(UnityAction<Texture2D> callBack)
        {
            yield return new WaitForEndOfFrame();
            RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24);
            sourceCamera.targetTexture = rt;
            sourceCamera.Render();
            RenderTexture.active = rt;

            Texture2D result = new Texture2D(Screen.width, Screen.height, TextureFormat.ARGB32, false);
            result.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            result.Apply();
            callBack(result);
            sourceCamera.targetTexture = null;
            RenderTexture.active = null;
            GameObject.Destroy(rt);
        }
    }
}
