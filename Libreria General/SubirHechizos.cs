using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueSharp.Common
{
    public static class SubirHechizos
    {
        private static int[] _HechizoPrioritario;
        private static int _SiguienteNivel = 0;
        private static Dictionary<string, int[]> HechizoPrioritarioLista = new Dictionary<string, int[]>();
        private static Menu _Menu;
        private static int _PrioridadSeleccionada;

        public static void AddToMenu(Menu menu)
        {
            _Menu = menu;
            if (HechizoPrioritarioLista.Count > 0)
            {
                _Menu.AddItem(new MenuItem(ObjectManager.Player.ChampionName, "Activado").SetValue(true));
                _Menu.AddItem(new MenuItem(ObjectManager.Player.ChampionName, "").SetValue(new StringList(HechizoPrioritarioLista.Keys.ToArray())));
                _PrioridadSeleccionada = _Menu.Item(ObjectManager.Player.ChampionName).GetValue<StringList>().SelectedIndex;
            }
        }

        public void Add(string spellPriorityDesc, int[] spellPriority)
        {
            HechizoPrioritarioLista.Add(spellPriorityDesc, spellPriority);
        }

        public void Update()
        {
            if (HechizoPrioritarioLista.Count == 0 || !_Menu.Item(ObjectManager.Player.ChampionName).GetValue<bool>() ||
                _SiguienteNivel == ObjectManager.Player.Level)
            {
                return;
            }

            _HechizoPrioritario = HechizoPrioritarioLista.Values.ElementAt(_PrioridadSeleccionada);

            var qL = ObjectManager.Player.Spellbook.GetSpell(SpellSlot.Q).Level;
            var wL = ObjectManager.Player.Spellbook.GetSpell(SpellSlot.W).Level;
            var eL = ObjectManager.Player.Spellbook.GetSpell(SpellSlot.E).Level;
            var rL = ObjectManager.Player.Spellbook.GetSpell(SpellSlot.R).Level;

            if (qL + wL + eL + rL >= ObjectManager.Player.Level)
            {
                return;
            }

            var level = new[] { 0, 0, 0, 0 };

            for (var i = 0; i < ObjectManager.Player.Level; i++)
            {
                level[_HechizoPrioritario[i] - 1] = level[_HechizoPrioritario[i] - 1] + 1;
            }

            if (qL < level[0])
            {
                ObjectManager.Player.Spellbook.LevelUpSpell(SpellSlot.Q);
            }
            if (wL < level[1])
            {
                ObjectManager.Player.Spellbook.LevelUpSpell(SpellSlot.W);
            }
            if (eL < level[2])
            {
                ObjectManager.Player.Spellbook.LevelUpSpell(SpellSlot.E);
            }
            if (rL < level[3])
            {
                ObjectManager.Player.Spellbook.LevelUpSpell(SpellSlot.R);
            }
        }
    }
}
