namespace IDT4.Engine.CM
{
    internal class ProcNode
    {
        public idPlane plane;
        public int[] children = new int[2];    // negative numbers are (-1 - areaNumber), 0 = solid
    }
}
