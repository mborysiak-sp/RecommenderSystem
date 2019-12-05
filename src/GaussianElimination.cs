﻿using System;

namespace RecommenderSystem
{
	class GaussianElimination
	{
		private void ZeroColumn(Matrix matrix, Matrix vector, int i)
		{
			for (int j = i + 1; j < matrix.RowCount; j++)
			{
				double m = matrix.Data[j, i] / matrix.Data[i, i];

				for (int k = 0; k < matrix.ColumnCount; k++)
					matrix.Data[j, k] -= matrix.Data[i, k] * m;
				vector.Data[j, 0] -= vector.Data[i, 0] * m;
			}
		}

		public Matrix EliminateWithPartialPivoting(Matrix matrix, Matrix vector)
		{
			for (int i = 0; i < matrix.RowCount; i++)
			{
				PartialPivot(matrix, vector, i);
				ZeroColumn(matrix, vector, i);
			}

			return GetResults(matrix, vector);
		}

		private Matrix GetResults(Matrix matrix, Matrix vector)
		{
			var results = new Matrix(matrix.RowCount, 1);

			for (int i = matrix.RowCount - 1; i >= 0; i--)
			{
				double sum = new double();

				for (int j = i + 1; j < matrix.RowCount; j++)
					sum += matrix.Data[i, j] * results.Data[j, 0];
				results.Data[i, 0] = (vector.Data[i, 0] - sum) / matrix.Data[i, i];
			}

			return results;
		}

		private void PartialPivot(Matrix matrix, Matrix vector, int p)
		{
			for (int j = p; j < matrix.RowCount; j++)
				if (Math.Abs(matrix.Data[p, p]) < Math.Abs(matrix.Data[j, p]))
				{
					matrix.SwapRows(p, j);
					vector.SwapRows(p, j);
				}
		}
	}
}
}
