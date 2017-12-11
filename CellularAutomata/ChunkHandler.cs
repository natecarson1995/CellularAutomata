using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CellularAutomata
{
    public enum GameRules
    {
        CONWAYS,
        BRIANSBRAIN,
        HIGHLIFE,
        SEEDS
    }
    class ChunkHandler
    {
        #region Fields

        private const int CHUNKWIDTH = 30;
        private const int CHUNKHEIGHT = 30;
        private List<Chunk> _activeChunks = new List<Chunk>();
        private GameRules _gameRules;

        #endregion

        #region Properties

        public GameRules GameRules
        {
            get { return _gameRules; }
            set { _gameRules = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Change's the rules for the automaton
        /// </summary>
        /// <param name="gameRules"></param>
        public void ChangeGameRules(GameRules gameRules)
        {
            _gameRules = gameRules;

            foreach (Chunk chunk in _activeChunks)
            {
                UpdateChunkRules(chunk);
            }
        }

        /// <summary>
        /// Set the chunks game rules to the current set of rules
        /// </summary>
        /// <param name="chunk"></param>
        private void UpdateChunkRules(Chunk chunk)
        {
            switch (_gameRules)
            {
                case GameRules.CONWAYS:
                    chunk.QueueChanges = chunk.QueueChangesForConways;
                    break;
                case GameRules.BRIANSBRAIN:
                    chunk.QueueChanges = chunk.QueueChangesForBriansBrain;
                    break;
                case GameRules.HIGHLIFE:
                    chunk.QueueChanges = chunk.QueueChangesForHighLife;
                    break;
                case GameRules.SEEDS:
                    chunk.QueueChanges = chunk.QueueChangesForSeeds;
                    break;
            }
        }

        /// <summary>
        /// Goes through all of the chunks, queues up their changes, then applies them.
        /// Finally removes chunks that are dead.
        /// </summary>
        public void AdvanceBoard()
        {
            foreach (Chunk chunk in _activeChunks.ToArray())
            {
                chunk.QueueChanges(this);
                if (chunk.ActivityLevel == Chunk.ActivityState.DEAD) _activeChunks.Remove(chunk);
            }
            foreach (Chunk chunk in _activeChunks)
            {
                chunk.ApplyChanges();
            }
        }
        
        /// <summary>
        /// Adds a new chunk at the specific x and y indices, and inserts it in the list, sorted
        /// </summary>
        /// <param name="xIndex"></param>
        /// <param name="yIndex"></param>
        /// <returns></returns>
        private Chunk AddNewChunk(int xIndex, int yIndex)
        {
            int newChunkIndex = 0;

            //
            // Finds where to insert the chunk to make sure the list is sorted
            //
            for (int index = 0; index < _activeChunks.Count; index++)
            {
                if (_activeChunks[index].YIndex > yIndex || (_activeChunks[index].YIndex == yIndex && _activeChunks[index].XIndex > xIndex))
                {
                    newChunkIndex = Math.Max(0, index - 1);
                    break;
                }
            }

            //
            // Actually instantiates and inserts the chunk
            //
            Chunk chunk = new Chunk(xIndex, yIndex, CHUNKWIDTH, CHUNKHEIGHT);
            UpdateChunkRules(chunk);
            _activeChunks.Insert(newChunkIndex, chunk);

            return chunk;
        }

        /// <summary>
        /// Goes through all of the chunks, returns the chunk if its already made
        /// If makeChunk is true, make it if not already made.
        /// </summary>
        /// <param name="xIndex"></param>
        /// <param name="yIndex"></param>
        /// <param name="makeChunk"></param>
        /// <returns></returns>
        public Chunk GetChunkAt(int xIndex, int yIndex, bool makeChunk=false)
        {
            //
            // Checks through all of the chunks for one with the specific x and y index
            //
            foreach (Chunk chunk in _activeChunks)
            {
                if (chunk.XIndex == xIndex && chunk.YIndex == yIndex)
                {
                    return chunk;
                }
            }

            //
            // If the chunk is not found, and makeChunk is true, create the chunk, hibernating by default
            //
            if (makeChunk)
            {
                Chunk c = AddNewChunk(xIndex, yIndex);
                c.ActivityLevel = Chunk.ActivityState.HIBERNATING;
                return c;
            }
            //
            // Elsewise just return a null value
            //
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the value for a specific cell in a specific chunk
        /// </summary>
        /// <param name="chunkXIndex"></param>
        /// <param name="chunkYIndex"></param>
        /// <param name="cellXIndex"></param>
        /// <param name="cellYIndex"></param>
        /// <param name="makeChunk"></param>
        /// <returns></returns>
        public int GetChunkCellAt(int chunkXIndex, int chunkYIndex, int cellXIndex, int cellYIndex, bool makeChunk=false)
        {
            Chunk chunk = GetChunkAt(chunkXIndex, chunkYIndex,makeChunk);
            if (chunk == null) return 0;
            else return chunk.GetChunkCell(cellXIndex, cellYIndex, this);
        }

        /// <summary>
        /// Gets the value for a cell in the entire grid, finding its value from the correct chunk
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int GetCellAt(int x, int y)
        {
            //
            // Do some math to find what the chunk's x and y indices would be
            //
            int chunkXIndex = (int)Math.Floor((double)x / CHUNKWIDTH);
            int chunkYIndex = (int)Math.Floor((double)y / CHUNKHEIGHT);

            //
            // Get that chunk if it exists
            //
            Chunk chunk = GetChunkAt(chunkXIndex, chunkYIndex);

            //
            // Find where the cell would be in that chunk
            //
            int cellXIndex = x % CHUNKWIDTH;
            int cellYIndex = y % CHUNKHEIGHT;
            
            //
            // If there is a negative x or y index, mirror them
            //
            if (cellXIndex<0) cellXIndex += CHUNKWIDTH;
            if (cellYIndex < 0) cellYIndex += CHUNKHEIGHT;

            //
            // If the chunk doesn't exist, or is hibernating, you know the cell value is 0
            //
            if (chunk == null || chunk.ActivityLevel == Chunk.ActivityState.HIBERNATING) return 0;
            
            //
            // Elsewise, just return the cell's value
            //
            else return chunk.GetChunkCell(cellXIndex, cellYIndex, this);
        }

        /// <summary>
        /// Set the value at a specific gridwide x and y index, making the chunk if necessary
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="value"></param>
        public void SetCellAt(int x, int y, int value)
        {
            //
            // Do some math to find what the chunk's x and y indices would be
            //
            int chunkXIndex = (int)Math.Floor((double)x / CHUNKWIDTH);
            int chunkYIndex = (int)Math.Floor((double)y / CHUNKHEIGHT);

            //
            // Get that chunk if it exists, make it if not
            //
            Chunk chunk = GetChunkAt(chunkXIndex, chunkYIndex, true);

            //
            // Find where the cell would be in that chunk
            //
            int cellXIndex = x % CHUNKWIDTH;
            int cellYIndex = y % CHUNKHEIGHT;

            //
            // If there is a negative x or y index, mirror them
            //
            if (cellXIndex < 0) cellXIndex += CHUNKWIDTH;
            if (cellYIndex < 0) cellYIndex += CHUNKHEIGHT;

            //
            // Set the chunk to active, and set the cell's value
            //
            chunk.ActivityLevel = Chunk.ActivityState.ACTIVE;
            chunk.HibernationTimer = 0;
            chunk.SetCellAt(cellXIndex, cellYIndex, value);
        }

        /// <summary>
        /// Clears out the entire grid
        /// </summary>
        public void ClearMap()
        {
            _activeChunks.Clear();
        }

        /// <summary>
        /// Set each cell in a specified width and height to a random value
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void RandomMap(int width, int height)
        {
            ClearMap();

            Random random = new Random();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (random.NextDouble() > .5) SetCellAt(x, y, 1);
                }
            }
        }

        #endregion

        #region Constructors

        public ChunkHandler()
        {

        }

        #endregion
    }
}
