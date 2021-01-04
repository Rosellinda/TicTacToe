
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members
        /// <summary>
        /// Holds the current results of cells in the active game
        /// </summary>
        private MarkType[] mResults;

        /// <summary>
        /// True if it is Player 1's turn (x) or Player 2's turn (0)
        /// </summary>
        private bool mPlayer1Turn;

        /// <summary>
        /// True if the game is ended
        /// </summary>
        private bool mGameEnded;

        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }

        #endregion

        /// <summary>
        /// Starts a new game and clears all values back to the start
        /// </summary>

        private void NewGame()
        {
            //Create a New Black Array of Free Cells
            mResults = new MarkType[9];

            for (var i = 0; i < mResults.Length; i++)
                mResults[i] = MarkType.Free;

            //Make sure Player 1 starts the game
            mPlayer1Turn = true;

            //Iterate every button on the grid...
            Container.Children.Cast<Button>().ToList().ForEach(button => 
            {
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            //Make sure the game has not ended
            mGameEnded = false;
        }

        /// <summary>
        /// Handles a button click event
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">The events of the click</param>

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Start the new game on the click after it finished
            if(mGameEnded)
            {
                NewGame();
                return;
            }
            //Cast the sender to a button
            var button = (Button)sender;
            //Find the Button position in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            //Don't do anything if the cell already has a value in it
            if (mResults[index] != MarkType.Free)
                return;

            //Set the cell value based on which players turn it is
            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;

            //Set button text to the result
            button.Content = mPlayer1Turn ? "X" : "0";

            //Change Noughts to Red
            if (!mPlayer1Turn)
                button.Foreground = Brushes.Red;

            //Toggle the Players turn
            mPlayer1Turn ^= true;

            //Check for the Winner
            CheckForWinner();
        }
        /// <summary>
        /// Check if there is a winner of a 3 line straight
        /// </summary>
        private void CheckForWinner()
        {
            #region Horizontal Wins
            //Check for horizontal winners
            //Row 0
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                //Game Ends
                mGameEnded = true;

                //Highlight the Winning Cells
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Yellow;
            }
            //Row 1
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                //Game Ends
                mGameEnded = true;

                //Highlight the Winning Cells
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Yellow;
            }
            //Row 2
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                //Game Ends
                mGameEnded = true;

                //Highlight the Winning Cells
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Yellow;
            }
            #endregion

            #region Vertical Wins
            //Check for vertical winners
            //Column 0
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                //Game Ends
                mGameEnded = true;

                //Highlight the Winning Cells
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Yellow;
            }
            //Column 1
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                //Game Ends
                mGameEnded = true;

                //Highlight the Winning Cells
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Yellow;
            }
            //Column 2
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                //Game Ends
                mGameEnded = true;

                //Highlight the Winning Cells
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Yellow;
            }
            #endregion

            #region Diagonal Wins
            //Check for diagonals winners
            //Top Left Button Right
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                //Game Ends
                mGameEnded = true;

                //Highlight the Winning Cells
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Yellow;
            }
            //Top Right Button Left
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                //Game Ends
                mGameEnded = true;

                //Highlight the Winning Cells
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Yellow;
            }
            #endregion

            #region No Winners
            //Check for no winner and full board
            if (!mResults.Any(f=>f == MarkType.Free))
            {
                //Game Ended
                mGameEnded = true;

                //Turn all cells orange
                //Iterate every button on the grid...
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Orange;
                });
            }
            #endregion
        }
    }
}
