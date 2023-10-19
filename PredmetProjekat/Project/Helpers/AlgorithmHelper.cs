using System.Collections.Generic;
using System.Linq;

namespace Project.Helpers
{
    public static class AlgorithmHelper
    {
        #region Properties
        public static bool[,] map = new bool[CanvasHelper.Size, CanvasHelper.Size];
        static Queue<List<(int, int)>> queue = new Queue<List<(int, int)>>();
        #endregion

        #region Methods
        private static List<(int, int)> GetAdjacentNeighbors((int, int) pos, (int, int) end)
        {
            List<(int, int)> neighbors = new List<(int, int)>();  //provjere okoline tacke koliko je otislo oko, treba da konvergira ka krajnjoj tacki

            if (pos.Item1 > end.Item1 && pos.Item1 > 0)
                neighbors.Add((pos.Item1 - 1, pos.Item2));

            if (pos.Item2 > end.Item2 && pos.Item2 > 0)
                neighbors.Add((pos.Item1, pos.Item2 - 1));

            if (pos.Item1 < end.Item1 && pos.Item1 < CanvasHelper.Size - 1)
                neighbors.Add((pos.Item1 + 1, pos.Item2));

            if (pos.Item2 < end.Item2 && pos.Item2 < CanvasHelper.Size - 1)
                neighbors.Add((pos.Item1, pos.Item2 + 1));

            return neighbors;
        }

        private static bool EvaluateNeighborCases((int, int) prev, (int, int) curr, (int, int) next)
        {
            if (prev.Item1 != curr.Item1 && curr.Item2 != next.Item2) //da li su x i y jednaki na putanji ako jesu crta liniju
                return true;

            if (prev.Item2 != curr.Item2 && curr.Item1 != next.Item1)
                return true;

            return false;
        }

        private static void ClearQueue()
        {
            queue.Clear();
        }

        private static List<(int,int)> FilterAndFillMappedPath(List<(int,int)> path)  //da se iscrtavaju pod pravim uglom
        {
            var returnValue = new List<(int,int)>();
            returnValue.Add(path.First());

            for(int i = 1; i < path.Count - 1; i++)
            {
                map[path[i].Item1, path[i].Item2] = true;
                if (EvaluateNeighborCases(path[i - 1], path[i], path[i + 1]))
                    returnValue.Add(path[i]);
            }

            returnValue.Add(path.Last());
            ClearQueue();
            return returnValue;
        }

        private static List<(int, int)> ExtractMarkedPoints(List<(int, int)> path)
        {
            // Kreira se nova lista "returnValue" u koju će biti smještene tačke koje su označene na mapi.
            var returnValue = new List<(int, int)>();
            for (int i = 1; i < path.Count - 1; i++) // Petlja prolazi kroz sve tačke u putanji, osim prve i poslednje tačke.
            {
                if (map[path[i].Item1, path[i].Item2])
                    returnValue.Add(path[i]);
            }
            return returnValue;
        }

        public static List<(int, int)> FindFirstLinePoints((int, int) start, (int, int) end)
        {
            bool[,] visited = new bool[CanvasHelper.Size, CanvasHelper.Size];  //matrica bool vrijednosti
            queue.Enqueue(new List<(int, int)>() { start });

            while (queue.Count > 0)
            {
                var path = queue.Dequeue();
                var last = path.Last();

                if (visited[last.Item1, last.Item2])
                    continue;

                visited[last.Item1, last.Item2] = true;

                var neighbors = GetAdjacentNeighbors(last, end);
                for(int i = 0; i < neighbors.Count; i++)
                {
                    if (neighbors[i] == end)
                    {
                        path.Add(neighbors[i]);
                        return FilterAndFillMappedPath(path);
                    }

                    if (!visited[neighbors[i].Item1, neighbors[i].Item2] && !map[neighbors[i].Item1, neighbors[i].Item2])
                    {
                        List<(int, int)> newPath = new List<(int, int)>(path);
                        newPath.Add(neighbors[i]);
                        queue.Enqueue(newPath);
                    }
                }
            }

            ClearQueue();
            return null;
        }

        public static (List<(int, int)>, List<(int, int)>) FindSecondLinePointsWithMarks((int, int) start, (int, int) end) //racuna pocetak, imam start i kraj
        {
            bool[,] visited = new bool[CanvasHelper.Size, CanvasHelper.Size];  //bool matrica
            queue.Enqueue(new List<(int, int)>() { start });

            while (queue.Count > 0)  //kada gueue ima vise od nula 
            {
                var path = queue.Dequeue();  //izbacim iz reda
                var last = path.Last();  //uzima posljednju tacku iz putanje

                if (visited[last.Item1, last.Item2])  //ako je visited nastavljam dalje 
                    continue;

                visited[last.Item1, last.Item2] = true;  //postaje visited prosli smo kroz nju

                var neighbors = GetAdjacentNeighbors(last, end);
                for (int i = 0; i < neighbors.Count; i++)
                {
                    if (neighbors[i] == end)
                    {
                        path.Add(neighbors[i]);
                        var marks = ExtractMarkedPoints(path);
                        return (marks, FilterAndFillMappedPath(path));
                    }

                    if (!visited[neighbors[i].Item1, neighbors[i].Item2])
                    {
                        List<(int, int)> newPath = new List<(int, int)>(path);
                        newPath.Add(neighbors[i]);
                        queue.Enqueue(newPath);
                    }
                }
            }

            ClearQueue();
            return (null, null);
        }
        #endregion
    }
}
