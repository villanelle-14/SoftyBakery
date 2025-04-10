using DataModel.BakeOperation;
using UnityEngine;

namespace GameLogic.Widge
{
    public class BeatItem2D : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer leftUnHit;
        [SerializeField] private SpriteRenderer leftHit;
        [SerializeField] private SpriteRenderer rightUnHit;
        [SerializeField] private SpriteRenderer rightHit;

        private BeatInput selfBeat;

        public BeatInput SetBeat
        {
            set
            {
                selfBeat = value;
                ShowBeat(selfBeat);
            }
        }

        public bool Hit(BeatInput hit)
        {
            if (hit == selfBeat)
            {
                if (selfBeat == BeatInput.left)
                {
                    ShownLeft(true);
                }else if (selfBeat == BeatInput.right)
                {
                    ShownRight(true);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        
        void ShowBeat(BeatInput beat)
        {
            Clear();
            if (beat == BeatInput.left)
            {
                ShownLeft(false);
            }
            else
            {
                ShownRight(false);
            }
        }
        
        void ShownLeft(bool isHit)
        {
            leftUnHit.enabled = !isHit;
            leftHit.enabled = isHit;
        }        
        void ShownRight(bool isHit)
        {
            rightUnHit.enabled = !isHit;
            rightHit.enabled = isHit;
        }

        void Clear()
        {
            leftUnHit.enabled = false;
            leftHit.enabled = false;
            rightUnHit.enabled = false;
            rightHit.enabled = false;
        }
    }
}