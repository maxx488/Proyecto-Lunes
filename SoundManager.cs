using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace MyGame
{
    public class SoundManager
    {
        private SoundPlayer background;
        private SoundPlayer explosion;
        private SoundPlayer bulletHit;
        private SoundPlayer powerUp;
        private SoundPlayer powerDown;
        private SoundPlayer respawn;

        public SoundManager()
        {
            explosion = new SoundPlayer("assets/sounds/explosion.wav");
            bulletHit = new SoundPlayer("assets/sounds/bulletHit.wav");
            powerUp = new SoundPlayer("assets/sounds/powerUp.wav");
            powerDown = new SoundPlayer("assets/sounds/powerDown.wav");
            respawn = new SoundPlayer("assets/sounds/respawn.wav");
        }

        public void PlayOnce(SoundPlayer sound)
        {
            sound.Play();
        }

        public void PlayRespawn()
        {
            respawn.Play();
        }

        public void SetPlayBackground(string sound)
        {
            background = new SoundPlayer(sound);
            background.PlayLooping();
        }

        public void StopBackground()
        {
            background.Stop();
        }

        public void SubCollisions(LevelCollider currentCollider)
        {
            currentCollider.OnCollisionSound += SoundEvent;
        }

        public void UnSubCollisions(LevelCollider currentCollider)
        {
            currentCollider.OnCollisionSound -= SoundEvent;
        }

        public void SoundEvent(string type, string state)
        {
            switch (type)
            {
                case "Enemy":
                    if (state == "hit")
                    {
                        PlayOnce(bulletHit);
                    }
                    else
                    {
                        PlayOnce(explosion);
                    }
                    break;
                case "PowerUp":
                    PlayOnce(powerUp);
                    break;
                case "Player":
                    if (state == "hit")
                    {
                        PlayOnce(powerDown);
                    }
                    else
                    {
                        PlayOnce(explosion);
                    }
                    break;
            }
        }
    }
}
