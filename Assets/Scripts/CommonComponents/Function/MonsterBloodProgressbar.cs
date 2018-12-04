using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace CrazyBox.Components.Functional
{
    public class MonsterBloodProgressbar : MonoBehaviour
    {
        public RectTransform FrontRectsParent = null;
        public RectTransform ValueChangeFlashFront = null;

        [SerializeField]
        float duration = 1f;
        [SerializeField]
        Ease ease = Ease.Linear;

        float value;
        float originWidth; //, originHeight;
        int layer = -1;
        List<RectTransform> fronts;
        Transform valueChangeFlashFrontParent;

        private void Awake()
        {
            if (FrontRectsParent != null)
            {
                fronts = new List<RectTransform>();
                for (int i = 0; i < FrontRectsParent.childCount; i++)
                    fronts.Add(FrontRectsParent.GetChild(i) as RectTransform);
                if (FrontRectsParent.childCount > 0)
                {
                    originWidth = fronts[0].sizeDelta.x;
                    //originHeight = fronts[0].sizeDelta.y;
                }
            }

            if (valueChangeFlashFrontParent == null)
            {
                if (ValueChangeFlashFront != null)
                    valueChangeFlashFrontParent = ValueChangeFlashFront.parent;
            }
        }

        #region set value without animation

        public void SetValue(int layer, float value)
        {
            if (layer > 0)
            {
                StopAniAndRecoverFlashRect();
                this.value = Mathf.Clamp01(value);
                this.layer = layer;
                ManageActive(layer);
                ManageOrder(layer);
                ManageSize(layer);
            }
        }

        void ManageActive(int layer)
        {
            layer = layer - 1;
            for (int i = 0; i < fronts.Count; i++)
            {
                fronts[i].gameObject.SetActive(layer >= i);
            }
        }

        void ManageOrder(int layer)
        {
            int frontIdx = GetFrontIdxByLayer(layer);
            for (int i = GetCyclePre(frontIdx);
            i != frontIdx; i = GetCyclePre(i))
            {
                fronts[i].SetAsFirstSibling();
            }
        }

        int GetCyclePre(int curIdx)
        {
            int result;
            if (curIdx == 0)
                result = fronts.Count - 1;
            else
                result = curIdx - 1;
            return result;
        }

        void ManageSize(int layer)
        {
            RectTransform rect = fronts[GetFrontIdxByLayer(layer)];
            rect.transform.SetAsLastSibling();
            foreach (RectTransform front in fronts)
            {
                if (rect != front)
                    front.sizeDelta = new Vector2(originWidth, front.sizeDelta.y);
            }
            rect.sizeDelta = new Vector2(originWidth * value, rect.sizeDelta.y);
        }

        int GetFrontIdxByLayer(int layer)
        {
            int result = -1;
            if (layer > 0)
            {
                layer = layer - 1;
                result = layer % fronts.Count;
            }
            return result;
        }

        void StopAniAndRecoverFlashRect()
        {
            seq.Kill();
            RecoverChangeFlashRect();
        }

        #endregion

        #region set value with animation
        Sequence seq;

        public void SetValueWithAnimation(int layer, float value, float duration = 1f)
        {
            value = Mathf.Clamp01(value);
            this.duration = duration;
            int originalLayer = this.layer;
            float originalValue = this.value;
            SetValue(layer, value);
            seq.Kill();
            if (IsDecline(layer, value, originalLayer, originalValue))
                seq = ConstructDeclineAniSeq(originalLayer, originalValue, this.layer, this.value);
            else
                seq = ConstructIncreseAniSeq(originalLayer, originalValue, this.layer, this.value);
        }

        Sequence ConstructDeclineAniSeq(int originalLayer, float originalValue,
        int destLayer, float destValue)
        {
            int totalLayerChange = originalLayer - destLayer;
            Sequence seq = DOTween.Sequence();
            //RectTransform FrontRectsParent = this.FrontRectsParent;
            PrepareChangeFlashRect();

            #region 第一层
            if (totalLayerChange > 0)//至少有第一层
            {
                seq.AppendCallback(() =>
                {
                //将白条设置到一层
                ValueChangeFlashFront.SetSiblingIndex(FrontRectsParent.childCount - 1);
                });
                //第一层动画//将白条长度设置到原始值
                seq.Append(DOTween.To(() =>
                {
                    return new Vector2(originalValue * originWidth, ValueChangeFlashFront.sizeDelta.y);
                },
                (v) =>
                {
                    ValueChangeFlashFront.sizeDelta = v;
                },
                new Vector2(0, ValueChangeFlashFront.sizeDelta.y), this.duration));
            }
            #endregion

            #region 第二层
            if (totalLayerChange > 1)//有第二层
            {
                //第二层动画 //将白条长度设置到1
                seq.Append(DOTween.To(() =>
                {
                    return new Vector2(originWidth, ValueChangeFlashFront.sizeDelta.y);
                },
                (v) =>
                {
                    ValueChangeFlashFront.sizeDelta = v;
                },
                new Vector2(0, ValueChangeFlashFront.sizeDelta.y), this.duration));
            }
            #endregion

            #region 最后一层
            seq.AppendCallback(() =>
            {
                ValueChangeFlashFront.SetSiblingIndex(Mathf.Clamp(FrontRectsParent.childCount - 2,
                    0, FrontRectsParent.childCount - 1));
            });
            seq.Append(DOTween.To(() =>
            {
                if (totalLayerChange > 0)
                {
                    return new Vector2(originWidth, ValueChangeFlashFront.sizeDelta.y);
                }
                else
                {
                    return new Vector2(originalValue * originWidth, ValueChangeFlashFront.sizeDelta.y);
                }
            },
            (v) =>
            {
                ValueChangeFlashFront.sizeDelta = v;
            }, new Vector2(destValue * originWidth, ValueChangeFlashFront.sizeDelta.y), this.duration));
            #endregion

            seq.AppendCallback(() =>
            {
                RecoverChangeFlashRect();
            });
            seq.SetEase(ease);

            return seq;
        }

        Sequence ConstructIncreseAniSeq(int originalLayer, float originalValue,
        int destLayer, float destValue)
        {
            int totalLayerChange = destLayer - originalLayer;
            Sequence seq = DOTween.Sequence();

            PrepareChangeFlashRect();

            #region 第一层
            if (totalLayerChange > 0)
            {
                int firstLayerBarIdx = GetFrontIdxByLayer(originalLayer);
                seq.AppendCallback(() =>
                {
                    ValueChangeFlashFront.SetSiblingIndex(0);
                    for (int i = 0; i < fronts.Count; i++)
                    {
                        if (firstLayerBarIdx != i)
                            fronts[i].gameObject.SetActive(false);
                    }
                    SetBarRectWithValue(ValueChangeFlashFront, 1f);
                });
                //第一层动画 //将第一层血条追赶到满血
                seq.Append(DOTween.To(() =>
                {
                    return new Vector2(originalValue * originWidth, ValueChangeFlashFront.sizeDelta.y);
                }, (v) =>
                {
                    fronts[firstLayerBarIdx].sizeDelta = v;
                }, new Vector2(originWidth, ValueChangeFlashFront.sizeDelta.y), this.duration));
            }
            #endregion

            #region 第二层
            if (totalLayerChange > 1) //有第二层
            {
                int lastLayerBarIdx = GetFrontIdxByLayer(layer - 1);
                seq.AppendCallback(() =>
                {
                //将所有血条隐藏，只显示倒数第二层血条
                for (int i = 0; i < fronts.Count; i++)
                    {
                        if (lastLayerBarIdx != i)
                            fronts[i].gameObject.SetActive(false);
                        else
                            fronts[i].gameObject.SetActive(true);
                    }
                    SetBarRectWithValue(ValueChangeFlashFront, 1f);
                });
                //第二层动画，将倒数第二层血条追赶到满血
                seq.Append(DOTween.To(() =>
                {
                    return new Vector2(0, ValueChangeFlashFront.sizeDelta.y);
                }, (v) =>
                 {
                     fronts[lastLayerBarIdx].sizeDelta = v;
                 },
                new Vector2(originWidth, ValueChangeFlashFront.sizeDelta.y), this.duration));
            }
            #endregion

            #region 最后一层

            seq.AppendCallback(() =>
            {
            //显示当前血条状态
            ManageActive(layer);
            //将白条设置到第二层以显示最后的追赶动画
            ValueChangeFlashFront.SetSiblingIndex(Mathf.Clamp(FrontRectsParent.childCount - 2,
                    0, FrontRectsParent.childCount - 1));
                SetBarRectWithValue(ValueChangeFlashFront, destValue);
            });
            seq.Append(DOTween.To(() =>
            {
                if (totalLayerChange > 0)
                {
                    return new Vector2(0, ValueChangeFlashFront.sizeDelta.y);
                }
                else
                {
                    return new Vector2(originalValue * originWidth, ValueChangeFlashFront.sizeDelta.y);
                }
            }, (v) =>
             {
                 fronts[GetFrontIdxByLayer(layer)].sizeDelta = v;
             }, new Vector2(destValue * originWidth, ValueChangeFlashFront.sizeDelta.y), this.duration));

            #endregion

            seq.AppendCallback(() =>
            {
                RecoverChangeFlashRect();
            });
            seq.SetEase(ease);

            return seq;
        }

        void PrepareChangeFlashRect()
        {
            ValueChangeFlashFront.SetParent(FrontRectsParent, true);
            ValueChangeFlashFront.gameObject.SetActive(true);
            SetBarRectWithValue(ValueChangeFlashFront, 1f);
        }

        void RecoverChangeFlashRect()
        {
            ValueChangeFlashFront.SetParent(valueChangeFlashFrontParent, true);
            ValueChangeFlashFront.gameObject.SetActive(false);
            SetBarRectWithValue(ValueChangeFlashFront, 1f);
        }

        void SetBarRectWithValue(RectTransform rect, float value)
        {
            rect.sizeDelta = new Vector2(value * originWidth, rect.sizeDelta.y);
        }

        bool IsDecline(int layer, float value, int oriLayer, float oriValue)
        {
            bool result = false;
            if (layer < oriLayer)
                result = true;
            else if (layer == oriLayer)
            {
                if (value < oriValue)
                    result = true;
            }
            return result;
        }
        #endregion
    }
}