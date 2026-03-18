using CannonTurret.DamageSystem;
using System;

namespace CannonTurret.Actors.Enemies.Armored
{
    public class ArmoredEnemyPresenter : IArmoredEnemyPresenter
    {
        private readonly IArmoredObject ArmoredModel;
        private readonly IEnemy Model;
        private readonly IEnemyView View;

        public ArmoredEnemyPresenter(IEnemy model, IEnemyView view)
        {
            if (model is IArmoredObject armoredModel)
                ArmoredModel = armoredModel;
            else
                throw new ArgumentException($"<{nameof(model)}> must implement {nameof(IArmoredObject)}");

            Model = model ?? throw new ArgumentNullException(nameof(model));
            View = view ?? throw new ArgumentNullException(nameof(view));
        }

        public void IgnoreArmor(IDamageAttributes damage)
        {
            ArmoredModel.IgnoreArmor(damage);

            if (Model.IsEnable)
                View.PlayDamage();
            else
                View.PlayDead();
        }
    }
}