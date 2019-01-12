using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class SkillData
    {
        private opType type;
        private SkillID skill_id;

        SkillData(SkillID id)
        {
            this.type = opType.skill;
            this.skill_id = id;
        }
    }
}