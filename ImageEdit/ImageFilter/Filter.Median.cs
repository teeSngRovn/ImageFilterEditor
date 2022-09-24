using OpenCvSharp;

namespace ImageFilter{
    public class MedianFilter:FilterBaseClass{
        public override FilterTypeCode FilterType{
            get=>FilterTypeCode.Median;
        }
        #region  OptimizeFunc
        public override Mat OptimizeApplyOperator(Mat srcMat,int armLength = 1){
            return base.PrimitiveApplyOperator(srcMat,armLength);
        }
        #endregion
        #region  PrimitiveFunc
        public override byte PrimitiveOperator(ref float[,] optArr, Mat extMat, int row, int col, int armLength = 1)
        {
            List<byte> sortList = new();


            for (int i = 0;i < 2 * armLength + 1;i++){
                for (int j = 0;j < 2 * armLength + 1;j++){
                    sortList.Add(extMat.At<byte>(i + row - armLength,j + col - armLength));
                }
            }
            sortList.Sort();
            /*
            众数查找
            Dictionary<byte,int> test = new();
            for (int i = 0;i<sortList.Count;i++){
                byte temp = sortList[i];
                if (!test.ContainsKey(temp)){
                    test.Add(temp,1);
                }else{
                    test[temp]++;
                }
            }
            byte MaxCountValue = 0;
            int MaxCount = 0;
            foreach(var i in test){
                if (i.Value>MaxCount){
                    MaxCount = i.Value;
                    MaxCountValue = i.Key;
                }
            }
            return MaxCountValue;
            return (MaxCount==1)?sortList[(2*armLength+1)*(2*armLength+1)/2]:MaxCountValue;
            */

            return sortList[(2*armLength+1)*(2*armLength+1)/2];
        }
        public override float[,] GetOperatorArr(int armLength){
            Mat result = Mat.Ones(2*armLength+1,2*armLength+1,MatType.CV_32FC1);
            result =  result / ((2*armLength+1)*(2*armLength+1));
            result.GetRectangularArray<float>(out float[,]data);
            return data;
        }
        #endregion
    }
}