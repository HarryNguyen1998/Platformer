using UnityEngine.Assertions;

namespace MyPlatformer
{
    public class Map2D
    {
        public int Width;
        public int Height;
        public bool[] map;
        
        // Indexer
        public bool this[int x, int y]
        {
            get { return map[y * Width + x]; }
            set { map[y * Width + x] = value; }
        }

        public Map2D(int Width, int Height, bool isEmpty)
        {
            Assert.IsTrue(Width > 0 && Height > 0, $"{this} CAN only have <b><color=yellow>positive</color></b> width and height");

            this.Width = Width;
            this.Height = Height;
            map = new bool[Width * Height];
            for (int y = 0; y < Height; ++y)
            {
                for (int x = 0; x < Width; ++x)
                {
                    if (isEmpty)
                        this[x, y] = false;
                    else
                        this[x, y] = true;
                }
            }
        }


    }
}
