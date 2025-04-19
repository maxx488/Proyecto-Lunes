using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Enemy
    {
        private EnemyController enemyController;
        private Transform enemyTransform;
        private AnimationController animationController;
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
            enemyTransform = new Transform(position);
            enemyData = new EnemyData(type);
            enemyController = new EnemyController(enemyTransform, isBoss, type, enemyData.Speed);
            animationController = new AnimationController(enemyTransform, $"assets/animations/enemies/{faction}/{type}/", 5, 0.15f);
            powerController = new EnemyPowerController(enemyData.Power);
        }

        public Transform GetEnemyTransform => enemyTransform;

        public EnemyData GetEnemyData => enemyData;

        public EnemyPowerController PowerController => powerController;

        public bool InBounds => inBounds;

        public bool IsBoss => isBoss;

        public bool ShootState
        {
            get
            {
                return enemyController.GetShoot;
            }
        }

        public void Update()
        {
            enemyController.Update();
            BoundsUpdate();
            AnimationUpdate();
        }

        public void Render()
        {
            AnimationRender();
        }

        private void BoundsUpdate()
        {
            if (enemyTransform.Position.Y > 768)
            {
                inBounds = false;
            }
        }

        private void AnimationRender()
        {
            animationController.Render();
        }

        private void AnimationUpdate()
        {
            animationController.Update();
        }
    }
}
