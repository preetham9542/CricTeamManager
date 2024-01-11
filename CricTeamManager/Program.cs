using System;
using System.Collections.Generic;
using System.Linq;

internal class Program
{
    static void Main()
    {
        CricketTeam cricketTeam = new CricketTeam();

        while (true)
        {
            Console.WriteLine("1. Add Player");
            Console.WriteLine("2. Remove Player");
            Console.WriteLine("3. Get Player Details by Id");
            Console.WriteLine("4. Get Player Details by Name");
            Console.WriteLine("5. Get All Player Details");
            Console.WriteLine("6. Exit");

            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    // Add Player
                    Console.Write("Enter Player Id: ");
                    int playerId = int.Parse(Console.ReadLine());
                    Console.Write("Enter Player Name: ");
                    string playerName = Console.ReadLine();
                    Console.Write("Enter Player Age: ");
                    int playerAge = int.Parse(Console.ReadLine());

                    try
                    {
                        cricketTeam.AddPlayer(new Player(playerId, playerName, playerAge));
                        Console.WriteLine("Player added successfully!");
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    break;

                case 2:
                    // Remove Player
                    Console.Write("Enter Player Id to remove: ");
                    int removePlayerId = int.Parse(Console.ReadLine());
                    cricketTeam.RemovePlayer(removePlayerId);
                    Console.WriteLine("Player removed successfully!");
                    break;

                case 3:
                    // Get Player Details by Id
                    Console.Write("Enter Player Id: ");
                    int getPlayerId = int.Parse(Console.ReadLine());
                    Player playerById = cricketTeam.GetPlayerById(getPlayerId);

                    if (playerById != null)
                        Console.WriteLine($"Player Details: {playerById}");
                    else
                        Console.WriteLine("Player not found!");
                    break;

                case 4:
                    // Get Player Details by Name
                    Console.Write("Enter Player Name: ");
                    string getPlayerName = Console.ReadLine();
                    List<Player> playersByName = cricketTeam.GetPlayersByName(getPlayerName);

                    if (playersByName.Count > 0)
                    {
                        Console.WriteLine("Players with the given name:");
                        foreach (var player in playersByName)
                            Console.WriteLine(player);
                    }
                    else
                    {
                        Console.WriteLine("No players found with the given name!");
                    }
                    break;

                case 5:
                    // Get All Player Details
                    List<Player> allPlayers = cricketTeam.GetAllPlayers();

                    if (allPlayers.Count > 0)
                    {
                        Console.WriteLine("All Players:");
                        foreach (var player in allPlayers)
                            Console.WriteLine(player);
                    }
                    else
                    {
                        Console.WriteLine("No players in the team!");
                    }
                    break;

                case 6:
                    // Exit
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}

class CricketTeam
{
    private List<Player> players;

    public CricketTeam()
    {
        players = new List<Player>();
    }

    public void AddPlayer(Player player)
    {
        if (players.Count < 11)
        {
            if (!players.Any(p => p.PlayerId == player.PlayerId))
            {
                players.Add(player);
            }
            else
            {
                throw new ArgumentException("Player with the same Id already exists.");
            }
        }
        else
        {
            throw new InvalidOperationException("Cannot add more than 11 players to the team.");
        }
    }

    public void RemovePlayer(int playerId)
    {
        Player playerToRemove = players.FirstOrDefault(p => p.PlayerId == playerId);

        if (playerToRemove != null)
        {
            players.Remove(playerToRemove);
        }
        else
        {
            Console.WriteLine("Player not found!");
        }
    }

    public Player GetPlayerById(int playerId)
    {
        return players.FirstOrDefault(p => p.PlayerId == playerId);
    }

    public List<Player> GetPlayersByName(string playerName)
    {
        return players.Where(p => p.PlayerName.Equals(playerName, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public List<Player> GetAllPlayers()
    {
        return players;
    }
}

class Player
{
    public int PlayerId { get; }
    public string PlayerName { get; }
    public int PlayerAge { get; }

    public Player(int playerId, string playerName, int playerAge)
    {
        PlayerId = playerId;
        PlayerName = playerName;
        PlayerAge = playerAge;
    }

    public override string ToString()
    {
        return $"Id: {PlayerId}, Name: {PlayerName}, Age: {PlayerAge}";
    }
}
