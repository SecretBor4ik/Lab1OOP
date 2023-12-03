using System;

namespace Lab1_oop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string answer;
            int rating;
            Game gameplay = new Game();

            GameAccount Gamer1 = new GameAccount();
            Console.WriteLine("Введіть ім'я: ");
            Gamer1.UserName = Console.ReadLine();
            Gamer1.CurrentRating = 100;
            GameAccount Gamer2 = new GameAccount();
            Console.WriteLine("Введіть ім'я: ");
            Gamer2.UserName = Console.ReadLine();
            Gamer2.CurrentRating = 100;

            Gamer1.OutGamers();
            Gamer2.OutGamers();
            do
            {
                Console.WriteLine("Введіть рейтинг на який грають:");
                rating = int.Parse(Console.ReadLine());
                while (rating <= 0 || Gamer1.CurrentRating < rating || Gamer2.CurrentRating < rating)
                {
                    Console.WriteLine("Рейтинг не може бути меньше 0, або рейтинг одного із гравців менше ніж заданий рейтинг, введіть інший рейтинг");
                    rating = int.Parse(Console.ReadLine());
                }
                if (Gamer1.CurrentRating <= 0 || Gamer2.CurrentRating <= 0)
                {
                    Console.WriteLine("Рейтинг одно з гравців меньше 0. Гру закінчено");
                    return;
                }
                else
                {
                    gameplay.PlayGame(Gamer1, Gamer2, rating);
                }
                Console.WriteLine("Зіграти ще раз? (Y/N)");
                answer = Console.ReadLine();
            } while (answer == "Y");
            Gamer1.GetStats();
        }
    }
    class GameAccount
    {
        public string UserName { get; set; }
        public int CurrentRating { get; set; }
        public int GamesCount { get; set; }
        public List<GameResult> GameResults { get; set; } = new List<GameResult>();

        public void OutGamers()
        {
            Console.WriteLine($"\nІм'я: {UserName}\nРейтинг: {CurrentRating} \nІгор зіграно: {GamesCount}\n");
        }
        public void WinGame(int rating)
        {
            GamesCount++;
            CurrentRating += rating;
        }
        public void LoseGame(int rating)
        {
            GamesCount++;
            if (CurrentRating > 1)
            {
                CurrentRating -= rating;
                if (CurrentRating < 1)
                {
                    CurrentRating = 1;
                }
            }
        }
        public void DrawGame()
        {
            GamesCount++;
        }
        public void GetStats()
        {
            foreach (GameResult result in GameResults)
            {
                Console.WriteLine($"Проти {result.Opponent}, {(result.IsWin ? "перемога" : "поразка")}, Рейтинг: {result.Rating}, Гра #{result.GameIndex}");
            }
        }
    }
    class Game
    {
        GameAccount game = new GameAccount();
        int gameIndex = 0;
        public void PlayGame(GameAccount player1, GameAccount player2, int rating)
        {
            Random random = new Random();
            int a = random.Next(1, 11);
            int b = random.Next(1, 11);
            if (a > b)
            {
                Console.WriteLine("Гравець 1 виграв");
                player1.WinGame(rating);
                Console.WriteLine("Гравець 2 програв");
                player2.LoseGame(rating);
            }
            if (a < b)
            {
                Console.WriteLine("Гравець 2 виграв");
                player2.WinGame(rating);
                Console.WriteLine("Гравець 1 програв");
                player1.LoseGame(rating);
            }
            if (a == b)
            {
                Console.WriteLine("Нічия");
                game.DrawGame();
            }
            gameIndex += 1;
            player1.GameResults.Add(new GameResult(player2.UserName, a > b, rating, gameIndex));
            player2.GameResults.Add(new GameResult(player1.UserName, b > a, rating, gameIndex));
        }
    }
    class GameResult
    {
        public string Opponent { get; }
        public bool IsWin { get; }
        public int Rating { get; }
        public int GameIndex { get; }

        public GameResult(string opponent, bool isWin, int rating, int gameIndex)
        {
            Opponent = opponent;
            IsWin = isWin;
            Rating = rating;
            GameIndex = gameIndex;
        }
    }
}