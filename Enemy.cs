using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Enemy: GameObject, IDamagable
    {
        private EnemyController enemyController;
        private EnemyData enemyData;
        private EnemyPowerController powerController;
        private int faction;
        private int type;
        private bool inBounds = true;
        private bool isBoss;

        public Enemy(Vector2 position,int fact,int typ, bool boss) 
        {
            faction = fact;
            type = typ;
            isBoss = boss;
            enemyData = new EnemyData(type);
            transform = new Transform(position, new Vector2(enemyData.SizeX, enemyData.SizeY));
            enemyController = new EnemyController(transform, isBoss, type, enemyData.Speed);
            animationController = new AnimationController(transform, $"assets/animations/enemies/{faction}/{type}/", 5, 0.15f);
            powerController = new EnemyPowerController(enemyData.Power);
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

        public override void Update()
        {
            enemyController.Update();
            BoundsUpdate();
            AnimationUpdate();
        }

        private void BoundsUpdate()
        {
            if (transform.Position.Y > 768)
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
    }
}
