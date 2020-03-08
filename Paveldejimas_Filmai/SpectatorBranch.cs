using System;

namespace _21
{
    class SpectatorBranch
    {
        public Spectator[] Spectators;
        public int Count { get; private set; }
        private const int MAXSPECT = 50;

        public SpectatorBranch()
        {
            Spectators = new Spectator[MAXSPECT];
        }
        public void AddSpectator(Spectator ziurov)
        {
            Spectators[Count++] = ziurov;
        }
        public void AddSpectator(Spectator ziurov, int index)
        {
            Spectators[index] = ziurov;
        }
        public Spectator GetSpectator(int index)
        {
            return Spectators[index];
        }
    }
}
