using UnityEngine;
using UnityEngine.UI;

namespace GameLogic.Widge
{

    //[RequireComponent(typeof(RectTransform))]
    public class Filling : MonoBehaviour
    {
        [SerializeField]
        RectTransform rectBg;
        [SerializeField]
        RectTransform rectFilling;
        
        //[Header("填充进度")] [SerializeField] private float fillingProgress = 0;

        /*public float FillingProgress
        {
            get { return fillingProgress; }
            set { fillingProgress = value; }
        }*/

        public void SetFillingProgress(float fillingProgress)
        {
            if (fillingProgress is > 1 or < 0)
            {
                return;
            }
            rectFilling.sizeDelta = new Vector2(rectFilling.sizeDelta.x ,rectBg.sizeDelta.y * fillingProgress);
        }
    }
}