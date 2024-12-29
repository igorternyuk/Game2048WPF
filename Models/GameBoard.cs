using Game2048.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048.Models
{
    internal class GameBoard
    {
        public readonly int BoardSize = 4;
        public readonly int WinValue = 2048;

        public int[,] board;
        public int score;

        public int[,] Board { get => board; set => board =value; }
        public int Score { get => score; set => score = value; }

    }
}
