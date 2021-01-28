using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Game_Of_Life
{
    public partial class Form1 : Form
    {

        #region StartingVariables
        // The universe array
        int userX = 20;
        int userY = 20;
        bool[,] universe = new bool[20, 20];
        bool[,] scratchpad = new bool[20, 20];
        bool isFinite = true;

        //Variables for cells count
        int numAlive = 0;

        //Variables for seeds
        decimal randomSeed = 1;

        //Variables for options
        int interval = 500;
       

        //Variables for view
        bool HUD_toggle = true;
        bool Grid_toggle = true;
        bool Neighbor_toggle = true;

        //checking if file has been saved before
        bool savedAs = false;
        string fileName = null;

        // Drawing colors
        Color gridColor = Color.Black;
        Color cellColor = Color.Gray;

       
        // The Timer class
        Timer timer = new Timer();

        // Generation count
        int generations = 0;

        #endregion

        public Form1()
        {
            InitializeComponent();

            this.Text = Properties.Resources.AppTitle;

            // Setup the timer
            timer.Interval = interval; // milliseconds, ten times a second this gets called
            timer.Tick += Timer_Tick;
            timer.Enabled = false;

            //Starting status labels
            IntervalStatusLabel.Text = "Interval: " + timer.Interval.ToString();

            //Starting HUD and making it transparent
            headsUpDisplayToolStripMenuItem.Image = Properties.Resources.CheckMark;
            GenerationsLabel.BackColor = System.Drawing.Color.Transparent;
            BoundaryTypeLabel.BackColor = System.Drawing.Color.Transparent;
            CellCountLabel.BackColor = System.Drawing.Color.Transparent;
            UniverseSizeLabel.BackColor = System.Drawing.Color.Transparent;

            GenerationsLabel.ForeColor = Color.Blue;
            BoundaryTypeLabel.ForeColor = Color.Blue;
            CellCountLabel.ForeColor = Color.Blue;
            UniverseSizeLabel.ForeColor = Color.Blue;

            //Starting variables for Grid
            GridToggleView.Image = Properties.Resources.CheckMark;
            //Starting Variables for Neighbor count
            NeighborCountView.Image = Properties.Resources.CheckMark;


        }

        #region Generations
        // Calculate the next generation of cells
        private void NextGenerationFinite()
        {
            //iterate through universe
            //apply rules of game of life
            //turn cells off etc

            //reset the scratchpad
            for (int x = 0; x < universe.GetLength(0); x++)
            {
                for (int y = 0; y < universe.GetLength(1); y++)
                {
                    scratchpad[x, y] = false;
                }
            }
            int tempDead = 0;
            for (int x = 0; x < universe.GetLength(0); x++)
            {
                for (int y = 0; y < universe.GetLength(1); y++)
                {
                    if (universe[x, y] == false)
                    {
                        tempDead++;
                    }
                }
            }
            numAlive = userX * userY - tempDead;
            AliveStatusLabel.Text = "Alive: " + numAlive.ToString();
            for (int x = 0; x < universe.GetLength(0); x++)
            {
                for (int y = 0; y < universe.GetLength(1); y++)
                {
                    int numNeighbors = CountNeighborsFinite(x, y);
                    if (numNeighbors < 2 || numNeighbors > 3)
                    {
                        scratchpad[x, y] = false;

                    }
                    if ((numNeighbors == 2 || numNeighbors == 3) && universe[x, y] == true)
                    {
                        scratchpad[x, y] = true;
                    }
                    if (numNeighbors == 3 && universe[x, y] == false)
                    {
                        scratchpad[x, y] = true;
                    }
                }
            }

            bool[,] temp = universe;
            universe = scratchpad;
            scratchpad = temp;



            // Increment generation count
            generations++;
            GenerationsLabel.Text = "Generations: " + generations.ToString();
            CellCountLabel.Text = "Cell Count: " + numAlive.ToString();


            // Update status strip generations
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();
            graphicsPanel1.Invalidate();
        }

        private void NextGenerationTorodial()
        {
            //iterate through universe
            //apply rules of game of life
            //turn cells off etc

            //reset the scratchpad
            for (int x = 0; x < universe.GetLength(0); x++)
            {
                for (int y = 0; y < universe.GetLength(1); y++)
                {
                    scratchpad[x, y] = false;
                }
            }
            int tempDead = 0;
            for (int x = 0; x < universe.GetLength(0); x++)
            {
                for (int y = 0; y < universe.GetLength(1); y++)
                {
                    if (universe[x, y] == false)
                    {
                        tempDead++;
                    }
                }
            }
            numAlive = userX * userY - tempDead;
            AliveStatusLabel.Text = "Alive: " + numAlive.ToString();
            for (int x = 0; x < universe.GetLength(0); x++)
            {
                for (int y = 0; y < universe.GetLength(1); y++)
                {
                    int numNeighbors = CountNeighborsToroidal(x, y);
                    if (numNeighbors < 2 || numNeighbors > 3)
                    {
                        scratchpad[x, y] = false;

                    }
                    if ((numNeighbors == 2 || numNeighbors == 3) && universe[x, y] == true)
                    {
                        scratchpad[x, y] = true;
                    }
                    if (numNeighbors == 3 && universe[x, y] == false)
                    {
                        scratchpad[x, y] = true;
                    }
                }
            }

            bool[,] temp = universe;
            universe = scratchpad;
            scratchpad = temp;



            // Increment generation count
            generations++;

            // Update status strip generations
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();
            CellCountLabel.Text = "Cell Count: " + numAlive.ToString();
            graphicsPanel1.Invalidate();
        }

        // The event called by the timer every Interval milliseconds.
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (isFinite)
            {
                NextGenerationFinite();
            } else
            {
                NextGenerationTorodial();
            }
            
        }

        //checks the surrounding cells to see how many neighbors a cell has in a finate universe
        private int CountNeighborsFinite(int x, int y)
        {
            int count = 0;
            int xLen = universe.GetLength(0);
            int yLen = universe.GetLength(1);

            for (int yOffset = -1; yOffset <= 1; yOffset++)
            {
                for (int xOffset = -1; xOffset <= 1; xOffset++)
                {
                    int xCheck = x + xOffset;
                    int yCheck = y + yOffset;

                    //check the boundaries 
                    if (xOffset == 0 && yOffset == 0)
                    {
                        continue;
                    }
                    if (xCheck < 0)
                    {
                        continue;
                    }
                    if (yCheck < 0)
                    {
                        continue;
                    }
                    if (xCheck >= xLen)
                    {
                        continue;
                    }
                    if (yCheck >= yLen)
                    {
                        continue;
                    }
                    //once boundaries are checked then check valid cells to find neighbors
                    if (universe[xCheck, yCheck])
                    {
                        count++;
                    }
                }
            }
            return count;
        }
        //checks the surrounding cells to see how many neighbors a cell has in a toroidal universe
        private int CountNeighborsToroidal(int x, int y)
        {
            int count = 0;
            int xLen = universe.GetLength(0);
            int yLen = universe.GetLength(1);

            for (int yOffset = -1; yOffset <= 1; yOffset++)
            {
                for (int xOffset = -1; xOffset <= 1; xOffset++)
                {
                    int xCheck = x + xOffset;
                    int yCheck = y + yOffset;

                    //check the boundaries 
                    if (xOffset == 0 && yOffset == 0)
                    {
                        continue;
                    }
                    if (xCheck < 0)
                    {
                        xCheck = xLen - 1;
                    }
                    if (yCheck < 0)
                    {
                        yCheck = yLen - 1;
                    }
                    if (xCheck >= xLen)
                    {
                        xCheck = 0;
                    }
                    if (yCheck >= yLen)
                    {
                        yCheck = 0;
                    }
                    //once boundaries are checked then check valid cells to count neighbors
                    if (universe[xCheck, yCheck])
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        #endregion

        #region GraphicsPanel
        private void graphicsPanel1_Paint(object sender, PaintEventArgs e)
        {
            // Calculate the width and height of each cell in pixels
            // CELL WIDTH = WINDOW WIDTH / NUMBER OF CELLS IN X
            float cellWidth = (float)graphicsPanel1.ClientSize.Width / (float)universe.GetLength(0);
            // CELL HEIGHT = WINDOW HEIGHT / NUMBER OF CELLS IN Y
            float cellHeight = (float)graphicsPanel1.ClientSize.Height / (float)universe.GetLength(1);

            // A Pen for drawing the grid lines (color, width)
            
            Pen gridPen = new Pen(gridColor, 1);
            if (!Grid_toggle)
            {
                gridPen.Color = graphicsPanel1.BackColor;
            }
            // A Brush for filling living cells interiors (color)
            Brush cellBrush = new SolidBrush(cellColor);

            //Toggle numbers font and allignment
            Font font = new Font("Arial", 12f);

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;



            
            if (isFinite)
            {
                //FINITE PAINT THE UNIVERSE
                for (int y = 0; y < universe.GetLength(1); y++)
                {
                    // Iterate through the universe in the x, left to right
                    for (int x = 0; x < universe.GetLength(0); x++)
                    {
                        // A rectangle to represent each cell in pixels
                        RectangleF cellRect = Rectangle.Empty;
                        //Rectangle cellRect = Rectangle.Empty;
                        cellRect.X = (float)x * cellWidth;
                        cellRect.Y = (float)y * cellHeight;
                        cellRect.Width = cellWidth;
                        cellRect.Height = cellHeight;

                        // Fill the cell with a brush if alive 
                        // Also checks if neighbor toggle is on
                        if (Neighbor_toggle)
                        {
                            if (universe[x, y] == true)
                            {
                                e.Graphics.FillRectangle(cellBrush, cellRect);
                                //puts number of neighbors in center of rectangle if there are more than 0 neighbors
                                if (CountNeighborsFinite(x, y) != 0)
                                    e.Graphics.DrawString(CountNeighborsFinite(x, y).ToString(), font, Brushes.Red, cellRect, stringFormat);

                            }
                            else
                            {
                                //puts number of neighbors in center of rectangle if there are more than 0 neighbors
                                if (CountNeighborsFinite(x, y) != 0)
                                    e.Graphics.DrawString(CountNeighborsFinite(x, y).ToString(), font, Brushes.Black, cellRect, stringFormat);
                            }
                        } else
                        {
                            if (universe[x,y] == true)
                            {
                                e.Graphics.FillRectangle(cellBrush, cellRect);
                            }
                        }
                        
                        

                        // Outline the cell with a pen
                        e.Graphics.DrawRectangle(gridPen, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);

                    }
                }
            } else
            {
                //TORODAL PAINT THE UNIVERSE

                for (int y = 0; y < universe.GetLength(1); y++)
                {
                    // Iterate through the universe in the x, left to right
                    for (int x = 0; x < universe.GetLength(0); x++)
                    {
                        // A rectangle to represent each cell in pixels
                        RectangleF cellRect = Rectangle.Empty;
                        //Rectangle cellRect = Rectangle.Empty;
                        cellRect.X = (float)x * cellWidth;
                        cellRect.Y = (float)y * cellHeight;
                        cellRect.Width = cellWidth;
                        cellRect.Height = cellHeight;

                        // Fill the cell with a brush if alive
                        if (universe[x, y] == true)
                        {
                            e.Graphics.FillRectangle(cellBrush, cellRect);
                            //puts number of neighbors in center of rectangle if there are more than 0 neighbors
                            if (CountNeighborsFinite(x, y) != 0)
                                e.Graphics.DrawString(CountNeighborsToroidal(x, y).ToString(), font, Brushes.Red, cellRect, stringFormat);

                        }
                        else
                        {
                            //puts number of neighbors in center of rectangle if there are more than 0 neighbors
                            if (CountNeighborsFinite(x, y) != 0)
                                e.Graphics.DrawString(CountNeighborsToroidal(x, y).ToString(), font, Brushes.Black, cellRect, stringFormat);
                        }

                        // Outline the cell with a pen
                        e.Graphics.DrawRectangle(gridPen, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);

                    }
                }
            }
           

           
            // Cleaning up pens and brushes
            gridPen.Dispose();
            cellBrush.Dispose();
        }

        private void graphicsPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            // If the left mouse button was clicked
            if (e.Button == MouseButtons.Left)
            {
                // Calculate the width and height of each cell in pixels
                //chaning these to float math so that the pointer is more accurate and the window gets refreshed without clipping
                float cellWidth = (float)graphicsPanel1.ClientSize.Width / (float)universe.GetLength(0);
                float cellHeight = (float)graphicsPanel1.ClientSize.Height / (float)universe.GetLength(1);

                // Calculate the cell that was clicked in
                // CELL X = MOUSE X / CELL WIDTH
                float floatX = (float)e.X / cellWidth;
                // CELL Y = MOUSE Y / CELL HEIGHT
                float floatY = (float)e.Y / cellHeight;

                // Toggle the cell's state
                int intX = (int)floatX;
                int intY = (int)floatY;

                if (universe[intX, intY] = !universe[intX, intY])
                {
                    numAlive++;
                    AliveStatusLabel.Text = "Alive: " + numAlive.ToString();
                }
                else
                {
                    if (numAlive > 0)
                    {
                        numAlive--;
                        AliveStatusLabel.Text = "Alive: " + numAlive.ToString();
                    }

                }

                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();
            }
        }
        #endregion

        #region NewOpenSave
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //change background back to white
            graphicsPanel1.BackColor = Color.White;
            gridColor = Color.Black;
            cellColor = Color.Gray;
            SeedStatusLabel.Text = "Seed: ";

            universe = null;
            universe = (bool[,])Array.CreateInstance(typeof(bool), userX, userY);

            generations = 0;
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();
            AliveStatusLabel.Text = "Alive: 0";

            timer.Enabled = false;

            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    universe[x, y] = false; // turn cells off when new is clicked

                    //repaint window correctly
                    graphicsPanel1.Invalidate();
                }
            }
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            //change background back to white
            graphicsPanel1.BackColor = Color.White;
            gridColor = Color.Black;
            cellColor = Color.Gray;
            SeedStatusLabel.Text = "Seed: ";

            StartButton.Image = Game_Of_Life.Properties.Resources.Start;
            PauseButton.Image = Game_Of_Life.Properties.Resources.Pause;
            universe = null;
            universe = (bool[,])Array.CreateInstance(typeof(bool), userX, userY);

            generations = 0;
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();
            AliveStatusLabel.Text = "Alive: 0";

            timer.Enabled = false;

            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    universe[x, y] = false; // turn cells off when new is clicked

                    //repaint window correctly
                    graphicsPanel1.Invalidate();
                }
            }

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "All Files|*.*|Cells|*.cells";
            dlg.FilterIndex = 2;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                StreamReader reader = new StreamReader(dlg.FileName);

                // Create a couple variables to calculate the width and height
                // of the data in the file.
                int maxWidth = 0;
                int maxHeight = 0;

                // Iterate through the file once to get its size.
                while (!reader.EndOfStream)
                {
                    // Read one row at a time.
                    string row = reader.ReadLine();

                    // If the row begins with '!' then it is a comment
                    // and should be ignored.
                    if (row.ElementAt(0) == '!')
                    {
                        continue;
                    }
                    // If the row is not a comment then it is a row of cells.
                    // Increment the maxHeight variable for each row read.

                    maxHeight++;
                    maxWidth = row.Length;
                    // Get the length of the current row string
                    // and adjust the maxWidth variable if necessary.
                }

                // Resize the current universe and scratchPad
                // to the width and height of the file calculated above.
                universe = null;
                universe = (bool[,])Array.CreateInstance(typeof(bool), maxWidth, maxHeight);
                // Reset the file pointer back to the beginning of the file.
                reader.BaseStream.Seek(0, SeekOrigin.Begin);

                // Iterate through the file again, this time reading in the cells.
                int yPos = 0;
                while (!reader.EndOfStream)
                {
                    // Read one row at a time.
                    string row = reader.ReadLine();

                    // If the row begins with '!' then
                    // it is a comment and should be ignored.
                    if (row.ElementAt(0) == '!')
                    {
                        continue;
                    }
                    // If the row is not a comment then 
                    // it is a row of cells and needs to be iterated through.

                    for (int xPos = 0; xPos < row.Length; xPos++)
                    {
                        // If row[xPos] is a 'O' (capital O) then
                        // set the corresponding cell in the universe to alive.
                        if (row[xPos] == 'O')
                        {
                            universe[xPos, yPos] = true;
                        }
                        // If row[xPos] is a '.' (period) then
                        // set the corresponding cell in the universe to dead.
                        if (row[xPos] == '.')
                        {
                            universe[xPos, yPos] = false;
                        }
                    }
                    yPos++;
                }

                // Close the file.
                reader.Close();
            }
            graphicsPanel1.Invalidate();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (savedAs)
            {
                StreamWriter writer = new StreamWriter(fileName);
                writer.WriteLine("!This is a universe.");


                for (int y = 0; y < universe.GetLength(1); y++)
                {
                    // Create a string to represent the current row.
                    String currentRow = string.Empty;

                    // Iterate through the current row one cell at a time.
                    for (int x = 0; x < universe.GetLength(0); x++)
                    {
                        // If the universe[x,y] is alive then append 'O' (capital O)
                        // to the row string.
                        if (universe[x, y] != false)
                        {
                            currentRow += 'O';
                        }
                        else
                        {
                            currentRow += '.';
                        }
                        // Else if the universe[x,y] is dead then append '.' (period)
                        // to the row string.
                    }

                    // Once the current row has been read through and the 
                    // string constructed then write it to the file using WriteLine.
                    writer.WriteLine(currentRow);
                }
                // After all rows and columns have been written then close the file.
                writer.Close();
            }
            else
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "All Files|*.*|Cells|*.cells";
                dlg.FilterIndex = 2; dlg.DefaultExt = "cells";


                if (DialogResult.OK == dlg.ShowDialog())
                {
                    //let the save event know that saved as happened so that we can fast save
                    savedAs = true;

                    StreamWriter writer = new StreamWriter(dlg.FileName);

                    //let save event know the file name
                    fileName = dlg.FileName;

                    // Write any comments you want to include first.
                    // Prefix all comment strings with an exclamation point.
                    // Use WriteLine to write the strings to the file. 
                    // It appends a CRLF for you.
                    writer.WriteLine("!This is a universe saved into a file.");

                    // Iterate through the universe one row at a time.
                    for (int y = 0; y < universe.GetLength(1); y++)
                    {
                        // Create a string to represent the current row.
                        String currentRow = string.Empty;

                        // Iterate through the current row one cell at a time.
                        for (int x = 0; x < universe.GetLength(0); x++)
                        {
                            // If the universe[x,y] is alive then append 'O' (capital O)
                            // to the row string.
                            if (universe[x, y] != false)
                            {
                                currentRow += 'O';
                            }
                            else
                            {
                                currentRow += '.';
                            }
                            // Else if the universe[x,y] is dead then append '.' (period)
                            // to the row string.
                        }

                        // Once the current row has been read through and the 
                        // string constructed then write it to the file using WriteLine.
                        writer.WriteLine(currentRow);
                    }

                    // After all rows and columns have been written then close the file.
                    writer.Close();
                }
            }
            graphicsPanel1.Invalidate();
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "All Files|*.*|Cells|*.cells";
            dlg.FilterIndex = 2; dlg.DefaultExt = "cells";


            if (DialogResult.OK == dlg.ShowDialog())
            {
                //let the save event know that saved as happened so that we can fast save
                savedAs = true;

                StreamWriter writer = new StreamWriter(dlg.FileName);

                //let save event know the file name
                fileName = dlg.FileName;
                // Write any comments you want to include first.
                // Prefix all comment strings with an exclamation point.
                // Use WriteLine to write the strings to the file. 
                // It appends a CRLF for you.
                writer.WriteLine("!This is a universe saved into a file.");

                // Iterate through the universe one row at a time.
                for (int y = 0; y < universe.GetLength(1); y++)
                {
                    // Create a string to represent the current row.
                    String currentRow = string.Empty;

                    // Iterate through the current row one cell at a time.
                    for (int x = 0; x < universe.GetLength(0); x++)
                    {
                        // If the universe[x,y] is alive then append 'O' (capital O)
                        // to the row string.
                        if (universe[x, y] != false)
                        {
                            currentRow += 'O';
                        }
                        else
                        {
                            currentRow += '.';
                        }
                        // Else if the universe[x,y] is dead then append '.' (period)
                        // to the row string.
                    }

                    // Once the current row has been read through and the 
                    // string constructed then write it to the file using WriteLine.
                    writer.WriteLine(currentRow);
                }

                // After all rows and columns have been written then close the file.
                writer.Close();
            }
        }

       

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "All Files|*.*|Cells|*.cells";
            dlg.FilterIndex = 2;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                StreamReader reader = new StreamReader(dlg.FileName);

                // Create a couple variables to calculate the width and height
                // of the data in the file.
                int maxWidth = 0;
                int maxHeight = 0;

                // Iterate through the file once to get its size.
                while (!reader.EndOfStream)
                {
                    // Read one row at a time.
                    string row = reader.ReadLine();

                    // If the row begins with '!' then it is a comment
                    // and should be ignored.
                    if (row.ElementAt(0) == '!')
                    {
                        continue;
                    }
                    // If the row is not a comment then it is a row of cells.
                    // Increment the maxHeight variable for each row read.

                    maxHeight++;
                    maxWidth = row.Length;
                    // Get the length of the current row string
                    // and adjust the maxWidth variable if necessary.
                }

                // Resize the current universe and scratchPad
                // to the width and height of the file calculated above.
                universe = null;
                universe = (bool[,])Array.CreateInstance(typeof(bool), maxWidth, maxHeight);
                // Reset the file pointer back to the beginning of the file.
                reader.BaseStream.Seek(0, SeekOrigin.Begin);

                // Iterate through the file again, this time reading in the cells.
                int yPos = 0;
                while (!reader.EndOfStream)
                {
                    // Read one row at a time.
                    string row = reader.ReadLine();

                    // If the row begins with '!' then
                    // it is a comment and should be ignored.
                    if (row.ElementAt(0) == '!')
                    {
                        continue;
                    }
                    // If the row is not a comment then 
                    // it is a row of cells and needs to be iterated through.

                    for (int xPos = 0; xPos < row.Length; xPos++)
                    {
                        // If row[xPos] is a 'O' (capital O) then
                        // set the corresponding cell in the universe to alive.
                        if (row[xPos] == 'O')
                        {
                            universe[xPos, yPos] = true;
                        }
                        // If row[xPos] is a '.' (period) then
                        // set the corresponding cell in the universe to dead.
                        if (row[xPos] == '.')
                        {
                            universe[xPos, yPos] = false;
                        }
                    }
                    yPos++;
                }

                // Close the file.
                reader.Close();
            }
            graphicsPanel1.Invalidate();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            if (savedAs)
            {
                StreamWriter writer = new StreamWriter(fileName);
                writer.WriteLine("!This is a universe.");


                for (int y = 0; y < universe.GetLength(1); y++)
                {
                    // Create a string to represent the current row.
                    String currentRow = string.Empty;

                    // Iterate through the current row one cell at a time.
                    for (int x = 0; x < universe.GetLength(0); x++)
                    {
                        // If the universe[x,y] is alive then append 'O' (capital O)
                        // to the row string.
                        if (universe[x, y] != false)
                        {
                            currentRow += 'O';
                        }
                        else
                        {
                            currentRow += '.';
                        }
                        // Else if the universe[x,y] is dead then append '.' (period)
                        // to the row string.
                    }

                    // Once the current row has been read through and the 
                    // string constructed then write it to the file using WriteLine.
                    writer.WriteLine(currentRow);
                }
                // After all rows and columns have been written then close the file.
                writer.Close();
            }
            else
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "All Files|*.*|Cells|*.cells";
                dlg.FilterIndex = 2; dlg.DefaultExt = "cells";


                if (DialogResult.OK == dlg.ShowDialog())
                {
                    //let the save event know that saved as happened so that we can fast save
                    savedAs = true;

                    StreamWriter writer = new StreamWriter(dlg.FileName);

                    //let save event know the file name
                    fileName = dlg.FileName;

                    // Write any comments you want to include first.
                    // Prefix all comment strings with an exclamation point.
                    // Use WriteLine to write the strings to the file. 
                    // It appends a CRLF for you.
                    writer.WriteLine("!This is a universe saved into a file.");

                    // Iterate through the universe one row at a time.
                    for (int y = 0; y < universe.GetLength(1); y++)
                    {
                        // Create a string to represent the current row.
                        String currentRow = string.Empty;

                        // Iterate through the current row one cell at a time.
                        for (int x = 0; x < universe.GetLength(0); x++)
                        {
                            // If the universe[x,y] is alive then append 'O' (capital O)
                            // to the row string.
                            if (universe[x, y] != false)
                            {
                                currentRow += 'O';
                            }
                            else
                            {
                                currentRow += '.';
                            }
                            // Else if the universe[x,y] is dead then append '.' (period)
                            // to the row string.
                        }

                        // Once the current row has been read through and the 
                        // string constructed then write it to the file using WriteLine.
                        writer.WriteLine(currentRow);
                    }

                    // After all rows and columns have been written then close the file.
                    writer.Close();
                }
            }
            graphicsPanel1.Invalidate();
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "All Files|*.*|Cells|*.cells";
            dlg.FilterIndex = 2;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                StreamReader reader = new StreamReader(dlg.FileName);

                // Create a couple variables to calculate the width and height
                // of the data in the file.
                int maxWidth = 0;
                int maxHeight = 0;

                // Iterate through the file once to get its size.
                while (!reader.EndOfStream)
                {
                    // Read one row at a time.
                    string row = reader.ReadLine();

                    // If the row begins with '!' then it is a comment
                    // and should be ignored.
                    if (row.ElementAt(0) == '!')
                    {
                        continue;
                    }
                    // If the row is not a comment then it is a row of cells.
                    // Increment the maxHeight variable for each row read.

                    maxHeight++;
                    maxWidth = row.Length;
                    // Get the length of the current row string
                    // and adjust the maxWidth variable if necessary.
                }

                // Resize the current universe and scratchPad
                // to the width and height of the file calculated above.
                universe = null;
                universe = (bool[,])Array.CreateInstance(typeof(bool), maxWidth, maxHeight);
                // Reset the file pointer back to the beginning of the file.
                reader.BaseStream.Seek(0, SeekOrigin.Begin);

                // Iterate through the file again, this time reading in the cells.
                int yPos = 0;
                while (!reader.EndOfStream)
                {
                    // Read one row at a time.
                    string row = reader.ReadLine();

                    // If the row begins with '!' then
                    // it is a comment and should be ignored.
                    if (row.ElementAt(0) == '!')
                    {
                        continue;
                    }
                    // If the row is not a comment then 
                    // it is a row of cells and needs to be iterated through.

                    for (int xPos = 0; xPos < row.Length; xPos++)
                    {
                        // If row[xPos] is a 'O' (capital O) then
                        // set the corresponding cell in the universe to alive.
                        if (row[xPos] == 'O')
                        {
                            universe[xPos, yPos] = true;
                        }
                        // If row[xPos] is a '.' (period) then
                        // set the corresponding cell in the universe to dead.
                        if (row[xPos] == '.')
                        {
                            universe[xPos, yPos] = false;
                        }
                    }
                    yPos++;
                }

                // Close the file.
                reader.Close();
            }
            graphicsPanel1.Invalidate();
        }

        #endregion

        #region StartPauseNextButtons
        private void StartButton_Click(object sender, EventArgs e)
        {
            timer.Enabled = true;
            StartButton.Image = Game_Of_Life.Properties.Resources.StartGray;
            PauseButton.Image = Game_Of_Life.Properties.Resources.Pause;

            //graphicsPanel1.Invalidate();
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
            StartButton.Image = Game_Of_Life.Properties.Resources.Start;
            PauseButton.Image = Game_Of_Life.Properties.Resources.PauseGray;
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            if (isFinite)
            {
                NextGenerationFinite();
            } else
            {
                NextGenerationTorodial();
            }
            
            graphicsPanel1.Invalidate();
        }
        #endregion

        #region ColorOptions
        private void backgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            dlg.Color = graphicsPanel1.BackColor;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                graphicsPanel1.BackColor = dlg.Color;
            }

            graphicsPanel1.Invalidate();
        }

        private void gridColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            dlg.Color = gridColor;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                gridColor = dlg.Color;
            }

            graphicsPanel1.Invalidate();
        }

        private void livingCellsColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            dlg.Color = cellColor;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                cellColor = dlg.Color;
            }

            graphicsPanel1.Invalidate();
        }

        private void backgroundColorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            dlg.Color = graphicsPanel1.BackColor;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                graphicsPanel1.BackColor = dlg.Color;
            }

            graphicsPanel1.Invalidate();
        }

        private void gridColorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            dlg.Color = gridColor;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                gridColor = dlg.Color;
            }

            graphicsPanel1.Invalidate();
        }

        private void livingCellColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            dlg.Color = cellColor;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                cellColor = dlg.Color;
            }

            graphicsPanel1.Invalidate();
        }

        #endregion

        #region SeedOptions
        private void fromSeedToolStripMenuItem_Click(object sender, EventArgs e)
        {

            timer.Enabled = false;
            StartButton.Image = Game_Of_Life.Properties.Resources.Start;
            PauseButton.Image = Game_Of_Life.Properties.Resources.Pause;


            ModalDialog dlg = new ModalDialog();

            

            if (DialogResult.OK == dlg.ShowDialog())
            {
                int tempDead = 0;
                
                

                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    for (int y = 0; y < universe.GetLength(1); y++)
                    {

                        universe[x, y] = false;

                    }
                }
                randomSeed = dlg.Seed;
                SeedStatusLabel.Text = "Seed: " + randomSeed.ToString();
                

                Random seededRand = new Random((int)randomSeed);


                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    for (int y = 0; y < universe.GetLength(1); y++)
                    {
                        if (seededRand.Next() % 2 == 0)
                        {
                            universe[x, y] = true;
                        }
                    }
                }

                //count number alive in new universe
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    for (int y = 0; y < universe.GetLength(1); y++)
                    {
                        if (universe[x, y] == false)
                        {
                            tempDead++;
                        }
                    }
                }

                GenerationsLabel.Text = "Generations: " + generations.ToString();
                numAlive = userX * userY - tempDead;
                AliveStatusLabel.Text = "Alive: " + numAlive.ToString();
                CellCountLabel.Text = "Cell Count: " + numAlive.ToString();

                graphicsPanel1.Invalidate();
                
            }
        }

        //Randomize universe from current seed
        private void fromCurrentSeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int tempDead = 0;
           

            for (int x = 0; x < universe.GetLength(0); x++)
            {
                for (int y = 0; y < universe.GetLength(1); y++)
                {

                    universe[x, y] = false;

                }
            }

            Random seededRand = new Random((int)randomSeed);
            randomSeed = seededRand.Next(0, 1000);
            SeedStatusLabel.Text = "Seed: " + randomSeed.ToString();

            

            for (int x = 0; x < universe.GetLength(0); x++)
            {
                for (int y = 0; y < universe.GetLength(1); y++)
                {
                    if (seededRand.Next() % 2 == 0)
                    {
                        universe[x, y] = true;
                    }
                }
            }

            //count number alive in new universe
            for (int x = 0; x < universe.GetLength(0); x++)
            {
                for (int y = 0; y < universe.GetLength(1); y++)
                {
                    if (universe[x, y] == false)
                    {
                        tempDead++;
                    }
                }
            }

            GenerationsLabel.Text = "Generations: " + generations.ToString();
            numAlive = userX * userY - tempDead;
            AliveStatusLabel.Text = "Alive: " + numAlive.ToString();
            CellCountLabel.Text = "Cell Count: " + numAlive.ToString();
            
            graphicsPanel1.Invalidate();
            
        }

        //Randomize Universe based on time in miliseconds
        private void fromTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int tempDead = 0;

            for (int x = 0; x < universe.GetLength(0); x++)
            {
                for (int y = 0; y < universe.GetLength(1); y++)
                {

                    universe[x, y] = false;

                }
            }

            Random rand = new Random();
            int randomTime = (int)DateTime.Now.Millisecond;
            Random seededRand = new Random(randomTime);
            SeedStatusLabel.Text = "Seed: " + randomTime.ToString();
            randomSeed = randomTime;

            for (int x = 0; x < universe.GetLength(0); x++)
            {
                for (int y = 0; y < universe.GetLength(1); y++)
                {
                    if (seededRand.Next() % 2 == 0)
                    {
                        universe[x, y] = true;
                    }
                }
            }

            //count number alive in new universe
            for (int x = 0; x < universe.GetLength(0); x++)
            {
                for (int y = 0; y < universe.GetLength(1); y++)
                {
                    if (universe[x, y] == false)
                    {
                        tempDead++;
                    }
                }
            }

            GenerationsLabel.Text = "Generations: " + generations.ToString();
            numAlive = userX * userY - tempDead;
            AliveStatusLabel.Text = "Alive: " + numAlive.ToString();
            CellCountLabel.Text = "Cell Count: " + numAlive.ToString();

            graphicsPanel1.Invalidate();
        }

        #endregion

        #region OptionsAndViewMenus
        //Opens Options Dialog and sets interval, width, and height of form
        private void optionsMenu_Click(object sender, EventArgs e)
        {

            timer.Enabled = false;
            StartButton.Image = Game_Of_Life.Properties.Resources.Start;
            PauseButton.Image = Game_Of_Life.Properties.Resources.Pause;

            OptionsModal dlg = new OptionsModal();
            dlg.Interval = interval;
            dlg.ParentWidth = userX;
            dlg.ParentHeight = userY;

            int tempx = userX;
            int tempy = userY;


            if (DialogResult.OK == dlg.ShowDialog())
            {


                userX = (int)dlg.ParentWidth;
                userY = (int)dlg.ParentHeight;
                interval = (int)dlg.Interval;
                if (tempx != userX)
                {
                    universe = null;
                    universe = new bool[userX, userY];
                    scratchpad = null;
                    scratchpad = new bool[userX, userY];
                }
                if (tempy != userY)
                {
                    universe = null;
                    universe = new bool[userX, userY];
                    scratchpad = null;
                    scratchpad = new bool[userX, userY];
                }

                if (interval == 0)
                {
                    interval = 1;
                }
                timer.Interval = interval;
                IntervalStatusLabel.Text = "Interval: " + interval.ToString();
            }

            graphicsPanel1.Invalidate();
        }

        //Toggles HUD display
        private void headsUpDisplayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (HUD_toggle)
            {
                headsUpDisplayToolStripMenuItem.Image = null;
                GenerationsLabel.Visible = false;
                CellCountLabel.Visible = false;
                BoundaryTypeLabel.Visible = false;
                UniverseSizeLabel.Visible = false;
                HUD_toggle = !HUD_toggle;
            }
            else
            {
                headsUpDisplayToolStripMenuItem.Image = Properties.Resources.CheckMark;
                GenerationsLabel.Visible = true;
                CellCountLabel.Visible = true;
                BoundaryTypeLabel.Visible = true;
                UniverseSizeLabel.Visible = true;
                HUD_toggle = !HUD_toggle;
            }


        }

        
        //Toggles Grid 
        private void GridToggleView_Click(object sender, EventArgs e)
        {
            if (Grid_toggle)
            {
                GridToggleView.Image = null;
                
                Grid_toggle = !Grid_toggle;
            }
            else
            {
                GridToggleView.Image = Properties.Resources.CheckMark;
                
                Grid_toggle = !Grid_toggle;
            }
            graphicsPanel1.Invalidate();
        }

        //Toggles Neighbor count
        private void NeighborCountView_Click(object sender, EventArgs e)
        {
            if (Neighbor_toggle)
            {
                NeighborCountView.Image = null;
                
                Neighbor_toggle = !Neighbor_toggle;
            }
            else
            {
                NeighborCountView.Image = Properties.Resources.CheckMark;
                
                Neighbor_toggle = !Neighbor_toggle;
            }
            graphicsPanel1.Invalidate();
        }

        #endregion
    }
}
