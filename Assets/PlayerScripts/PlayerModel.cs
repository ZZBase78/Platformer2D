using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Platformer2D.Assets.PlayerScripts
{
    [CreateAssetMenu(fileName = "PlayerModel", menuName = "ScriptableObjects/Player/PlayerModel", order = 1)]
    internal sealed class PlayerModel : ScriptableObject
    {
        public float moveSpeed;
        public float jumpForce;
    }
}
