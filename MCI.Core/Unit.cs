namespace Miharu
{
    public sealed class Unit
    {
        private Unit()
        {
        }

        public static readonly Unit Instance;

        static Unit()
        {
            Instance = new Unit();
        }
    }
}
