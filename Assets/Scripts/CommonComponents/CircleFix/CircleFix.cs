using UnityEngine;

namespace CrazyBox.Components
{
    /*                                            notice
    * both pivot of relatee and self transform has to be set to center,more generic support may export in future version.
    */
    public class CircleFix : MonoBehaviour
    {
        RectTransform relatee; //Circle RectTransform

        public void FixPos(RectTransform circleRect = null, CircleFixType fixType = CircleFixType.FixByHeight,
            float? radius = null)
        {
            transform.localPosition = GetFixedPos(circleRect, fixType, radius);
        }

        public Vector3 GetFixedPos(RectTransform circleRect = null, CircleFixType fixType = CircleFixType.FixByHeight,
            float? radius = null)
        {
            if (relatee == null)
                SetRelateeRect(circleRect);

            Vector3 rPos = circleRect.InverseTransformPoint(transform.position);
            switch (fixType)
            {
                case CircleFixType.FixByHeight:
                    rPos = FixPosByHeight(rPos);
                    break;
                case CircleFixType.FixByWidth:
                    rPos = FixPosByWidth(rPos);
                    break;
                case CircleFixType.FixByRadius:
                    rPos = FixPosByRadius(rPos, radius);
                    break;
            }
            rPos = circleRect.TransformPoint(rPos);
            rPos = transform.parent.InverseTransformPoint(rPos);

            return rPos;
        }

        Vector2 FixPosByHeight(Vector2 pos)
        {
            Vector2 quadrant = GetQuadrant(pos);
            pos.y = Mathf.Abs(pos.y);
            if (pos.y >= relatee.GetSelfHeight() / 2)
                pos.x = 0;
            else
                pos.x = Mathf.Sqrt(Mathf.Pow(relatee.GetSelfWidth() / 2, 2)
                    - Mathf.Pow(pos.y, 2));
            pos.x *= quadrant.x;
            pos.y *= quadrant.y;
            return pos;
        }

        Vector2 FixPosByWidth(Vector2 pos)
        {
            Vector2 quadrant = GetQuadrant(pos);
            pos.x = Mathf.Abs(pos.x);
            if (pos.x >= relatee.GetSelfWidth() / 2)
                pos.y = 0;
            else
                pos.y = Mathf.Sqrt(Mathf.Pow(relatee.GetSelfHeight() / 2, 2)
                    - Mathf.Pow(pos.x, 2));
            pos.x *= quadrant.x;
            pos.y *= quadrant.y;
            return pos;
        }

        Vector2 FixPosByRadius(Vector2 pos, float? radius)
        {
            float rad = radius == null ? relatee.GetSelfWidth() / 2 :
                radius.Value;

            pos = pos.normalized * rad;

            return pos;
        }

        Vector2 GetQuadrant(Vector2 pos)
        {
            Vector2 result = Vector2.zero;
            if (pos.x != 0)
            {
                result.x = pos.x > 0 ? 1 : -1;
            }
            if (pos.y != 0)
            {
                result.y = pos.y > 0 ? 1 : -1;
            }

            return result;
        }

        public void SetRelateeRect(RectTransform circleRect)
        {
            this.relatee = circleRect;
        }

        public enum CircleFixType
        {
            FixByHeight,
            FixByWidth,
            FixByRadius
        }
    }
}
