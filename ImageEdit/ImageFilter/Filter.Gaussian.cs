using OpenCvSharp;

namespace ImageFilter{
    public class GaussianFilter:FilterBaseClass{
        public override FilterTypeCode FilterType{
            get=>FilterTypeCode.Gaussian;
        }
        private float GaussianSigma = 5f;
        #region ConstantSetting
        public void SetGaussianSigma(float sigma){
            GaussianSigma = Math.Min(Math.Max(sigma,0),255);
        }
        #endregion
        #region OptimizeFunc
        public override Mat OptimizeApplyOperator(Mat srcMat,int armLength = 1){
            int borderSize = armLength + 1;
            Mat extendMat = CopyMakeBorder(srcMat,borderSize);
            float[] GaussianArr = GetGaussianArrayOnVar(armLength);
            byte[,] resultx = new byte[extendMat.Rows,srcMat.Cols];
            byte[,] result = new byte[srcMat.Rows,srcMat.Cols];


            for (int i = 0;i<extendMat.Rows;i++){
                for (int j = 0;j<srcMat.Cols;j++){
                    resultx[i,j] = IntegralFromAxis(ref GaussianArr,extendMat,i,j+borderSize,armLength,"X");
                }
            }
            Mat MatX = new(extendMat.Rows,srcMat.Cols,MatType.CV_8UC1);
            MatX.SetRectangularArray<byte>(resultx);
            for (int i = 0;i<srcMat.Rows;i++){
                for (int j = 0;j<srcMat.Cols;j++){
                    result[i,j] = IntegralFromAxis(ref GaussianArr,MatX,i+borderSize,j,armLength,"Y");
                }
            }


            Mat MatResult = new(srcMat.Rows,srcMat.Cols,MatType.CV_8UC1);
            MatResult.SetRectangularArray<byte>(result);
            return MatResult;
        }
        public byte IntegralFromAxis(ref float[]optArr,Mat srcMat,int row,int col,int armLength,string Axis){
            float sum = 0f;
            for (int i = 0;i < 2 * armLength + 1;i++){
                if (Axis.ToUpper() == "X"){
                    sum += optArr[i] * srcMat.At<byte>(row,i + col - armLength);
                }else{
                    sum += optArr[i] * srcMat.At<byte>(i + row - armLength,col);
                }
            }
            sum = Math.Min(255,sum);
            return (byte)(sum);
        }
        public float[] GetGaussianArrayOnVar(int armLength){
            float[] result = new float[2 * armLength + 1];
            float sum = 0;


            for (int i = 0;i < 2 * armLength + 1;i++){
                int delta = i - armLength;
                result[i] = (float)Math.Exp(-(delta*delta)/(2*GaussianSigma*GaussianSigma));
                sum += result[i];
            }
            sum = 1/sum;
            for (int i = 0;i < 2 * armLength + 1;i++){
                result[i] *= sum;
            }


            return result;
        }
        #endregion
        #region PrimitiveFunc
        public override float[,] GetOperatorArr(int armLength){
            Mat result = Mat.Ones(2*armLength+1,2*armLength+1,MatType.CV_32FC1);
            result.GetRectangularArray<float>(out float[,]data);
            float sum = 0;


            for (int i = 0;i < 2 * armLength + 1;i++){
                for (int j = 0;j < 2 * armLength + 1;j++){
                    int dx = i - armLength;
                    int dy = j - armLength;
                    data[i,j] = (float)Math.Exp(-(dx*dx+dy*dy)/(2*GaussianSigma*GaussianSigma));
                    sum += data[i,j];
                }
            }
            sum = 1/sum;
            for (int i = 0;i < 2 * armLength + 1;i++){
                for (int j = 0;j < 2 * armLength + 1;j++){
                    data[i,j] *= sum;
                }
            }

            return data;
        }
        #endregion
    }
}