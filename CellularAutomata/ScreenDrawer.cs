using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CellularAutomata
{
    class ScreenDrawer
    {

        #region Fields

        private BufferedGraphicsContext _bufferContext;
        private BufferedGraphics _buffer;
        private Graphics _renderTo;
        private Graphics _drawingGraphics;
        private int _screenWidth;
        private int _screenHeight;
        private int _cellWidth=12;

        private Color _backgroundColor = Color.White;
        private Brush _activeCellBrush = new SolidBrush(Color.Black);
        private Brush _secondaryCellBrush = new SolidBrush(Color.Blue);
        private Pen _outlinePen = new Pen(new SolidBrush(Color.LightGray), 1);

        #endregion

        #region Properties
        
        public Color BackgroundColor
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value; }
        }
        public Brush ActiveCellBrush
        {
            get { return _activeCellBrush; }
            set { _activeCellBrush = value; }
        }
        public Brush SecondaryCellBrush
        {
            get { return _secondaryCellBrush; }
            set { _secondaryCellBrush = value; }
        }
        public Pen OutlinePen
        {
            get { return _outlinePen; }
            set { _outlinePen = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Changes the draw width of the cells in the grid
        /// </summary>
        /// <param name="zoomLevel"></param>
        /// <param name="screenArea"></param>
        public void ChangeZoomLevel(int zoomLevel, Rectangle screenArea)
        {
            _cellWidth = zoomLevel;
            _screenWidth = screenArea.Width / _cellWidth;
            _screenHeight = screenArea.Height / _cellWidth;
        }

        /// <summary>
        /// Returns the nearest cell to a specific x or y value
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private int GetNearestCell(int number)
        {
            return (int)Math.Floor(number / (double)_cellWidth);
        }

        /// <summary>
        /// Draws the entire map, using the chunk handler to get the cell values
        /// </summary>
        /// <param name="chunkHandler"></param>
        public void DrawMap(ChunkHandler chunkHandler, int offsetX, int offsetY)
        {
            //
            // Only draw if the graphics are available
            //
            if (_drawingGraphics == null) return;

            int cell;
            Brush toUse = _activeCellBrush;

            _drawingGraphics.Clear(_backgroundColor);

            //
            // Iterate through all of the cells onscreen
            //
            for (int y = 0; y < _screenHeight; y++)
            {
                for (int x = 0; x < _screenWidth; x++)
                {
                    //
                    // Apply the pan factor, and get the cell value
                    //
                    cell = chunkHandler.GetCellAt(x - GetNearestCell(offsetX), y - GetNearestCell(offsetY));

                    //
                    // Set the color based on the cell value
                    //
                    if (cell == 1) toUse = _activeCellBrush;
                    else if (cell > 0) toUse = _secondaryCellBrush;

                    //
                    // If the cell is active, fill it in
                    // No matter what, give it an outline
                    //
                    if (cell > 0)
                    {
                        _drawingGraphics.FillRectangle(toUse, x * _cellWidth, y * _cellWidth, _cellWidth, _cellWidth);
                    }
                    if (_cellWidth > 4)
                    {
                        _drawingGraphics.DrawRectangle(_outlinePen, x * _cellWidth, y * _cellWidth, _cellWidth, _cellWidth);
                    }
                }
            }

            //
            // Apply everything we have been drawing to the buffer onto the drawing area
            //
            _buffer.Render(_renderTo);
        }

        #endregion

        #region Constructors

        public ScreenDrawer() { }
        public ScreenDrawer(Graphics renderTo, Rectangle screenArea)
        {
            _renderTo = renderTo;
            _screenWidth = screenArea.Width / _cellWidth;
            _screenHeight = screenArea.Height / _cellWidth;

            _bufferContext = BufferedGraphicsManager.Current;
            _buffer = _bufferContext.Allocate(renderTo, screenArea);

            _drawingGraphics = _buffer.Graphics;
        }

        #endregion
    }
}
