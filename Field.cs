namespace ConsoleMinesweeper
{

    class Field
    {

        public bool IsMine { get; set; }
        public bool IsHidden { get; set; }
        public int NearbyMines { get; set; }

        public Field()
        {
            IsMine = false;
            IsHidden = true;
            NearbyMines = 0;
        }

        public override string ToString()
        {
            if (IsHidden)
            {
                return ".";
            }
            else if (IsMine)
            {
                return "X";
            }
            else
            {
                return NearbyMines.ToString();
            }
        }

    }

}
