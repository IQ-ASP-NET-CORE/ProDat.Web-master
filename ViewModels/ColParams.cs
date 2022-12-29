namespace ProDat.Web2.ViewModels
{
    public class ColParams
    {
        public ColParams(int Order, int Width)
        {
            this.Order = Order;
            this.Width = Width;
        }

        public ColParams(int Order, int Width, bool Visible)
        {
            this.Order = Order;
            this.Width = Width;
            this.Visible = Visible;
        }

        public int Order { get; set; }
        public int Width { get; set; }
        public bool Visible { get; set; }
    }
}
