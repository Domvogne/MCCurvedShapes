namespace MCDApp
{
    public class LineView : IFigureView
    {
        public event ShemeUpdate OnNewSheme;

        private int angle;
        private int lenght;

        public int Lenght
        {
            get { return lenght; }
            set
            {
                lenght = value;
                Rebuild();
            }
        }

        public int Angle
        {
            get { return angle; }
            set
            {
                angle = value;
                Rebuild();
            }
        }


        public void Rebuild()
        {

        }
    }
}