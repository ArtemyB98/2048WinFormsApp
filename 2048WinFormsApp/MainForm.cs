using _2048.Common;
using Microsoft.VisualBasic;
using System.Security.Cryptography.Pkcs;

namespace _2048WinFormsApp
{
    public partial class MainForm : Form
    {
        private static int mapSize = 4;
        private const int frameWeight = 6;
        private const int frameHeight = 6;
        private const int cellSize = 90;
        private const int labelSpace = 60;
        private int mainFormWeignt = 0;
        private int mainFormHeignt = 0;

        private static Random random = new();

        private Label[,] labelsMap;

        private Label labelScore;
        private static Label bestScoreLabel;

        private int score = 0;
        private int bestScore = 0;

        private User user;
        public MainForm()

        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Hide();
            var startForm = new StartForm();
            startForm.ShowDialog();
            user = new User(startForm.nameTextBox.Text);
            mapSize = startForm.mapSizeTrackBar.Value;
            Show();
            mainFormWeignt = mapSize * cellSize + (mapSize + 1) * frameWeight;
            mainFormHeignt = mapSize * cellSize + (mapSize + 1) * frameHeight + labelSpace;
            Size = new Size(mainFormWeignt, mainFormHeignt + menuStrip1.Size.Height);

            labelScore = CreateLabelScore();
            Controls.Add(labelScore);
            ShowScore();

            bestScoreLabel = CreateBestLabelScore();
            Controls.Add(bestScoreLabel);
            ShowBestScore();

