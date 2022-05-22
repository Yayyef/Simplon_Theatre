using System.Text;

namespace Simplon_Theatre
{
    public class Theatre
    {
        // Les deux propriétés suivantes sont utilisées pour la visualisation du cinéma.
        public int Rows { get; set; }
        public int Seats { get; set; }

        // Je stocke mes données sur les places dans une liste de tableaux.
        public List<int[]> SeatsList { get; set; } = new List<int[]>();

        // Constructeur
        public Theatre(int rows, int seatsPerRow)
        {
            Rows = rows;
            Seats = seatsPerRow;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < seatsPerRow; j++)
                {
                    // Je créé mes sièges. La troisième valeur du tableau représente la disponibilité du siège. 0 pour indisponible, 1 pour disponible.
                    SeatsList.Add(new int[] { i, j, 0 });
                }
            }
        }

        public static void Launch()
        {
            var simplonTheatre = new Theatre(8, 9);

            Console.WriteLine("*******Bonjour et Bienvenue au Simplon Theatre!*******");
            Thread.Sleep(2000);
            Console.Clear();

            simplonTheatre.AskUserInput();

        }

        // METHODE POUR RESERVER LES PLACES
        public void BookSeats(string row, string numberOfSeats)
        {
            // Je vérifie que l'utilisateur à entré un entier ne dépassant le cadre de la salle
            if (int.Parse(row) > Rows || int.Parse(numberOfSeats) > Seats || !int.TryParse(numberOfSeats, out _) || !int.TryParse(row, out _))
            {
                Console.Clear();
                Console.WriteLine("Entrez une valeur valide!");
                return;
            }

            // Une liste d'index me permet de les garder en mémoire pour réinitialiser les sièges si la réservation échoue.
            List<int> allSeatsIndexes = new List<int>();
            // Ce booléen me permet de vérifier si la réservation à été ou non un succès
            bool BookingSuccess = true;

            for (int i = 0; i < int.Parse(numberOfSeats); i++)
            {
                try
                {
                    // Je récupère l'index de mon siège pour signaler qu'il est réservé.
                    int seatIndex = SeatsList.FindIndex(seat => seat[0] == int.Parse(row) && seat[2] == 0);
                    SeatsList[seatIndex][2] = 1;

                    allSeatsIndexes.Add(seatIndex);
                }
                catch
                {
                    // Si j'ai une OutOfRangeException je sors de la boucle.
                    BookingSuccess = false;
                    break;
                }
            }

            // En cas d'insuccès, je libère les places de la rangée.
            if (BookingSuccess == false)
            {
                Console.Clear();
                Console.WriteLine("Il n'y a plus assez de places disponibles sur cette rangée.");

                foreach (int index in allSeatsIndexes)
                    SeatsList[index][2] = 0;

            }
            else
            {
                Console.Clear();
                Console.WriteLine("Merci pour votre réservation!");
            }
                
        }

        // DEMANDE LES ENTREES UTILISATEURS
        public void AskUserInput()
        {
            TheatreDisplay.Display(Rows, Seats, SeatsList);
            Console.WriteLine();
            Console.WriteLine("A quelle rangée désirez vous être placé?");
            string row = Console.ReadLine();
            Console.Clear();

            TheatreDisplay.Display(Rows, Seats, SeatsList);

            Console.WriteLine();
            Console.WriteLine("Combien de places désirez vous réserver?");
            string seats = Console.ReadLine();

            BookSeats(row, seats);
            TheatreDisplay.Display(Rows, Seats, SeatsList);
            AskBookAgain();
        }
        
        // METHODE POUR DEMANDER SI L'UTILISATEUR VEUT EFFECTUER UNE NOUVELLE RESERVATION
        private void AskBookAgain()
        {
            Console.WriteLine("Désirez-vous réserver à nouveau? Y/N");
            string userAnswer = Console.ReadLine();
            if (userAnswer.ToLower().Trim() != "y" && userAnswer.ToLower().Trim() != "n")
            {
                Console.WriteLine("Je ne comprends pas votre réponse.");
                AskBookAgain();
            }
            else
            {
                if (userAnswer.ToLower().Trim() == "y")
                {
                    Console.Clear();
                    AskUserInput();
                } else
                    Console.WriteLine("Merci d'être venu au Simplon Theatre! Bonne journée!");
            }
               
        }
    }
}