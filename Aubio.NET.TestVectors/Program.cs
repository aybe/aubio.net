using System.Linq;
using Aubio.NET.Vectors;

namespace Aubio.NET.TestVectors
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            using (var vec = new CVec(10))
            {
                vec.Norm.Ones();
                vec.Norm.SetAll(2);
                vec.Norm.Zeros();

                vec.Phas.Ones();
                vec.Phas.SetAll(2);
                vec.Phas.Zeros();

                vec[0] = new CVecComplex(3, 4);
                var complex = vec[0];

                vec.Zeros();
                vec.Ones();
                vec.SetAll(new CVecComplex(5, 6));

                var array = vec.ToArray();
            }

            using (var vec = new FVec(10))
            {
                vec[0] = 1;
                var f = vec[0];
                vec.SetAll(5);
                vec.Zeros();
                vec.Ones();
                var array = vec.ToArray();
            }

            using (var vec = new LVec(10))
            {
                vec[0] = 1;
                var d = vec[0];
                vec.SetAll(5);
                vec.Zeros();
                vec.Ones();
                var array = vec.ToArray();
            }

            using (var mat1 = new FMat(2, 5))
            using (var mat2 = new FMat(mat1.Rows, mat1.Columns))
            {
                for (var row = 0; row < mat1.Rows; row++)
                for (var col = 0; col < mat1.Columns; col++)
                    mat1[row, col] = row * mat1.Columns + col;

                var enumerables = mat1.ToArray();

                using (var channel = mat1.GetChannel(0))
                {
                    channel.Print();
                }

                mat2.Ones();
                mat2.Weight(mat1);

                using (var scale = new FVec(mat1.Columns))
                using (var output = new FVec(mat1.Rows))
                {
                    for (var i = 0; i < scale.Length; i++)
                        scale[i] = i;

                    output.Ones();
                    mat1.VecMul(scale, output);
                }

                mat1.Copy(mat2);

                mat1.Print();
                mat1.Rev();
                mat1.Ones();
                mat1.Zeros();
                mat1.Set(1234.0f);
            }

            AubioUtils.Cleanup();
        }
    }
}