using System.Collections.ObjectModel;
using System.Text.Json;

namespace ConnectFour
{
    public partial class MainPage : ContentPage
    {
        private Dictionary<string, Player> Leaderboard = new();
        private ObservableCollection<Player> LeaderboardList = new();


        public Game ActiveGame { get; set; }
        public MainPage()
        {

            InitializeComponent();
            ActiveGame = new Game("Player1", "Player2");

        }


        private void ResetGame(object? sender, EventArgs e)
        {
            ActiveGame = new Game(ActiveGame.Player1.Name, ActiveGame.Player2.Name);
            foreach (var child in GameGrid.Children)
            {
                if (child is Frame frame)
                {
                    frame.BackgroundColor = Colors.LightGray;
                }
            }
            TurnLabel.Text = ActiveGame.Player1.Name + " turn";
            TurnLabel.Background = Colors.Red;
        }
        private void AddScore(object? sender, EventArgs e)
        {

            if (sender is Button button)
            {
                var param = button.CommandParameter;
                int column = Convert.ToInt32(param);
                int row = ActiveGame.AddScore(column); 
                UpdateBoard(row, column);
                ChangeLabel(sender, e);
                if (ActiveGame.Winner != "none")
                {
                    ShowWinner(ActiveGame.Winner);
                }
            }
        }
        private void UpdateBoard(int row, int column)
        {
            var targetFrame = GetGridElement(row, column);

            if (targetFrame != null)
            {
                if (ActiveGame.PlayerTurn == 1)
                    targetFrame.BackgroundColor = Colors.Blue;
                else targetFrame.BackgroundColor = Colors.Red;
            }
        }
        private void ShowLeaderboard(object sender, EventArgs e)
        {
            return; // Placeholder for leaderboard logic
        }

        private void ChangeLabel(object sender, EventArgs e)
        {

            if (ActiveGame.PlayerTurn == 1)
            {
                TurnLabel.Text = ActiveGame.Player1.Name + " turn";
                TurnLabel.Background = Colors.Red;
            }
            else
            {
                TurnLabel.Text = ActiveGame.Player2.Name + " turn";
                TurnLabel.Background = Colors.Blue;

            }
        }
        private Frame? GetGridElement(int row, int column)
        {
            foreach (var child in GameGrid.Children)
            {
                if (child is Frame frame &&
                    Grid.GetRow(frame) == row &&
                    Grid.GetColumn(frame) == column)
                {
                    return frame;
                }
            }

            return null; 
        }

        public async void ShowWinner(string winnerName)
        {
            
            await DisplayAlert("🎉 Game Over", $"{winnerName} wins!", "New Game");
            ResetGame(null, EventArgs.Empty);
        }
        private void OpenPlayerModal(object sender, EventArgs e)
        {
            PlayerNameModal.IsVisible = true;
        }

        private void ClosePlayerModal(object sender, EventArgs e)
        {
            PlayerNameModal.IsVisible = false;
        }

        private void StartGameFromModal(object sender, EventArgs e)
        {
            string name1 = Player1Entry.Text?.Trim() ?? "Player1";
            string name2 = Player2Entry.Text?.Trim() ?? "Player2";
            if(name1 == "") name1 = "Player1";
            if(name2 == "") name2 = "Player2";
            ActiveGame = new Game(name1, name2);
            ResetGame(null, EventArgs.Empty); 

            PlayerNameModal.IsVisible = false;
        }
    }
}
