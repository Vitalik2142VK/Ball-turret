using System;

namespace RecorderLevel
{
    //todo It is only needed for the RecorderLevel
    public class RecorderPlayerStep : IStep
    {
        private IPlayerController _playerController;

        public RecorderPlayerStep(IPlayerController playerController)
        {
            _playerController = playerController ?? throw new ArgumentNullException(nameof(playerController));
        }

        public void Action()
        {
            _playerController.SelectTarget();
        }
    }
}
