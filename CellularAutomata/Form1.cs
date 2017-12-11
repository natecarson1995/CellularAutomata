using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CellularAutomata
{
    public partial class Form1 : Form
    {
        private ChunkHandler chunkHandler = new ChunkHandler();
        private ScreenDrawer screenDrawer = new ScreenDrawer();

        private int cellWidth = 12;

        //
        // Define how far we have panned
        //
        private int offsetX = 0;
        private int offsetY = 0;
        
        private int lastMouseX = 0;
        private int lastMouseY = 0;

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Toggles whether or not the board will continue advancing
        /// </summary>
        private void TogglePlayState()
        {
            timerAdvance.Enabled = !timerAdvance.Enabled;
            menuPlay.Text = (timerAdvance.Enabled ? "Pause" : "Play");
        }
        
        /// <summary>
        /// Gets the nearest cell to a specific x or y value
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private int GetNearestCell(int number)
        {
            return (int)Math.Floor(number / (double)cellWidth);
        }

        /// <summary>
        /// Resets the camera's offset
        /// </summary>
        private void ResetOffset()
        {
            offsetX = 0;
            offsetY = 0;
        }

        /// <summary>
        /// Changes the zoom level of the grid
        /// </summary>
        /// <param name="zoomLevel"></param>
        private void ChangeZoomLevel(int zoomLevel)
        {
            cellWidth = zoomLevel;
            screenDrawer.ChangeZoomLevel(zoomLevel, drawingArea.DisplayRectangle);
            screenDrawer.DrawMap(chunkHandler, offsetX, offsetY);
        }

        private void menuPlay_Click(object sender, EventArgs e)
        {
            TogglePlayState();
        }

        private void timerAdvance_Tick(object sender, EventArgs e)
        {
            //
            // Each tick, advance the board, and then draw the grid to the window
            //
            chunkHandler.AdvanceBoard();
            screenDrawer.DrawMap(chunkHandler, offsetX, offsetY);
        }

        private void drawingArea_Paint(object sender, PaintEventArgs e)
        {
            //
            // Do the instantiation and initial drawing of the grid
            //
            screenDrawer = new ScreenDrawer(drawingArea.CreateGraphics(), drawingArea.DisplayRectangle);
            screenDrawer.DrawMap(chunkHandler, offsetX, offsetY);
        }

        private void drawingArea_MouseMove(object sender, MouseEventArgs e)
        {
            //
            // Set the cell to active if left clicking, inactive if right clicking
            //
            if (e.Button==MouseButtons.Left)
            {
                chunkHandler.SetCellAt(GetNearestCell(e.X) - GetNearestCell(offsetX), GetNearestCell(e.Y) - GetNearestCell(offsetY), 1);
            }
            else if (e.Button == MouseButtons.Right)
            {
                chunkHandler.SetCellAt(GetNearestCell(e.X) - GetNearestCell(offsetX), GetNearestCell(e.Y) - GetNearestCell(offsetY), 0);
            }
            //
            // Do the panning if middle clicking, and set the cursor to a panning cursor
            //
            else if (e.Button == MouseButtons.Middle)
            {
                Cursor.Current = Cursors.SizeAll;

                //
                // Uses the mouse's velocity to pan around
                //
                offsetX += e.X - lastMouseX;
                offsetY += e.Y - lastMouseY;
            }
            //
            // Reset the cursor to not panning
            //
            else if (e.Button != MouseButtons.Middle && Cursor.Current == Cursors.SizeAll)
            {
                Cursor.Current = Cursors.Default;
            }

            //
            // As we are usually changing the grid, redraw it
            //
            screenDrawer.DrawMap(chunkHandler, offsetX, offsetY);

            //
            // Used to calculate the mouse's velocity for panning
            //
            lastMouseX = e.X;
            lastMouseY = e.Y;
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //
            // Reset to the center of the screen
            //
            ResetOffset();

            //
            // Clear and draw the map
            //
            chunkHandler.ClearMap();
            screenDrawer.DrawMap(chunkHandler, offsetX, offsetY);

            //
            // Pause if the board is playing
            //
            if (timerAdvance.Enabled) TogglePlayState();
        }

        private void randomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //
            // Reset to the center of the screen
            //
            ResetOffset();

            //
            // Clear and draw the map
            //
            chunkHandler.RandomMap(drawingArea.Width / cellWidth, drawingArea.Height / cellWidth);
            screenDrawer.DrawMap(chunkHandler, offsetX, offsetY);

            //
            // Pause if the board is playing
            //
            if (timerAdvance.Enabled) TogglePlayState();
        }

        private void drawingArea_MouseClick(object sender, MouseEventArgs e)
        {
            //
            // Set the cell to active if left clicking, inactive if right clicking
            //
            if (e.Button == MouseButtons.Left)
            {
                chunkHandler.SetCellAt(GetNearestCell(e.X) - GetNearestCell(offsetX), GetNearestCell(e.Y) - GetNearestCell(offsetY), 1);
            }
            else if (e.Button == MouseButtons.Right)
            {
                chunkHandler.SetCellAt(GetNearestCell(e.X) - GetNearestCell(offsetX), GetNearestCell(e.Y) - GetNearestCell(offsetY), 0);
            }

            //
            // As we are changing the grid, redraw it
            //
            screenDrawer.DrawMap(chunkHandler, offsetX, offsetY);
        }

        private void menuTwoX_Click(object sender, EventArgs e)
        {
            ChangeZoomLevel(2);
        }

        private void menuFourX_Click(object sender, EventArgs e)
        {
            ChangeZoomLevel(4);
        }

        private void menuEightX_Click(object sender, EventArgs e)
        {
            ChangeZoomLevel(8);
        }

        private void menuTwelveX_Click(object sender, EventArgs e)
        {
            ChangeZoomLevel(12);
        }

        private void menuTwentyFourX_Click(object sender, EventArgs e)
        {
            ChangeZoomLevel(24);
        }

        private void menuConways_Click(object sender, EventArgs e)
        {
            chunkHandler.ChangeGameRules(GameRules.CONWAYS);
        }

        private void menuBriansBrain_Click(object sender, EventArgs e)
        {
            chunkHandler.ChangeGameRules(GameRules.BRIANSBRAIN);
        }

        private void menuHighLife_Click(object sender, EventArgs e)
        {
            chunkHandler.ChangeGameRules(GameRules.HIGHLIFE);
        }

        private void menuSeeds_Click(object sender, EventArgs e)
        {
            chunkHandler.ChangeGameRules(GameRules.SEEDS);
        }
    }
}
