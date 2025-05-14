using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class PlayerSpawner
    {
        private List<GameObject> objectsListToCheck;
        private AnimationController animationController;
        private bool spawn = false;
        private float noDamageTimer;

        public PlayerSpawner(List<GameObject> objectsListToCheck)
        {
            this.objectsListToCheck = objectsListToCheck;
        }

        public void Update()
        {
            if (spawn == true)
            {
                animationController.Update();
                noDamageTimer += Time.DeltaTime;
                if (noDamageTimer > 1.9f)
                {
                    noDamageTimer = 0;
                    animationController = null;
                    spawn = false;
                }

            }
        }

        public void Render()
        {
            if (animationController != null)
            {
                animationController.Render();
            }
        }

        public void GetCurrentPlayer()
        {
            for (int i = 0; i < objectsListToCheck.Count; i++)
            {
                if (objectsListToCheck[i] is Player)
                {
                    Player player = (Player) objectsListToCheck[i];
                    animationController = new AnimationController(player.GetTransform, "assets/animations/shield/", 12, 0.077f);
                    break;
                }
            }
        }

        public void Spawn()
        {
            spawn = true;
            objectsListToCheck.Add(new Player(new Vector2(400, 650)));
            GetCurrentPlayer();
        }
    }
}
