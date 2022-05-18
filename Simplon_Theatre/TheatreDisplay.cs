using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplon_Theatre
{
    // Pour alléger ma classe Theatre, je place la logique de l'affichage dans un autre classe. Pas convaincu par l'intérêt de la manoeuvre ceci dit dans ce cas précis.
    public class TheatreDisplay
    {
        // METHODE POUR AFFICHER LES PLACES
        public static void Display(int rows, int seats, List<int[]> seatsList)
        {
            for (int i = 0; i < rows; i++)
            {
                // J'utilise StringBuilder par soucis de performance. J'évite d'occuper inutilement de la mémoire et de faire travailler le Garbage Collector.
                StringBuilder newRow = new StringBuilder();
                newRow.Append(i);
                for (int j = 0; j < seats; j++)
                {
                    int[] seat = seatsList.Find(element => element[0] == i && element[1] == j);

                    if (seat[2] == 0)
                        newRow.Append("[ ]");
                    else
                        newRow.Append("[X]");
                }
                Console.WriteLine(newRow);

                
            }
            StringBuilder seatsNumberDisplay = new StringBuilder();
            seatsNumberDisplay.Append(" ");
            for (int i = 0; i < seats; i++)
            {
                seatsNumberDisplay.Append($" {i} ");
            }
            Console.WriteLine(seatsNumberDisplay);
        }
    }
}
