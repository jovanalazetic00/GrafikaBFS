namespace Project.Helpers
{
    public class CanvasHelper
    {
        public static bool RenderNodes { get; set; } = true;  //za renderovanje sviceva
        public static int Size { get; set; }  //velicina mape
        public static int Move { get; set; }  //koliko ima prostora oko tackica
        public static int DotSize { get; set;}  //velicina tacke 
        public static double CanvasMinX { get; set; } //trazi se min i max koordinata x i z da znam koja ce biti ramjera na mapi
        public static double CanvasMaxX { get; set; }
        public static double CanvasMinY { get; set; }
        public static double CanvasMaxY { get; set; }
        public static double CanvasOffsetX { get; set; }  //za racunanje 
        public static double CanvasOffsetY { get; set; }
    }
}
