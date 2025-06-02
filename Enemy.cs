using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Enemy: GameObject, IDamagable, IPoolable
    {
        private EnemyController enemyController;
        private EnemyData enemyData;
        private EnemyPowerController powerController;
        private int faction;
        private int type;
        private bool inBounds = true;
        private bool isBoss;

        public event Action OnDisable;

        public Enemy() 
        {
            
        }

        public EnemyData GetEnemyData => enemyData;

        public EnemyPowerController PowerController => powerController;

        public int Type => type;

        public bool InBounds => inBounds;

        public bool IsBoss => isBoss;

        public bool ShootState
        {
            get
            {
                return enemyController.GetShoot;
            }
        }

        public void Initialize(Vector2 position, int fact, int typ, bool boss)
        {
            faction = fact;
            type = typ;
            isBoss = boss;
            if (enemyData == null)
            {
                enemyData = new EnemyData(type);
                transform = new Transform(position, new Vector2(enemyData.SizeX, enemyData.SizeY));
                enemyController = new EnemyController(transform, isBoss, type, enemyData.Speed);
                animationController = new AnimationController(transform, $"assets/animations/enemies/{faction}/{type}/", 5, 0.15f);
                powerController = new EnemyPowerController(enemyData.Power);
            }
            else
            {
                transform.SetPosition(position);
                enemyData.ResetData(type);
                transform.SetScale(new Vector2(enemyData.SizeX, enemyData.SizeY));
                enemyController.GetTransform.SetPosition(position);
                enemyController.GetTransform.SetScale(new Vector2(enemyData.SizeX, enemyData.SizeY));
                enemyController.ResetData(isBoss, type, enemyData.Speed);
                powerController.ResetData(enemyData.Power);
                animationController.GetTransform.SetPosition(position);
                animationController.Path = $"assets/animations/enemies/{faction}/{type}/";
                animationController.ForceAnimationUpdate();
            }
        }

        public override void Update()
        {
            enemyController.Update();
            BoundsUpdate();
            AnimationUpdate();
        }

        private void BoundsUpdate()
        {
            if (transform.Position.Y > Engine.ScreenSizeH)
            {
                inBounds = false;
            }
        }

        private void AnimationUpdate()
        {
            animationController.Update();
        }

        public void GetDamage()
        {
            PowerController.DamageEnemy();
        }

        public void Disable()
        {
            OnDisable.Invoke();
        }

        public void Reset()
        {
            inBounds = true;
        }
    }
}
