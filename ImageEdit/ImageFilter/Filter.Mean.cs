using OpenCvSharp;

namespace ImageFilter{
    public class MeanFilter:FilterBaseClass{
        public override FilterTypeCode FilterType{
            get=>FilterTypeCode.Mean;
        }
        #region  OptimizeFunc
        public override Mat OptimizeApplyOperator(Mat srcMat,int armLength = 1){
            int borderSize = armLength + 1;
            Mat extendMat = CopyMakeBorder(srcMat,borderSize);
            srcMat.GetRectangularArray<byte>(out byte[,] data);
            int[,] IntArr = Integral(extendMat);
            float[,] OptArr = GetOperatorArr(armLength);
            
            for (int i = 0;i < srcMat.Rows;i++){
                for (int j = 0;j < srcMat.Cols;j++){
                    #if __debug__
                    Console.WriteLine("source:"+extendMat.At<byte>(i,j));
                    #endif
                    data[i,j] = OptimizeOperator(ref OptArr,ref IntArr,borderSize+i,borderSize+j,armLength);
                    #if __debug
                    Console.WriteLine("result:"+data[i,j]);
                    #endif
                }
            }

            Mat result = new(srcMat.Rows,srcMat.Cols,MatType.CV_8UC1);
            result.SetRectangularArray<byte>(data);
            return result;
        }
        private byte OptimizeOperator(ref float[,] optArr,ref int[,] itgArr,int row,int col,int armLength = 1){
            int sum = itgArr[row+armLength,col+armLength] + itgArr[row-armLength-1,col-armLength-1]
                    -(itgArr[row-armLength-1,col+armLength] + itgArr[row+armLength,col-armLength-1]);
            sum /= (2*armLength+1)*(2*armLength+1);
            sum = (sum>255)?255:sum;
            return (byte)sum;
        }
        public int[,] Integral(Mat srcMat){
            srcMat.GetRectangularArray<byte>(out byte[,] data);
            int[,] result = new int[srcMat.Rows,srcMat.Cols];
        
            for (int i = 1;i < srcMat.Cols;i++){
                int temp = result[0,i-1] + data[0,i];
                result[0,i] = temp;
            }
            for (int i = 1;i < srcMat.Rows;i++){
                int temp = 0;
                for (int j = 0;j < srcMat.Cols;j++){
                    temp += data[i,j];
                    result[i,j] = temp + result[i-1,j];
                }
            }

            return result;
        }
        #endregion
        #region  PrimitiveFunc
        public override float[,] GetOperatorArr(int armLength){
            Mat result = Mat.Ones(2*armLength+1,2*armLength+1,MatType.CV_32FC1);
            result =  result / ((2*armLength+1)*(2*armLength+1));
            result.GetRectangularArray<float>(out float[,]data);
            return data;
        }
        #endregion
    }
}