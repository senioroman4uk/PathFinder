using System.Collections.Generic;
using System.Linq;

namespace PathFinder.Trips.WebApi.Mappers
{
    public static class MatrixMapper
    {
        public static bool Validate(this List<List<double>> parsedMatrix)
        {
            int length = parsedMatrix.Count;

            return parsedMatrix.All(row => row.Count == length);
        }

        public static double[,] ToSquareMatrix(this List<List<double>> parsedMatrix)
        {
            double[,] matrix = new double[parsedMatrix.Count, parsedMatrix.Count];

            for (int i = 0; i < parsedMatrix.Count; i++)
            {
                for (int j = 0; j < parsedMatrix[i].Count; j++)
                {
                    matrix[i, j] = parsedMatrix[i][j];
                }
            }

            return matrix;
        }
    }
}