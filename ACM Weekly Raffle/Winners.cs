using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM_Weekly_Raffle
{
    class Winners
    {
        private List<string> winnersAvailable = new List<string>();
        private List<string> previousWinners = new List<string>();
        private Random rand = new Random();

        public Winners()
        {
            if (System.IO.File.Exists("winnersAvail.txt"))
            {
                string[] winners = System.IO.File.ReadAllLines("winnersAvail.txt");
                foreach (string winner in winners)
                {
                    winnersAvailable.Add(winner);
                }
            }

            if (System.IO.File.Exists("previousWinner.txt"))
            {
                string[] oldWinners = System.IO.File.ReadAllLines("previousWinner.txt");
                foreach (string winner in oldWinners)
                {
                    previousWinners.Add(winner);
                }
            }
        }

        public void AddWinner()
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Enter a winner name", "Add Winner");
            if (!this.winnersAvailable.Contains(input))
            {
                this.winnersAvailable.Add(input);
            }
        }

        public string selectWinner()
        {
            if(this.winnersAvailable.Count < 1)
            {
                return null;
            }
            int selection = rand.Next(0, this.winnersAvailable.Count);
            string winner = this.winnersAvailable[selection];
            this.previousWinners.Add(winner);
            this.winnersAvailable.RemoveAt(selection);

            return winner;
        }

        public void reset()
        {
            foreach(string winner in this.previousWinners)
            {
                this.winnersAvailable.Add(winner);
            }
            this.previousWinners.Clear();
        }

        public void Save()
        {
            System.IO.File.WriteAllLines("winnersAvail.txt", winnersAvailable.ToArray());
            System.IO.File.WriteAllLines("previousWinner.txt", previousWinners.ToArray());
        }
    }
}
