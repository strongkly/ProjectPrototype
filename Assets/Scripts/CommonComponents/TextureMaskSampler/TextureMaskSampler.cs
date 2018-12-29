using UnityEngine;

namespace CrazyBox.Components
{
    [RequireComponent(typeof(RectTransform))]
    public class TextureMaskSampler : MonoBehaviour
    {
        [SerializeField]
        protected Texture2D TextureMask;
        protected RectTransform rtf;

        public void Init()
        {
            rtf = transform.GetComponent<RectTransform>();
        }

        public bool IsBlocked(Vector3 clickWorldPos, float threshold = 1)
        {
            clickWorldPos = rtf.InverseTransformPoint(clickWorldPos);
            clickWorldPos.x /= rtf.rect.width;
            clickWorldPos.y /= rtf.rect.height;
            float a = TextureMask.GetPixel(Mathf.RoundToInt(clickWorldPos.x * TextureMask.width),
                Mathf.RoundToInt(clickWorldPos.y * TextureMask.height)).a;

            return a >= threshold;
        }
    }
}
