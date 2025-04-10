using UnityEngine;

namespace Logic.VIew2D
{
    public class HandController : MonoBehaviour
    {
        private HandTool selfMeal;
        public void SetTool(HandTool meal)
        {
            if (selfMeal != null)
            {
                Destroy(selfMeal.gameObject);
            }

            selfMeal = meal;
            if (meal != null)
            {
                selfMeal.transform.parent = transform;                
            }

        }
        public void Hit(float progress)
        {
            //selfMeal.FillingProgress = progress;
            selfMeal.Hit();
        }
    }
}