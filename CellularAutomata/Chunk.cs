using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CellularAutomata
{
    class Chunk
    {
        public enum ActivityState
        {
            DEAD,
            HIBERNATING,
            ACTIVE
        }
        public delegate void DelegateQueueChanges(ChunkHandler chunkHandler);

        #region Fields

        private ActivityState _activityLevel;
        private int _xIndex;
        private int _yIndex;
        private int _width;
        private int _height;
        private int _hibernationTimer;
        private int[] _board;
        private Dictionary<int, int> _cellChanges = new Dictionary<int, int>();
        private DelegateQueueChanges _queueChanges;

        #endregion

        #region Properties

        public ActivityState ActivityLevel
        {
            get { return _activityLevel; }
            set { _activityLevel = value; }
        }
        public int XIndex
        {
            get { return _xIndex; }
            set { _xIndex = value; }
        }
        public int YIndex
        {
            get { return _yIndex; }
            set { _yIndex = value; }
        }
        public int HibernationTimer
        {
            get { return _hibernationTimer; }
            set { _hibernationTimer = value; }
        }
        public DelegateQueueChanges QueueChanges
        {
            get { return _queueChanges; }
            set { _queueChanges = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a bool indicating whether the chunk does not contain any live cells
        /// </summary>
        /// <returns></returns>
        private bool IsEmpty()
        {
            for (int i = 0; i < _width * _height; i++)
            {
                if (_board[i] != 0) return false;
            }

            return true;
        }

        /// <summary>
        /// Returns an int corresponding to the index of a certain point in the chunk, unrolled
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private int GetUnrolledIndex(int x, int y)
        {
            return y * _height + x;
        }

        /// <summary>
        /// Returns the value of a specific cell in the chunk, using the ChunkHandler to get it if it is in a neighboring chunk
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="chunkHandler"></param>
        /// <returns></returns>
        public int GetChunkCell(int x, int y, ChunkHandler chunkHandler)
        {
            //
            // Declare variables
            //
            int chunkDeltaX = 0; // Describes if the cell is in a neighboring chunk
            int chunkDeltaY = 0;
            int cellX = x;
            int cellY = y;
            bool makeNeighboringChunk;

            //
            // We allow x and y values that go out of the bounds of the chunk,
            // and use this area to check the neighboring chunk if that is the case.
            //
            if (x < 0)
            {
                chunkDeltaX = -1;
                cellX = _width - 1;
            }
            else if (x >= _width)
            {
                chunkDeltaX = 1;
                cellX = 0;
            }

            if (y < 0)
            {
                chunkDeltaY = -1;
                cellY = _height - 1;
            }
            else if (y >= _height)
            {
                chunkDeltaY = 1;
                cellY = 0;
            }

            //
            // Do not allow hibernating chunks to make more chunks,
            // this would lead to infinite chunks being made.
            //
            makeNeighboringChunk = (_activityLevel==ActivityState.ACTIVE);

            //
            // This if statement only fires if we are checking the neighboring chunk
            //
            if (chunkDeltaX != 0 || chunkDeltaY != 0)
            {
                return chunkHandler.GetChunkCellAt(_xIndex + chunkDeltaX, _yIndex + chunkDeltaY, cellX, cellY, makeNeighboringChunk);
            }
            //
            // Elsewise, we just return the cell's value
            //
            else
            {
                return _board[GetUnrolledIndex(x, y)];
            }
        }

        /// <summary>
        /// Sets the value of the specific cell in the chunk
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="value"></param>
        public void SetCellAt(int x, int y, int value)
        {
            _board[GetUnrolledIndex(x, y)] = value;
        }

        /// <summary>
        /// Gets the number of neighbor cells active around a particular cell
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="chunkHandler"></param>
        /// <returns></returns>
        private int GetNeighborCount(int x, int y, ChunkHandler chunkHandler)
        {
            int neighbors = 0;

            //
            // Iterate through all of the neighbors
            //
            for (int deltaY = -1; deltaY <= 1; deltaY++)
            {
                for (int deltaX = -1; deltaX <= 1; deltaX++)
                {
                    //
                    // First, check that it is only a neighbor, not the cell itself.
                    // Second, check if it's value is one, if so, increment the neighbor variable.
                    //
                    if ((deltaX != 0 || deltaY != 0) && GetChunkCell(x + deltaX, y + deltaY, chunkHandler) == 1)
                    {
                        neighbors++;
                    }
                }
            }

            return neighbors;
        }

        /// <summary>
        /// Queues up all of the changes for the chunk, according to the rules of Conway's Game of Life
        /// </summary>
        /// <param name="chunkHandler"></param>
        public void QueueChangesForConways(ChunkHandler chunkHandler)
        {
            int neighbors;

            //
            // Dump the queued up changes
            //
            _cellChanges.Clear();

            //
            // Iterate through the board
            //
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    //
                    // Get the neighbor count for each cell
                    //
                    neighbors = GetNeighborCount(x, y, chunkHandler);

                    //
                    // If the cell is alive, only toggle the value if
                    // there are too few or too many neighbors
                    //
                    if (GetChunkCell(x, y, chunkHandler) == 1)
                    {
                        if (neighbors < 2 || neighbors > 3) _cellChanges.Add(GetUnrolledIndex(x, y), 0);
                    }
                    //
                    // Elsewise, only toggle the value if there are exactly three neighbors
                    //
                    else
                    {
                        if (neighbors == 3) _cellChanges.Add(GetUnrolledIndex(x, y), 1);
                    }
                }
            }


        }

        /// <summary>
        /// Queues up all of the changes for the chunk, according to the
        /// rules of Brian's Brain
        /// </summary>
        /// <param name="chunkHandler"></param>
        public void QueueChangesForBriansBrain(ChunkHandler chunkHandler)
        {
            //
            // For Brian's Brain, we consider
            // 0 as dead
            // 1 as alive
            // 2 as "dying"
            //
            int neighbors;
            int cellValue;

            //
            // Dump the queued up changes
            //
            _cellChanges.Clear();

            //
            // Iterate through the board
            //
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    //
                    // Get the neighbor count, and cell value for each cell
                    //
                    neighbors = GetNeighborCount(x, y, chunkHandler);
                    cellValue = GetChunkCell(x, y, chunkHandler);
                    
                    if (cellValue == 0)
                    {
                        if (neighbors == 2) _cellChanges.Add(GetUnrolledIndex(x, y), 1);
                    }
                    else if (cellValue == 1)
                    {
                        _cellChanges.Add(GetUnrolledIndex(x, y), 2);
                    }
                    else
                    {
                        _cellChanges.Add(GetUnrolledIndex(x, y), 0);
                    }
                }
            }

        }

        /// <summary>
        /// Queues up all of the changes for the chunk, according to the
        /// rules of Seeds
        /// </summary>
        /// <param name="chunkHandler"></param>
        public void QueueChangesForSeeds(ChunkHandler chunkHandler)
        {
            //
            // For Seeds, we consider
            // 0 as dead
            // 1 as alive
            //
            int neighbors;
            int cellValue;

            //
            // Dump the queued up changes
            //
            _cellChanges.Clear();

            //
            // Iterate through the board
            //
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    //
                    // Get the neighbor count, and cell value for each cell
                    //
                    neighbors = GetNeighborCount(x, y, chunkHandler);
                    cellValue = GetChunkCell(x, y, chunkHandler);

                    if (cellValue == 0)
                    {
                        if (neighbors == 2) _cellChanges.Add(GetUnrolledIndex(x, y), 1);
                    }
                    else
                    {
                        _cellChanges.Add(GetUnrolledIndex(x, y), 0);
                    }
                }
            }

        }

        /// <summary>
        /// Queues up all of the changes for the chunk, according to the
        /// rules of High Life
        /// </summary>
        /// <param name="chunkHandler"></param>
        public void QueueChangesForHighLife(ChunkHandler chunkHandler)
        {
            //
            // For High Life, we consider
            // 0 as dead
            // 1 as alive
            //
            int neighbors;
            int cellValue;

            //
            // Dump the queued up changes
            //
            _cellChanges.Clear();

            //
            // Iterate through the board
            //
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    //
                    // Get the neighbor count, and cell value for each cell
                    //
                    neighbors = GetNeighborCount(x, y, chunkHandler);
                    cellValue = GetChunkCell(x, y, chunkHandler);

                    if (cellValue == 0)
                    {
                        if (neighbors == 3 || neighbors==6) _cellChanges.Add(GetUnrolledIndex(x, y), 1);
                    }
                    else
                    {
                        if (neighbors < 2 || neighbors > 3) _cellChanges.Add(GetUnrolledIndex(x, y), 0);
                    }
                }
            }

        }

        /// <summary>
        /// Applies the queued up changes created by the QueueChanges function
        /// </summary>
        public void ApplyChanges()
        {            
            //
            // If no cells have changed, and the chunk is entirely empty, set it as "hibernating"
            //
            if (!_cellChanges.Any())
            {
                if (IsEmpty()) _activityLevel = ActivityState.HIBERNATING;
            }
            //
            // If cells have changed, set it as "active"
            //
            else
            {
                _activityLevel = ActivityState.ACTIVE;
                _hibernationTimer = 0;
            }

            //
            // If the chunk is hibernating for a long period, set it as "dead"
            // It will be removed from the list by the chunk handler
            //
            if (_activityLevel == ActivityState.HIBERNATING)
            {
                _hibernationTimer++;
                if (_hibernationTimer > 20) _activityLevel = ActivityState.DEAD;
            }

            //
            // Toggle the values for each of the queued up cell changes
            //
            foreach (KeyValuePair<int, int> cellChange in _cellChanges)
            {
                _board[cellChange.Key] = cellChange.Value;
            }
        }

        #endregion

        #region Constructors

        public Chunk(int xIndex, int yIndex, int width, int height)
        {
            _xIndex = xIndex;
            _yIndex = yIndex;
            _width = width;
            _height = height;
            _board = new int[width * height]; // Using a one dimensional array, that represents that chunk unrolled
            _queueChanges = QueueChangesForSeeds;
        }

        #endregion
    }
}
