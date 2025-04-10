namespace Logic.VIew2D
{
    public class MealSequenceView : SequenceSprite
    {
        private float fillingProgress = 0;

        public float FillingProgress
        {
            get => fillingProgress;
            set
            {
                fillingProgress = value;
                if (fillingProgress > 1)
                {
                    fillingProgress = 1;
                }

                if (fillingProgress > CurProgress)
                {
                    GoNextSprite();
                }
            }
        }
    }
}