using Assets.Scripts.Data;
namespace Assets.Scripts.Core
{
    public class Model
    {
        private static Tile[] _grid;
        public static Tile[] Grid { get => _grid; }

        public Model()
        {
            _grid = new Tile[Config.WIDTH * Config.HEIGHT];

            CreateGrid();
        }

        private void CreateGrid()
        {
            float xCell;
            float yCell;

            for (int i = 0; i < _grid.Length; i++)
            {
                yCell = (i / Config.WIDTH) * Config.SIZE_H;
                xCell = (i - i / Config.WIDTH * Config.WIDTH) * Config.SIZE_W;
                _grid[i] = new Tile(xCell, -yCell, i);
            }
        }

    }
}
