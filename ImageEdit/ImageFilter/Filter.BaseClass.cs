using OpenCvSharp;

namespace ImageFilter{
    interface IFilter{
        public Mat ApplyFilter(Mat srcMat,int armLength,FilterOperatorTypeCode optType);
    }
    public abstract class FilterBaseClass:IFilter{
        private delegate Mat OperatorFunc(Mat srcMat,int armLength);
        public abstract FilterTypeCode FilterType{get;}
        public abstract Mat OptimizeApplyOperator(Mat srcMat,int armLength = 1);
        public abstract float[,] GetOperatorArr(int armLength = 1);
        public Mat ApplyFilter(Mat srcMat,int armLength,FilterOperatorTypeCode optType){
            List<Mat> multi_channel = new();
            OperatorFunc OptFunc;
            Mat result = new();
            switch (optType){
                case FilterOperatorTypeCode.Primitive:
                    OptFunc = new OperatorFunc(PrimitiveApplyOperator);
                    break;
                case FilterOperatorTypeCode.Optimize:
                    OptFunc = new OperatorFunc(OptimizeApplyOperator);
                    break;
                default:
                    OptFunc = new OperatorFunc(PrimitiveApplyOperator);
                    break;
            }
            for (int i = 0;i<srcMat.Channels();i++){
                multi_channel.Add(OptFunc(srcMat.ExtractChannel(i),armLength));
            }
            Cv2.Merge(multi_channel.ToArray(),result);
            return result;
        }
        public virtual byte PrimitiveOperator(ref float[,] optArr,Mat extMat,int row,int col,int armLength = 1){
            float sum = 0;
            for (int i = 0;i < 2*armLength+1;i++){
                for (int j = 0;j < 2 * armLength + 1;j++){
                    sum += optArr[i,j]*extMat.At<byte>(i+row-armLength,j+col-armLength);
                }
            }
            sum = Math.Min(255,sum);
            return (byte)sum;
        }
        public Mat PrimitiveApplyOperator(Mat srcMat,int armLength = 1){
            int borderSize = armLength + 1;
            Mat extendMat = CopyMakeBorder(srcMat,borderSize);
            srcMat.GetRectangularArray<byte>(out byte[,] data);
            float[,] OptArr = GetOperatorArr(armLength);
            for (int i = 0;i < srcMat.Rows;i++){
                for (int j = 0;j < srcMat.Cols;j++){
                    #if __debug__
                    Console.WriteLine("source:"+extendMat.At<byte>(i,j));
                    #endif
                    data[i,j] = PrimitiveOperator(ref OptArr,extendMat,borderSize+i,borderSize+j,armLength);
                    #if __debug
                    Console.WriteLine("result:"+data[i,j]);
                    #endif
                }
            }
            Mat result = new(srcMat.Rows,srcMat.Cols,MatType.CV_8UC1);
            result.SetRectangularArray<byte>(data);
            return result;
        }
        public Mat CopyMakeBorder(Mat srcMat,int WindowArmLength){
            Mat result = new(srcMat.Rows+2*WindowArmLength,srcMat.Cols+2*WindowArmLength,srcMat.Type());
            byte[,] temp = new byte[srcMat.Rows+2*WindowArmLength,srcMat.Cols+2*WindowArmLength];
            for (int i = 0;i<srcMat.Rows;i++){
                for (int j = 0;j<srcMat.Cols;j++){
                    temp[WindowArmLength + i,WindowArmLength + j] = srcMat.At<byte>(i,j);
                }
            }
            result.SetRectangularArray<byte>(temp);
            return result;
        }
    }   
}  