            InitMap();
            CaluclateBestScore();
            GenerateNumber();

        }
        //Score
        private Label CreateLabelScore()
        {
            var labelScore = new Label();

            labelScore.BackColor = Color.Gray;
            labelScore.Font = new Font("Showcard Gothic", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelScore.Name = "labelScore";
            labelScore.Size = new Size((mainFormWeignt - 3 * frameWeight) / 2, labelSpace - 2 * frameHeight);
            labelScore.TextAlign = ContentAlignment.MiddleCenter;
            labelScore.Location = new Point(frameWeight, frameHeight + menuStrip1.Size.Height);
            return labelScore;
        }
        public void ShowScore()
        {
            labelScore.Text = "Score: " + score.ToString();
        }

        //BestScore
        private Label CreateBestLabelScore()
        {
            var labelBestScore = new Label();

            labelBestScore.BackColor = Color.Gray;
            labelBestScore.Font = new Font("Showcard Gothic", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelBestScore.Name = "bestScoreLabel";
            labelBestScore.Size = new Size((mainFormWeignt - 3 * frameWeight) / 2, labelSpace - 2 * frameHeight);
            labelBestScore.TextAlign = ContentAlignment.MiddleCenter;
            labelBestScore.Location = new Point(frameWeight + (mainFormWeignt - frameWeight) / 2, frameHeight + menuStrip1.Size.Height);
            return labelBestScore;
        }
        private void CaluclateBestScore()
        {
            var users = UserResults.GetAll();
            if (users.Count == 0)
            {
                return;
            }
            bestScore = users[0].Score;
            foreach (var user in users)
            {
                if (user.Score > bestScore)
                {
                    bestScore = user.Score;
                }
            }
            bestScoreLabel.Text = bestScore.ToString();
        }
        public void ShowBestScore()
        {
            if (score > bestScore)
            {
                bestScore = score;
            }
            bestScoreLabel.Text = "Best: " + bestScore.ToString();
        }


        //InitMap
        private Label CreateLabel(int indexRow, int indexColumn)
        {
            var label = new Label();

            label.BackColor = Color.Gray;
            label.Font = new Font("Showcard Gothic", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label.Name = "label1";
            label.Size = new Size(cellSize, cellSize);
            label.TextAlign = ContentAlignment.MiddleCenter;
            var x = frameWeight + indexRow * (cellSize + frameWeight);
            var y = frameHeight + indexColumn * (cellSize + frameHeight) + labelSpace + menuStrip1.Size.Height;
            label.Location = new Point(x, y);
            return label;
        }
        private void InitMap()
        {
            labelsMap = new Label[mapSize, mapSize];
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    var newLabel = CreateLabel(j, i);
                    Controls.Add(newLabel);
                    labelsMap[i, j] = newLabel;
                }
            }

        }


        private void GenerateNumber()
        {
            var numberTry = 0;
            while (true)
            {
                numberTry++;
                var randomNumber = random.Next(mapSize * mapSize);
                var indexRow = randomNumber / mapSize;
                var indexColumn = randomNumber % mapSize;
                if (labelsMap[indexRow, indexColumn].Text == string.Empty)
                {
                    randomNumber = random.Next(0, 100);
                    if (randomNumber < 75)
                    {
                        labelsMap[indexRow, indexColumn].Text = "2";
                    }
                    else
                    {
                        labelsMap[indexRow, indexColumn].Text = "4";
                    }

                    break;
                }
                if (numberTry == mapSize * mapSize)
                {
                    break;
                }
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Right && e.KeyCode != Keys.Left && e.KeyCode != Keys.Down && e.KeyCode != Keys.Up)
            {
                return;
            }
            if (e.KeyCode == Keys.Right)
            {
                MoveRight();
            }
            if (e.KeyCode == Keys.Left)
            {
                MoveLeft();
            }
            if (e.KeyCode == Keys.Up)
            {
                MoveUp();
            }
            if (e.KeyCode == Keys.Down)
            {
                MoveDown();
            }
            ShowScore();
            ShowBestScore();

            if (Win())
            {
                user.Score = score;
                UserResults.Add(user);
                MessageBox.Show($"Congratulations, {user.Name}! Your are winner");
                Application.Restart();
            }
            if (GameOver())
            {
                user.Score = score;
                UserResults.Add(user);
                MessageBox.Show("GAME OVER");
                Application.Restart();
            }
        }

        private void MoveDown()
        {
            for (int j = 0; j < mapSize; j++)
            {
                for (int i = mapSize - 1; i >= 0; i--)
                {
                    if (labelsMap[i, j].Text != string.Empty)
                    {
                        for (int k = i - 1; k >= 0; k--)
                        {
                            if (labelsMap[k, j].Text != string.Empty)
                            {
                                if (labelsMap[i, j].Text == labelsMap[k, j].Text)
                                {
                                    var number = int.Parse(labelsMap[i, j].Text);
                                    labelsMap[i, j].Text = (number * 2).ToString();
                                    score += number * 2;

                                    labelsMap[k, j].Text = string.Empty;
                                }
                                break;
                            }
                        }
                    }
                }
            }
            for (int j = 0; j < mapSize; j++)
            {
                for (int i = mapSize - 1; i >= 0; i--)
                {
                    if (labelsMap[i, j].Text == string.Empty)
                    {
                        for (int k = i - 1; k >= 0; k--)
                        {
                            if (labelsMap[k, j].Text != string.Empty)
                            {
                                labelsMap[i, j].Text = labelsMap[k, j].Text;
                                labelsMap[k, j].Text = string.Empty;
                                break;
                            }
                        }
                    }
                }
            }
            GenerateNumber();
        }
        private void MoveUp()
        {
            for (int j = 0; j < mapSize; j++)
            {
                for (int i = 0; i < mapSize; i++)
                {
                    if (labelsMap[i, j].Text != string.Empty)
                    {
                        for (int k = i + 1; k < mapSize; k++)
                        {
                            if (labelsMap[k, j].Text != string.Empty)
                            {
                                if (labelsMap[i, j].Text == labelsMap[k, j].Text)
                                {
                                    var number = int.Parse(labelsMap[i, j].Text);
                                    labelsMap[i, j].Text = (number * 2).ToString();
                                    score += number * 2;
                                    labelsMap[k, j].Text = string.Empty;
                                }
                                break;
                            }
                        }
                    }
                }
            }
            for (int j = 0; j < mapSize; j++)
            {
                for (int i = 0; i < mapSize; i++)
                {
                    if (labelsMap[i, j].Text == string.Empty)
                    {
                        for (int k = i + 1; k < mapSize; k++)
                        {
                            if (labelsMap[k, j].Text != string.Empty)
                            {
                                labelsMap[i, j].Text = labelsMap[k, j].Text;
                                labelsMap[k, j].Text = string.Empty;
                                break;
                            }
                        }
                    }
                }
            }
            GenerateNumber();
        }
        private void MoveLeft()
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (labelsMap[i, j].Text != string.Empty)
                    {
                        for (int k = j + 1; k < mapSize; k++)
                        {
                            if (labelsMap[i, k].Text != string.Empty)
                            {
                                if (labelsMap[i, j].Text == labelsMap[i, k].Text)
                                {
                                    var number = int.Parse(labelsMap[i, j].Text);
                                    labelsMap[i, j].Text = (number * 2).ToString();
                                    score += number * 2;
                                    labelsMap[i, k].Text = string.Empty;
                                }
                                break;
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (labelsMap[i, j].Text == string.Empty)
                    {
                        for (int k = j + 1; k < mapSize; k++)
                        {
                            if (labelsMap[i, k].Text != string.Empty)
                            {
                                labelsMap[i, j].Text = labelsMap[i, k].Text;
                                labelsMap[i, k].Text = string.Empty;
                                break;
                            }
                        }
                    }
                }
            }
            GenerateNumber();
        }
        private void MoveRight()
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = mapSize - 1; j >= 0; j--)
                {
                    if (labelsMap[i, j].Text != string.Empty)
                    {
                        for (int k = j - 1; k >= 0; k--)
                        {
                            if (labelsMap[i, k].Text != string.Empty)
                            {
                                if (labelsMap[i, j].Text == labelsMap[i, k].Text)
                                {
                                    var number = int.Parse(labelsMap[i, j].Text);
                                    labelsMap[i, j].Text = (number * 2).ToString();
                                    score += number * 2;
                                    labelsMap[i, k].Text = string.Empty;
                                }
                                break;
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = mapSize - 1; j >= 0; j--)
                {
                    if (labelsMap[i, j].Text == string.Empty)
                    {
                        for (int k = j - 1; k >= 0; k--)
                        {
                            if (labelsMap[i, k].Text != string.Empty)
                            {
                                labelsMap[i, j].Text = labelsMap[i, k].Text;
                                labelsMap[i, k].Text = string.Empty;
                                break;
                            }
                        }
                    }
                }
            }
            GenerateNumber();
        }


        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void RestartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void GuidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(GetInformationText.Rules());
        }

        private void ResultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var resultsForm = new ResutlsForm();
            resultsForm.ShowDialog();
        }
        private bool Win()
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (labelsMap[i, j].Text == "2048")
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private bool GameOver()
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (labelsMap[i, j].Text == "")
                    {
                        return false;
                    }
                }
            }

            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (i < mapSize - 1 && j < mapSize - 1)
                    {
                        if (labelsMap[i, j].Text == labelsMap[i, j + 1].Text || labelsMap[i, j].Text == labelsMap[i + 1, j].Text)
                        {
                            return false;
                        }
                    }
                    if (i == mapSize - 1 && j < mapSize - 1)
                    {
                        if (labelsMap[i, j].Text == labelsMap[i, j + 1].Text && i != j)
                        {
                            return false;
                        }
                    }
                    if (j == mapSize - 1 && i < mapSize - 1)
                    {
                        if (labelsMap[i, j].Text == labelsMap[i + 1, j].Text && i != j)
                        {
                            return false;
                        }
                    }


                }
            }
            return true;
        }


    }
}
