using System;
using System.Collections.Generic;

namespace Character
{
    public class Hero:CharacterBase
    {
        public void Init(Dictionary<string, string> data, int row, int column)
        {
            this.DataFromConfig = data;
            rowIndex = row;
            colIndex = column;
            Id = int.Parse(data["Id"]);
            type = int.Parse(data["Type"]);
            attack = int.Parse(data["Attack"]);
            step = int.Parse(data["Step"]);
            maxHp = int.Parse(data["Hp"]);
            currentHp = maxHp;
        }
    }
}