using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class EffectSpawner
    {
        private List<GameObject> objList;
        private GenericDynamicPool<Effect> pool;

        public EffectSpawner(List<GameObject> objList)
        {
            this.objList = objList;
            pool = new GenericDynamicPool<Effect>();
        }

        public void EffectSpawn(Vector2 position, string path, int maxIndex, float animCooldown)
        {
            Effect effect = pool.Get();
            effect.Initialize(position, path, maxIndex, animCooldown);
            objList.Add(effect);
        }
    }
}
