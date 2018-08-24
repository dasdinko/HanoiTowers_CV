using DG.Tweening;
using UnityEngine;

namespace HanoiTowers.Scripts.Game.Components
{
    public class DiscComponent : MonoBehaviour
    {
        [SerializeField] SpriteRenderer _sprite;



        public void Init(float width, Color color)
        {
            _sprite.transform.localScale = new Vector3(width, _sprite.transform.localScale.y);
            _sprite.color = color;
        }
        
        public void Animate(float yPickPosition, Vector3 endPosition, float time)
        {
            Sequence sequence = DOTween.Sequence();

            float operationTime = time / 3;

            sequence.Append(transform.DOLocalMoveY(yPickPosition, operationTime));
            sequence.Append(transform.DOLocalMoveX(endPosition.x, operationTime));
            sequence.Append(transform.DOLocalMoveY(endPosition.y, operationTime));

            sequence.SetAutoKill();

        }
    }
}