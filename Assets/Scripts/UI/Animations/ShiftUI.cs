using UnityEngine;

namespace CannonTurret.UI.Animations
{
    public struct ShiftUI
    {
        private Vector2 _finishPosition;
        private float _offsetShift;

        public Vector2 GetStartPosition(Direction direction, Vector2 finishPosition, float offsetShift)
        {
            if (_offsetShift < 0)
                throw new System.ArgumentOutOfRangeException(nameof(offsetShift));

            _finishPosition = finishPosition;
            _offsetShift = offsetShift;

            switch (direction)
            {
                case Direction.up:
                    return GetUp();

                case Direction.down:
                    return GetDown();

                case Direction.left:
                    return GetLeft();

                case Direction.right:
                    return GetRight();

                default:
                    return GetDown();
            }
        }

        readonly private Vector2 GetUp()
        {
            float shift = Screen.height * _offsetShift;

            return new Vector2(_finishPosition.x, shift);
        }

        readonly private Vector2 GetDown()
        {
            float shift = -Screen.height * _offsetShift;

            return new Vector2(_finishPosition.x, shift);
        }

        readonly private Vector2 GetLeft()
        {
            float shift = Screen.width * _offsetShift;

            return new Vector2(shift, _finishPosition.y);
        }

        readonly private Vector2 GetRight()
        {
            float shift = -Screen.width * _offsetShift;

            return new Vector2(shift, _finishPosition.y);
        }

        public enum Direction
        {
            up,
            down,
            left,
            right
        }
    }
}